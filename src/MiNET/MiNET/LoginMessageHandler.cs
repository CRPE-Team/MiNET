﻿#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2021 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using fNbt;
using Jose;
using log4net;
using MiNET.Net;
using MiNET.Net.RakNet;
using MiNET.Utils;
using MiNET.Utils.Cryptography;
using MiNET.Utils.IO;
using MiNET.Utils.Skins;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using SicStream;

namespace MiNET
{
	public class LoginMessageHandler : IMcpeMessageHandler
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(LoginMessageHandler));

		private readonly BedrockMessageHandler _bedrockHandler;
		private readonly RakSession _session;
		private readonly IServerManager _serverManager;

		private object _loginSyncLock = new object();
		private PlayerInfo _playerInfo = new PlayerInfo();

		static LoginMessageHandler()
		{
			JWT.DefaultSettings.JsonMapper = new NewtonsoftMapper();
		}

		public LoginMessageHandler(BedrockMessageHandler bedrockHandler, RakSession session, IServerManager serverManager)
		{
			_bedrockHandler = bedrockHandler;
			_session = session;
			_serverManager = serverManager;
		}

		public void Disconnect(string reason, bool sendDisconnect = true)
		{
		}

		public virtual void HandleMcpeRequestNetworkSettings(McpeRequestNetworkSettings message)
		{
			_playerInfo.ProtocolVersion = message.protocolVersion;
			if (_playerInfo.ProtocolVersion < McpeProtocolInfo.ProtocolVersion || _playerInfo.ProtocolVersion > 65535)
			{
				Log.Warn($"Wrong version ({_playerInfo.ProtocolVersion}) of Minecraft. Upgrade to join this server.");
				_session.Disconnect($"Wrong version ({_playerInfo.ProtocolVersion}) of Minecraft. Upgrade to join this server.");
				return;
			}

			var compressionAlgorithm = CompressionAlgorithm.ZLib;

			McpeNetworkSettings settingsPacket = McpeNetworkSettings.CreateObject();
			settingsPacket.compressionAlgorithm = (short) compressionAlgorithm;
			settingsPacket.compressionThreshold = _session.CompressionManager.CompressionThreshold;
			settingsPacket.clientThrottleEnabled = false;
			settingsPacket.clientThrottleScalar = 0;
			settingsPacket.clientThrottleThreshold = 0;
			settingsPacket.ForceClear = true; // Must be!

			_session.SendPrepareDirectPacket(settingsPacket);

			_session.CompressionManager.CompressionAlgorithm = compressionAlgorithm;
		}

		public virtual void HandleMcpeLogin(McpeLogin message)
		{
			// Only one login!
			lock (_loginSyncLock)
			{
				if (_session.Username != null)
				{
					Log.Info($"Player {_session.Username} doing multiple logins");
					return; // Already doing login
				}

				_session.Username = string.Empty;
			}

			_playerInfo.ProtocolVersion = message.protocolVersion;
			//_playerInfo.Edition = message.edition;

			DecodeCert(message);

			////string fileName = Path.GetTempPath() + "Skin_" + Skin.SkinType + ".png";
			////Log.Info($"Writing skin to filename: {fileName}");
			////Skin.SaveTextureToFile(fileName, Skin.Texture);
		}

		public void DecodeCert(McpeLogin message)
		{
			byte[] buffer = message.payload;

			if (message.payload.Length != buffer.Length)
			{
				Log.Debug($"Wrong lenght {message.payload.Length} != {message.payload.Length}");
				throw new Exception($"Wrong lenght {message.payload.Length} != {message.payload.Length}");
			}

			if (Log.IsDebugEnabled) Log.Debug("Lenght: " + message.payload.Length + ", Message: " + buffer.EncodeBase64());

			string certificateChain;
			string skinData;

			try
			{
				var destination = new MemoryStream(buffer);
				destination.Position = 0;
				// TODO - WHY NOT VarInt?!?!?!?! 
				NbtBinaryReader reader = new NbtBinaryReader(destination, NbtFlavor.BedrockNoVarInt);

				var countCertData = reader.ReadInt32();
				certificateChain = Encoding.UTF8.GetString(reader.ReadBytes(countCertData));
				if (Log.IsDebugEnabled) Log.Debug($"Certificate Chain (Lenght={countCertData})\n{certificateChain}");

				var countSkinData = reader.ReadInt32();
				skinData = Encoding.UTF8.GetString(reader.ReadBytes(countSkinData));
				if (Log.IsDebugEnabled) Log.Debug($"Skin data (Lenght={countSkinData})\n{skinData}");
			}
			catch (Exception e)
			{
				Log.Error("Parsing login", e);
				return;
			}

			try
			{
				{
					IDictionary<string, dynamic> headers = JWT.Headers(skinData);
					dynamic payload = JObject.Parse(JWT.Payload(skinData));

					if (Log.IsDebugEnabled) Log.Debug($"Skin JWT Header: {string.Join(";", headers)}");
					if (Log.IsDebugEnabled) Log.Debug($"Skin JWT Payload:\n{payload.ToString()}");

					// Skin JWT Payload: 

					//{
					//	"CapeData": "",
					//	"ClientRandomId": 1423700530444426768,
					//	"CurrentInputMode": 1,
					//	"DefaultInputMode": 1,
					//	"DeviceModel": "ASUSTeK COMPUTER INC. N550JK",
					//	"DeviceOS": 7,
					//	"GameVersion": "1.2.0.18",
					//	"GuiScale": 0,
					//	"LanguageCode": "en_US",
					//	"ServerAddress": "yodamine.com:19132",
					//	"SkinData": "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",
					//	"SkinGeometry": "ew0KICAiZ2VvbWV0cnkuaHVtYW5vaWQiOiB7DQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJib2R5IiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTQuMCwgMTIuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDgsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDE2LCAxNiBdDQogICAgICAgICAgfQ0KICAgICAgICBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogIndhaXN0IiwNCiAgICAgICAgIm5ldmVyUmVuZGVyIjogdHJ1ZSwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDEyLjAsIDAuMCBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImhlYWQiLA0KICAgICAgICAicGl2b3QiOiBbIDAuMCwgMjQuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtNC4wLCAyNC4wLCAtNC4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgOCwgOCwgOCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAwIF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAiaGF0IiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTQuMCwgMjQuMCwgLTQuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDgsIDgsIDggXSwNCiAgICAgICAgICAgICJ1diI6IFsgMzIsIDAgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC41DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiB0cnVlDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0QXJtIiwNCiAgICAgICAgInBpdm90IjogWyAtNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC04LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyA0MCwgMTYgXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInBpdm90IjogWyA1LjAsIDIyLjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgNC4wLCAxMi4wLCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgNCwgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtaXJyb3IiOiB0cnVlDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0TGVnIiwNCiAgICAgICAgInBpdm90IjogWyAtMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0zLjksIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAibGVmdExlZyIsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtaXJyb3IiOiB0cnVlDQogICAgICB9DQogICAgXQ0KICB9LA0KDQogICJnZW9tZXRyeS5jYXBlIjogew0KICAgICJ0ZXh0dXJld2lkdGgiOiA2NCwNCiAgICAidGV4dHVyZWhlaWdodCI6IDMyLA0KDQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJjYXBlIiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIC0zLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC01LjAsIDguMCwgLTMuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDEwLCAxNiwgMSBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAwIF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0NCiAgICBdDQogIH0sDQogICJnZW9tZXRyeS5odW1hbm9pZC5jdXN0b206Z2VvbWV0cnkuaHVtYW5vaWQiOiB7DQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJoYXQiLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiBmYWxzZSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdDQogICAgICB9LA0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDUuMCwgMjIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyA0LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAzMiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJyaWdodEFybSIsDQogICAgICAgICJyZXNldCI6IHRydWUsDQogICAgICAgICJwaXZvdCI6IFsgLTUuMCwgMjIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtOC4wLCAxMi4wLCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgNCwgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRJdGVtIiwNCiAgICAgICAgInBpdm90IjogWyAtNiwgMTUsIDEgXSwNCiAgICAgICAgIm5ldmVyUmVuZGVyIjogdHJ1ZSwNCiAgICAgICAgInBhcmVudCI6ICJyaWdodEFybSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAibGVmdFNsZWV2ZSIsDQogICAgICAgICJwaXZvdCI6IFsgNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIDQuMCwgMTIuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDQ4LCA0OCBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0U2xlZXZlIiwNCiAgICAgICAgInBpdm90IjogWyAtNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC04LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyA0MCwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0TGVnIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMC4xLCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0UGFudHMiLA0KICAgICAgICAicGl2b3QiOiBbIDEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMC4xLCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCA0OCBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAicG9zIjogWyAxLjksIDEyLCAwIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgLTEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMy45LCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAicG9zIjogWyAtMS45LCAxMiwgMCBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImphY2tldCIsDQogICAgICAgICJwaXZvdCI6IFsgMC4wLCAyNC4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC00LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA4LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfQ0KICAgIF0NCiAgfSwNCiAgImdlb21ldHJ5Lmh1bWFub2lkLmN1c3RvbVNsaW06Z2VvbWV0cnkuaHVtYW5vaWQiOiB7DQoNCiAgICAiYm9uZXMiOiBbDQogICAgICB7DQogICAgICAgICJuYW1lIjogImhhdCIsDQogICAgICAgICJuZXZlclJlbmRlciI6IGZhbHNlLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDUuMCwgMjEuNSwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyA0LjAsIDExLjUsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyAzLCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAzMiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJyaWdodEFybSIsDQogICAgICAgICJyZXNldCI6IHRydWUsDQogICAgICAgICJwaXZvdCI6IFsgLTUuMCwgMjEuNSwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtNy4wLCAxMS41LCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgMywgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgInBpdm90IjogWyAtNiwgMTQuNSwgMSBdLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiB0cnVlLA0KICAgICAgICAibmFtZSI6ICJyaWdodEl0ZW0iLA0KICAgICAgICAicGFyZW50IjogInJpZ2h0QXJtIg0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0U2xlZXZlIiwNCiAgICAgICAgInBpdm90IjogWyA1LjAsIDIxLjUsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgNC4wLCAxMS41LCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgMywgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDgsIDQ4IF0sDQogICAgICAgICAgICAiaW5mbGF0ZSI6IDAuMjUNCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRTbGVldmUiLA0KICAgICAgICAicGl2b3QiOiBbIC01LjAsIDIxLjUsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTcuMCwgMTEuNSwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDMsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDQwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImxlZnRMZWciLA0KICAgICAgICAicmVzZXQiOiB0cnVlLA0KICAgICAgICAibWlycm9yIjogZmFsc2UsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDE2LCA0OCBdDQogICAgICAgICAgfQ0KICAgICAgICBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImxlZnRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDQ4IF0sDQogICAgICAgICAgICAiaW5mbGF0ZSI6IDAuMjUNCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgLTEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMy45LCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImphY2tldCIsDQogICAgICAgICJwaXZvdCI6IFsgMC4wLCAyNC4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC00LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA4LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfQ0KICAgIF0NCiAgfQ0KDQp9DQo=",
					//	"SkinGeometryName": "geometry.humanoid.custom",
					//	"SkinId": "c18e65aa-7b21-4637-9b63-8ad63622ef01_Custom",
					//	"UIProfile": 0

					//}

					//	"CapeData": "////AAFHif8BMmH/ASNE/wEjRP8BI0T/ASNE/wEjRP8BI0T/ATJh/wFHif8BDhz/AQ4c/wEOHP8BDhz/AQ4c/wEOHP8BDhz/AQ4c/wEOHP8BDhz/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BR4n/AW3N/wFHif8BMmH/ATJh/wEyYf8BR4n/AUeJ/wFtzf8BR4n/ASNE/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wABI0T/ATJh/wFHif8Bbc3/AV6x/wFesf8BXrH/AV6x/wFtzf8BR4n/ATJh/wEjRP8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8AASNE/wE2af8BI0T/ASNE/wEjRP8BMmH/ATJh/wEyYf8BI0T/ASNE/wE2af8BI0T/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/////wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BR4n/ATJh/wEjRP8BI0T/ASNE/wEyYf8BNmn/ATZp/wEyYf8BNmn/ASNE/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/8BFiv/ARYr/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wABI0T/AUeJ/wEyYf8BNmn/ATJh/7aAGv9sWDT/AUeJ/wFHif8BMmH/AUeJ/wEjRP8BFiv/ARYr/wEWK/8BFiv/ARYr/wEdOf8BFiv/ARYr/wEWK/8BFiv/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8AASNE/wFHif8BMmH/toAa//m7Ff/5uxX/+bsV//m7Ff+2gBr/ATJh/wFHif8BI0T/ARYr/wEWK/8BFiv/ARYr/wEWK/8BHTn/ARYr/wEWK/8BFiv/ARYr/////wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BR4n/toAa//m7Ff+2gBr/bFg0/2xYNP+2gBr/+bsV/7aAGv8BR4n/ASNE/wEWK/8BFiv/ARYr/wEWK/8BFiv/AR05/wEWK/8BFiv/ARYr/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wABI0T/ATZp//m7Ff9sWDT/ATZp/7aAGv9sWDT/AW3N/2xYNP/5uxX/AUeJ/wEjRP8BFiv/ARYr/wEWK/8BFiv/ARYr/wEdOf8BHTn/ARYr/wEWK/8BFiv/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8AASNE/wE2af8BMmH/AUeJ/wFHif/5uxX/toAa/wFtzf8BR4n/ATZp/wFHif8BI0T/ARYr/wEWK/8BFiv/AR05/wEWK/8BHTn/AR05/wEWK/8BFiv/ARYr/////wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BMmH/ATZp/wFHif8BR4n/+bsV/7aAGv8Oju//AW3N/wE2af8BNmn/ASNE/wEWK/8BFiv/ARYr/wEdOf8BFiv/AR05/wEdOf8BFiv/ARYr/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wABI0T/ATJh/wE2af8Bbc3/AUeJ//m7Ff+2gBr/Do7v/wFtzf8BNmn/ATZp/wEjRP8BFiv/AR05/wEWK/8BHTn/ARYr/wEdOf8BHTn/ARYr/wEWK/8BFiv/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8AASNE/wEyYf8BR4n/AW3N/wFHif/5uxX/toAa/w6O7/8Bbc3/AUeJ/wE2af8BI0T/ARYr/wEdOf8BFiv/AR05/wEdOf8BHTn/AR05/wEWK/8BFiv/ARYr/////wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BMmH/AW3N/w6O7/8Bbc3/+bsV/7aAGv8Oju//AW3N/wFHif8BNmn/ASNE/wEWK/8BHTn/ARYr/wEdOf8BHTn/AR05/wEdOf8BHTn/ARYr/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wABI0T/ATZp/wFtzf8Oju//Do7v/wFtzf8Oju//Do7v/w6O7/8BR4n/ATZp/wEjRP8BFiv/AR05/wEdOf8BHTn/AR05/wEdOf8BHTn/AR05/wEWK/8BFiv/////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8AASNE/wE2af8Oju//Do7v/w6O7/8Bbc3/Do7v/w6O7/8Oju//AW3N/wE2af8BI0T/ARYr/wEdOf8BHTn/AR05/wEdOf8BHTn/AR05/wEdOf8BFiv/ARYr/////wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AAEjRP8BR4n/Do7v/w6O7/8Oju//AW3N/w6O7/8Oju//Do7v/wFtzf8BR4n/ASNE/wEWK/8BHTn/AR05/wEdOf8BHTn/AR05/wEdOf8BHTn/AR05/wEWK/////8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wD///8A////AP///wA=",
					//	"SkinGeometryName": "geometry.humanoid.custom",
					//	"SkinId": "c18e65aa-7b21-4637-9b63-8ad63622ef01_Custom",
					//	"SkinData": "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",
					//	"SkinGeometry": "ew0KICAiZ2VvbWV0cnkuaHVtYW5vaWQiOiB7DQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJib2R5IiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTQuMCwgMTIuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDgsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDE2LCAxNiBdDQogICAgICAgICAgfQ0KICAgICAgICBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogIndhaXN0IiwNCiAgICAgICAgIm5ldmVyUmVuZGVyIjogdHJ1ZSwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDEyLjAsIDAuMCBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImhlYWQiLA0KICAgICAgICAicGl2b3QiOiBbIDAuMCwgMjQuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtNC4wLCAyNC4wLCAtNC4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgOCwgOCwgOCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAwIF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAiaGF0IiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTQuMCwgMjQuMCwgLTQuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDgsIDgsIDggXSwNCiAgICAgICAgICAgICJ1diI6IFsgMzIsIDAgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC41DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiB0cnVlDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0QXJtIiwNCiAgICAgICAgInBpdm90IjogWyAtNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC04LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyA0MCwgMTYgXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInBpdm90IjogWyA1LjAsIDIyLjAsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgNC4wLCAxMi4wLCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgNCwgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtaXJyb3IiOiB0cnVlDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0TGVnIiwNCiAgICAgICAgInBpdm90IjogWyAtMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0zLjksIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAibGVmdExlZyIsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtaXJyb3IiOiB0cnVlDQogICAgICB9DQogICAgXQ0KICB9LA0KDQogICJnZW9tZXRyeS5jYXBlIjogew0KICAgICJ0ZXh0dXJld2lkdGgiOiA2NCwNCiAgICAidGV4dHVyZWhlaWdodCI6IDMyLA0KDQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJjYXBlIiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIC0zLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC01LjAsIDguMCwgLTMuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDEwLCAxNiwgMSBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAwIF0NCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0NCiAgICBdDQogIH0sDQogICJnZW9tZXRyeS5odW1hbm9pZC5jdXN0b206Z2VvbWV0cnkuaHVtYW5vaWQiOiB7DQogICAgImJvbmVzIjogWw0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJoYXQiLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiBmYWxzZSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIiwNCiAgICAgICAgInBpdm90IjogWyAwLjAsIDI0LjAsIDAuMCBdDQogICAgICB9LA0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDUuMCwgMjIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyA0LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAzMiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJyaWdodEFybSIsDQogICAgICAgICJyZXNldCI6IHRydWUsDQogICAgICAgICJwaXZvdCI6IFsgLTUuMCwgMjIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtOC4wLCAxMi4wLCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgNCwgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRJdGVtIiwNCiAgICAgICAgInBpdm90IjogWyAtNiwgMTUsIDEgXSwNCiAgICAgICAgIm5ldmVyUmVuZGVyIjogdHJ1ZSwNCiAgICAgICAgInBhcmVudCI6ICJyaWdodEFybSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAibGVmdFNsZWV2ZSIsDQogICAgICAgICJwaXZvdCI6IFsgNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIDQuMCwgMTIuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDQ4LCA0OCBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogInJpZ2h0U2xlZXZlIiwNCiAgICAgICAgInBpdm90IjogWyAtNS4wLCAyMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC04LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyA0MCwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0TGVnIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMC4xLCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0UGFudHMiLA0KICAgICAgICAicGl2b3QiOiBbIDEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMC4xLCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCA0OCBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAicG9zIjogWyAxLjksIDEyLCAwIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgLTEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMy45LCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAicG9zIjogWyAtMS45LCAxMiwgMCBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImphY2tldCIsDQogICAgICAgICJwaXZvdCI6IFsgMC4wLCAyNC4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC00LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA4LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfQ0KICAgIF0NCiAgfSwNCiAgImdlb21ldHJ5Lmh1bWFub2lkLmN1c3RvbVNsaW06Z2VvbWV0cnkuaHVtYW5vaWQiOiB7DQoNCiAgICAiYm9uZXMiOiBbDQogICAgICB7DQogICAgICAgICJuYW1lIjogImhhdCIsDQogICAgICAgICJuZXZlclJlbmRlciI6IGZhbHNlLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0QXJtIiwNCiAgICAgICAgInJlc2V0IjogdHJ1ZSwNCiAgICAgICAgIm1pcnJvciI6IGZhbHNlLA0KICAgICAgICAicGl2b3QiOiBbIDUuMCwgMjEuNSwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyA0LjAsIDExLjUsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyAzLCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAzMiwgNDggXQ0KICAgICAgICAgIH0NCiAgICAgICAgXQ0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJyaWdodEFybSIsDQogICAgICAgICJyZXNldCI6IHRydWUsDQogICAgICAgICJwaXZvdCI6IFsgLTUuMCwgMjEuNSwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtNy4wLCAxMS41LCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgMywgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDAsIDE2IF0NCiAgICAgICAgICB9DQogICAgICAgIF0NCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgInBpdm90IjogWyAtNiwgMTQuNSwgMSBdLA0KICAgICAgICAibmV2ZXJSZW5kZXIiOiB0cnVlLA0KICAgICAgICAibmFtZSI6ICJyaWdodEl0ZW0iLA0KICAgICAgICAicGFyZW50IjogInJpZ2h0QXJtIg0KICAgICAgfSwNCg0KICAgICAgew0KICAgICAgICAibmFtZSI6ICJsZWZ0U2xlZXZlIiwNCiAgICAgICAgInBpdm90IjogWyA1LjAsIDIxLjUsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgNC4wLCAxMS41LCAtMi4wIF0sDQogICAgICAgICAgICAic2l6ZSI6IFsgMywgMTIsIDQgXSwNCiAgICAgICAgICAgICJ1diI6IFsgNDgsIDQ4IF0sDQogICAgICAgICAgICAiaW5mbGF0ZSI6IDAuMjUNCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRTbGVldmUiLA0KICAgICAgICAicGl2b3QiOiBbIC01LjAsIDIxLjUsIDAuMCBdLA0KICAgICAgICAiY3ViZXMiOiBbDQogICAgICAgICAgew0KICAgICAgICAgICAgIm9yaWdpbiI6IFsgLTcuMCwgMTEuNSwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDMsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDQwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImxlZnRMZWciLA0KICAgICAgICAicmVzZXQiOiB0cnVlLA0KICAgICAgICAibWlycm9yIjogZmFsc2UsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDE2LCA0OCBdDQogICAgICAgICAgfQ0KICAgICAgICBdDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImxlZnRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgMS45LCAxMi4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC0wLjEsIDAuMCwgLTIuMCBdLA0KICAgICAgICAgICAgInNpemUiOiBbIDQsIDEyLCA0IF0sDQogICAgICAgICAgICAidXYiOiBbIDAsIDQ4IF0sDQogICAgICAgICAgICAiaW5mbGF0ZSI6IDAuMjUNCiAgICAgICAgICB9DQogICAgICAgIF0sDQogICAgICAgICJtYXRlcmlhbCI6ICJhbHBoYSINCiAgICAgIH0sDQoNCiAgICAgIHsNCiAgICAgICAgIm5hbWUiOiAicmlnaHRQYW50cyIsDQogICAgICAgICJwaXZvdCI6IFsgLTEuOSwgMTIuMCwgMC4wIF0sDQogICAgICAgICJjdWJlcyI6IFsNCiAgICAgICAgICB7DQogICAgICAgICAgICAib3JpZ2luIjogWyAtMy45LCAwLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA0LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAwLCAzMiBdLA0KICAgICAgICAgICAgImluZmxhdGUiOiAwLjI1DQogICAgICAgICAgfQ0KICAgICAgICBdLA0KICAgICAgICAibWF0ZXJpYWwiOiAiYWxwaGEiDQogICAgICB9LA0KDQogICAgICB7DQogICAgICAgICJuYW1lIjogImphY2tldCIsDQogICAgICAgICJwaXZvdCI6IFsgMC4wLCAyNC4wLCAwLjAgXSwNCiAgICAgICAgImN1YmVzIjogWw0KICAgICAgICAgIHsNCiAgICAgICAgICAgICJvcmlnaW4iOiBbIC00LjAsIDEyLjAsIC0yLjAgXSwNCiAgICAgICAgICAgICJzaXplIjogWyA4LCAxMiwgNCBdLA0KICAgICAgICAgICAgInV2IjogWyAxNiwgMzIgXSwNCiAgICAgICAgICAgICJpbmZsYXRlIjogMC4yNQ0KICAgICAgICAgIH0NCiAgICAgICAgXSwNCiAgICAgICAgIm1hdGVyaWFsIjogImFscGhhIg0KICAgICAgfQ0KICAgIF0NCiAgfQ0KDQp9DQo=",

					try
					{
						//foreach (var x in payload)
						//{
						//	if (((string)x.Name).StartsWith("AnimatedImageData") || x.Name == "PersonaSkin" || x.Name == "PremiumSkin")
						//	{
						//		System.Console.WriteLine("-------------------------- {0}", x.Name);
						//		System.Console.WriteLine(x.Value);
						//	}
						//}

						//-------------------------- ClientRandomId
						//-------------------------- CurrentInputMode
						//-------------------------- DefaultInputMode
						//-------------------------- DeviceModel
						//-------------------------- DeviceOS
						//-------------------------- GameVersion
						//-------------------------- GuiScale
						//-------------------------- LanguageCode
						//-------------------------- PlatformOnlineId
						//-------------------------- ServerAddress
						//-------------------------- UIProfile
						//-------------------------- ThirdPartyName

						//-------------------------- CapeId
						//-------------------------- CapeData
						//-------------------------- CapeImageHeight
						//-------------------------- CapeImageWidth
						//-------------------------- CapeOnClassicSkin

						//-------------------------- SkinId
						//-------------------------- SkinResourcePatch [base64][json] contains GeometryName
						//-------------------------- SkinImageHeight
						//-------------------------- SkinImageWidth
						//-------------------------- SkinData
						//-------------------------- SkinGeometryData
						//-------------------------- PremiumSkin
						//-------------------------- PersonaSkin
						//-------------------------- SkinAnimationData
						//-------------------------- AnimatedImageData

						//-------------------------- DeviceId

						// --------------------------------------------------------------

						// Unused
						//-------------------------- SelfSignedId
						//-------------------------- ThirdPartyNameOnly
						//-------------------------- PlatformOfflineId

						_playerInfo.ClientId = payload.ClientRandomId;
						_playerInfo.CurrentInputMode = payload.CurrentInputMode;
						_playerInfo.DefaultInputMode = payload.DefaultInputMode;
						_playerInfo.DeviceModel = payload.DeviceModel;
						_playerInfo.DeviceOS = payload.DeviceOS;
						_playerInfo.GameVersion = payload.GameVersion;
						_playerInfo.GuiScale = payload.GuiScale;
						_playerInfo.LanguageCode = payload.LanguageCode;
						_playerInfo.PlatformChatId = payload.PlatformOnlineId;
						_playerInfo.ServerAddress = payload.ServerAddress;
						_playerInfo.UIProfile = payload.UIProfile;
						_playerInfo.ThirdPartyName = payload.ThirdPartyName;
						_playerInfo.TenantId = payload.TenantId;
						_playerInfo.DeviceId = payload.DeviceId;

						_playerInfo.Skin = new Skin()
						{
							Cape = new Cape()
							{
								Data = Convert.FromBase64String((string) payload.CapeData ?? string.Empty),
								Id = payload.CapeId,
								ImageHeight = payload.CapeImageHeight,
								ImageWidth = payload.CapeImageWidth,
								OnClassicSkin = payload.CapeOnClassicSkin,
							},
							SkinId = payload.SkinId,
							ResourcePatch = Encoding.UTF8.GetString(Convert.FromBase64String((string) payload.SkinResourcePatch ?? string.Empty)),
							Width = payload.SkinImageWidth,
							Height = payload.SkinImageHeight,
							Data = Convert.FromBase64String((string) payload.SkinData ?? string.Empty),
							GeometryData = Encoding.UTF8.GetString(Convert.FromBase64String((string) payload.SkinGeometryData ?? string.Empty)),
							AnimationData = payload.SkinAnimationData,
							IsPremiumSkin = payload.PremiumSkin,
							IsPersonaSkin = payload.PersonaSkin,
						};
						foreach (dynamic animationData in payload.AnimatedImageData)
						{
							_playerInfo.Skin.Animations.Add(
								new Animation()
								{
									Image = Convert.FromBase64String((string) animationData.Image ?? string.Empty),
									ImageHeight = animationData.ImageHeight,
									ImageWidth = animationData.ImageWidth,
									FrameCount = animationData.Frames,
									Expression = animationData.AnimationExpression,
									Type = animationData.Type,
								}
							);
						}
					}
					catch (Exception e)
					{
						Log.Error("Parsing skin data", e);
					}
				}

				{
					dynamic json = JObject.Parse(certificateChain);

					if (Log.IsDebugEnabled) Log.Debug($"Certificate JSON:\n{json}");

					JArray chain = json.chain;
					//var chainArray = chain.ToArray();

					string validationKey = null;
					string identityPublicKey = null;

					foreach (JToken token in chain)
					{
						IDictionary<string, dynamic> headers = JWT.Headers(token.ToString());

						if (Log.IsDebugEnabled)
						{
							Log.Debug("Raw chain element:\n" + token.ToString());
							Log.Debug($"JWT Header: {string.Join(";", headers)}");

							dynamic jsonPayload = JObject.Parse(JWT.Payload(token.ToString()));
							Log.Debug($"JWT Payload:\n{jsonPayload}");
						}

						// Mojang root x5u cert (string): MHYwEAYHKoZIzj0CAQYFK4EEACIDYgAE8ELkixyLcwlZryUQcu1TvPOmI2B7vX83ndnWRUaXm74wFfa5f/lwQNTfrLVHa2PmenpGI6JhIMUJaWZrjmMj90NoKNFSNBuKdm8rYiXsfaz3K36x/1U26HpG0ZxK/V1V

						if (!headers.ContainsKey("x5u")) continue;

						string x5u = headers["x5u"];

						if (identityPublicKey == null)
						{
							if (CertificateData.MojangRootKey.Equals(x5u, StringComparison.InvariantCultureIgnoreCase))
							{
								Log.Debug("Key is ok, and got Mojang root");
							}
							else if (chain.Count > 1)
							{
								Log.Debug("Got client cert (client root)");
								continue;
							}
							else if (chain.Count == 1)
							{
								Log.Debug("Selfsigned chain");
							}
						}
						else if (identityPublicKey.Equals(x5u))
						{
							Log.Debug("Derived Key is ok");
						}

						ECPublicKeyParameters x5KeyParam = (ECPublicKeyParameters) PublicKeyFactory.CreateKey(x5u.DecodeBase64());
						var signParam = new ECParameters
						{
							Curve = ECCurve.NamedCurves.nistP384,
							Q =
							{
								X = x5KeyParam.Q.AffineXCoord.GetEncoded(),
								Y = x5KeyParam.Q.AffineYCoord.GetEncoded()
							},
						};
						signParam.Validate();

						CertificateData data = JWT.Decode<CertificateData>(token.ToString(), ECDsa.Create(signParam));

						// Validate

						if (data != null)
						{
							identityPublicKey = data.IdentityPublicKey;

							if (Log.IsDebugEnabled) Log.Debug("Decoded token success");

							if (CertificateData.MojangRootKey.Equals(x5u, StringComparison.InvariantCultureIgnoreCase))
							{
								Log.Debug("Got Mojang key. Is valid = " + data.CertificateAuthority);
								validationKey = data.IdentityPublicKey;
							}
							else if (validationKey != null && validationKey.Equals(x5u, StringComparison.InvariantCultureIgnoreCase))
							{
								//TODO: Remove. Just there to be able to join with same XBL multiple times without crashing the server.
								//data.ExtraData.Identity = Guid.NewGuid().ToString();
								_playerInfo.CertificateData = data;
							}
							else
							{
								if (data.ExtraData == null) continue;

								// Self signed, make sure they don't fake XUID
								if (data.ExtraData.Xuid != null)
								{
									Log.Warn("Received fake XUID from " + data.ExtraData.DisplayName);
									data.ExtraData.Xuid = null;
								}

								//TODO: Remove. Just there to be able to join with same XBL multiple times without crashing the server.
								//data.ExtraData.Identity = Guid.NewGuid().ToString();
								_playerInfo.CertificateData = data;
							}
						}
						else
						{
							Log.Error("Not a valid Identity Public Key for decoding");
						}
					}

					//TODO: Implement disconnect here

					{
						_playerInfo.Username = _playerInfo.CertificateData.ExtraData.DisplayName;
						_session.Username = _playerInfo.Username;
						string identity = _playerInfo.CertificateData.ExtraData.Identity;

						if (Log.IsDebugEnabled) Log.Debug($"Connecting user {_playerInfo.Username} with identity={identity} on protocol version={_playerInfo.ProtocolVersion}");
						_playerInfo.ClientUuid = new UUID(identity);

						_bedrockHandler.CryptoContext = new CryptoContext
						{
							UseEncryption = Config.GetProperty("UseEncryptionForAll", false) || (Config.GetProperty("UseEncryption", true) && !string.IsNullOrWhiteSpace(_playerInfo.CertificateData.ExtraData.Xuid)),
						};

						if (_bedrockHandler.CryptoContext.UseEncryption)
						{
							// Use bouncy to parse the DER key
							ECPublicKeyParameters remotePublicKey = (ECPublicKeyParameters)
								PublicKeyFactory.CreateKey(_playerInfo.CertificateData.IdentityPublicKey.DecodeBase64());

							var b64RemotePublicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(remotePublicKey).GetEncoded().EncodeBase64();
							Debug.Assert(_playerInfo.CertificateData.IdentityPublicKey == b64RemotePublicKey);
							Debug.Assert(remotePublicKey.PublicKeyParamSet.Id == "1.3.132.0.34");
							Log.Debug($"{remotePublicKey.PublicKeyParamSet}");

							var generator = new ECKeyPairGenerator("ECDH");
							generator.Init(new ECKeyGenerationParameters(remotePublicKey.PublicKeyParamSet, SecureRandom.GetInstance("SHA256PRNG")));
							var keyPair = generator.GenerateKeyPair();

							ECPublicKeyParameters pubAsyKey = (ECPublicKeyParameters) keyPair.Public;
							ECPrivateKeyParameters privAsyKey = (ECPrivateKeyParameters) keyPair.Private;

							var secretPrepend = Encoding.UTF8.GetBytes("RANDOM SECRET");

							ECDHBasicAgreement agreement = new ECDHBasicAgreement();
							agreement.Init(keyPair.Private);
							byte[] secret;
							using (var sha = SHA256.Create())
							{
								secret = sha.ComputeHash(secretPrepend.Concat(agreement.CalculateAgreement(remotePublicKey).ToByteArrayUnsigned()).ToArray());
							}

							Debug.Assert(secret.Length == 32);

							if (Log.IsDebugEnabled) Log.Debug($"SECRET KEY (b64):\n{secret.EncodeBase64()}");

							var encryptor = new StreamingSicBlockCipher(new SicBlockCipher(new AesEngine()));
							var decryptor = new StreamingSicBlockCipher(new SicBlockCipher(new AesEngine()));
							decryptor.Init(false, new ParametersWithIV(new KeyParameter(secret), secret.Take(12).Concat(new byte[] {0, 0, 0, 2}).ToArray()));
							encryptor.Init(true, new ParametersWithIV(new KeyParameter(secret), secret.Take(12).Concat(new byte[] {0, 0, 0, 2}).ToArray()));

							//IBufferedCipher decryptor = CipherUtilities.GetCipher("AES/CFB8/NoPadding");
							//decryptor.Init(false, new ParametersWithIV(new KeyParameter(secret), secret.Take(16).ToArray()));

							//IBufferedCipher encryptor = CipherUtilities.GetCipher("AES/CFB8/NoPadding");
							//encryptor.Init(true, new ParametersWithIV(new KeyParameter(secret), secret.Take(16).ToArray()));

							_bedrockHandler.CryptoContext.Key = secret;
							_bedrockHandler.CryptoContext.Decryptor = decryptor;
							_bedrockHandler.CryptoContext.Encryptor = encryptor;

							var signParam = new ECParameters
							{
								Curve = ECCurve.NamedCurves.nistP384,
								Q =
								{
									X = pubAsyKey.Q.AffineXCoord.GetEncoded(),
									Y = pubAsyKey.Q.AffineYCoord.GetEncoded()
								}
							};
							signParam.D = CryptoUtils.FixDSize(privAsyKey.D.ToByteArrayUnsigned(), signParam.Q.X.Length);
							signParam.Validate();

							string signedToken = null;
							//if (_session.Server.IsEdu)
							//{
							//	EduTokenManager tokenManager = _session.Server.EduTokenManager;
							//	signedToken = tokenManager.GetSignedToken(_playerInfo.TenantId);
							//}

							var signKey = ECDsa.Create(signParam);
							var b64PublicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubAsyKey).GetEncoded().EncodeBase64();
							var handshakeJson = new HandshakeData
							{
								salt = secretPrepend.EncodeBase64(),
								signedToken = signedToken
							};
							string val = JWT.Encode(handshakeJson, signKey, JwsAlgorithm.ES384, new Dictionary<string, object> {{"x5u", b64PublicKey}});

							Log.Debug($"Headers:\n{string.Join(";", JWT.Headers(val))}");
							Log.Debug($"Return salt:\n{JWT.Payload(val)}");
							Log.Debug($"JWT:\n{val}");


							var response = McpeServerToClientHandshake.CreateObject();
							response.ForceClear = true; // Must be!
							response.token = val;

							_session.SendPacket(response);

							if (Log.IsDebugEnabled) Log.Warn($"Encryption enabled for {_session.Username}");
						}
					}
				}
				if (!_bedrockHandler.CryptoContext.UseEncryption)
				{
					_bedrockHandler.Handler.HandleMcpeClientToServerHandshake(null);
				}
			}
			catch (Exception e)
			{
				Log.Error("Decrypt", e);
			}
		}

		public void HandleMcpeClientToServerHandshake(McpeClientToServerHandshake message)
		{
			Log.Warn($"{(_bedrockHandler.CryptoContext == null ? "C" : $"Encrypted c")}onnection established with {_playerInfo.Username} using MC version {_playerInfo.GameVersion} with protocol version {_playerInfo.ProtocolVersion}");

			IServer server = _serverManager.GetServer();

			IMcpeMessageHandler messageHandler = server.CreatePlayer(_session, _playerInfo);
			_bedrockHandler.Handler = messageHandler; // Replace current message handler with real one.

			if (_playerInfo.ProtocolVersion < McpeProtocolInfo.ProtocolVersion || _playerInfo.ProtocolVersion > 65535)
			{
				Log.Warn($"Wrong version ({_playerInfo.ProtocolVersion}) of Minecraft. Upgrade to join this server.");
				_session.Disconnect($"Wrong version ({_playerInfo.ProtocolVersion}) of Minecraft. Upgrade to join this server.");
				return;
			}

			if (Config.GetProperty("ForceXBLAuthentication", false) && _playerInfo.CertificateData.ExtraData.Xuid == null)
			{
				Log.Warn($"You must authenticate to XBOX Live to join this server.");
				_session.Disconnect(Config.GetProperty("ForceXBLLogin", "You must authenticate to XBOX Live to join this server."));

				return;
			}

			_bedrockHandler.Handler.HandleMcpeClientToServerHandshake(null);
		}

		public void HandleMcpeResourcePackClientResponse(McpeResourcePackClientResponse message)
		{
		}

		public void HandleMcpeText(McpeText message)
		{
		}

		public void HandleMcpeMoveEntity(McpeMoveEntity message)
		{
		}

		public void HandleMcpeMovePlayer(McpeMovePlayer message)
		{
		}

		public void HandleMcpeRiderJump(McpeRiderJump message)
		{
		}

		public void HandleMcpeLevelSoundEvent(McpeLevelSoundEvent message)
		{
		}

		public void HandleMcpeClientCacheStatus(McpeClientCacheStatus message)
		{
		}

		public void HandleMcpeNetworkSettings(McpeNetworkSettings message)
		{
		}

		/// <inheritdoc />
		public void HandleMcpePlayerAuthInput(McpePlayerAuthInput message)
		{
			
		}

		public void HandleMcpeItemStackRequest(McpeItemStackRequest message)
		{
		}

		public void HandleMcpeUpdatePlayerGameType(McpeUpdatePlayerGameType message)
		{
		}

		public void HandleMcpePacketViolationWarning(McpePacketViolationWarning message)
		{
		}

		/// <inheritdoc />
		public void HandleMcpeUpdateSubChunkBlocksPacket(McpeUpdateSubChunkBlocksPacket message)
		{
			
		}

		/// <inheritdoc />
		public void HandleMcpeSubChunkRequestPacket(McpeSubChunkRequestPacket message)
		{
			
		}

		/// <inheritdoc />
		public void HandleMcpeRequestAbility(McpeRequestAbility message)
		{
			
		}

		public void HandleMcpeEntityEvent(McpeEntityEvent message)
		{
		}

		public void HandleMcpeInventoryTransaction(McpeInventoryTransaction message)
		{
		}

		public void HandleMcpeMobEquipment(McpeMobEquipment message)
		{
		}

		public void HandleMcpeMobArmorEquipment(McpeMobArmorEquipment message)
		{
		}

		public void HandleMcpeInteract(McpeInteract message)
		{
		}

		public void HandleMcpeBlockPickRequest(McpeBlockPickRequest message)
		{
		}

		public void HandleMcpeEntityPickRequest(McpeEntityPickRequest message)
		{
		}

		public void HandleMcpePlayerAction(McpePlayerAction message)
		{
		}

		public void HandleMcpeSetEntityData(McpeSetEntityData message)
		{
		}

		public void HandleMcpeSetEntityMotion(McpeSetEntityMotion message)
		{
		}

		public void HandleMcpeAnimate(McpeAnimate message)
		{
		}

		public void HandleMcpeRespawn(McpeRespawn message)
		{
		}

		public void HandleMcpeContainerClose(McpeContainerClose message)
		{
		}

		public void HandleMcpePlayerHotbar(McpePlayerHotbar message)
		{
		}

		public void HandleMcpeInventoryContent(McpeInventoryContent message)
		{
		}

		public void HandleMcpeInventorySlot(McpeInventorySlot message)
		{
		}

		public void HandleMcpeAdventureSettings(McpeAdventureSettings message)
		{
		}

		public void HandleMcpeBlockEntityData(McpeBlockEntityData message)
		{
		}

		public void HandleMcpePlayerInput(McpePlayerInput message)
		{
		}

		public void HandleMcpeSetPlayerGameType(McpeSetPlayerGameType message)
		{
		}

		public void HandleMcpeMapInfoRequest(McpeMapInfoRequest message)
		{
		}

		public void HandleMcpeRequestChunkRadius(McpeRequestChunkRadius message)
		{
		}

		public void HandleMcpeCommandRequest(McpeCommandRequest message)
		{
		}

		public void HandleMcpeCommandBlockUpdate(McpeCommandBlockUpdate message)
		{
		}

		public void HandleMcpeResourcePackChunkRequest(McpeResourcePackChunkRequest message)
		{
		}

		public void HandleMcpePurchaseReceipt(McpePurchaseReceipt message)
		{
		}

		public void HandleMcpePlayerSkin(McpePlayerSkin message)
		{
		}

		public void HandleMcpeNpcRequest(McpeNpcRequest message)
		{
		}

		public void HandleMcpePhotoTransfer(McpePhotoTransfer message)
		{
		}

		public void HandleMcpeModalFormResponse(McpeModalFormResponse message)
		{
		}

		public void HandleMcpeServerSettingsRequest(McpeServerSettingsRequest message)
		{
		}

		public void HandleMcpeLabTable(McpeLabTable messae)
		{
		}

		public void HandleMcpeSetLocalPlayerAsInitialized(McpeSetLocalPlayerAsInitialized message)
		{
		}

		public void HandleMcpeNetworkStackLatency(McpeNetworkStackLatency message)
		{
		}

		public void HandleMcpeScriptCustomEvent(McpeScriptCustomEvent message)
		{
		}

		public void HandleMcpePlayerToggleCrafterSlotRequest(McpePlayerToggleCrafterSlotRequest message)
		{
		}

		public void HandleMcpeSetPlayerInventoryOptions(McpeSetPlayerInventoryOptions message)
		{
		}

		public void HandleMcpeServerPlayerPostMovePosition(McpeServerPlayerPostMovePosition message)
		{
		}

		public void HandleMcpeBossEvent(McpeBossEvent message)
		{
		}

		public void HandleMcpeServerboundLoadingScreen(McpeServerboundLoadingScreen message)
		{
		}

		public void HandleMcpeContainerRegistryCleanup(McpeContainerRegistryCleanup message)
		{
		}
	}

	public interface IServerManager
	{
		IServer GetServer();
	}

	public interface IServer
	{
		IMcpeMessageHandler CreatePlayer(INetworkHandler session, PlayerInfo playerInfo);
	}

	public class PlayerInfo
	{
		public int ADRole { get; set; }
		public CertificateData CertificateData { get; set; }
		public string Username { get; set; }
		public UUID ClientUuid { get; set; }
		public string ServerAddress { get; set; }
		public long ClientId { get; set; }
		public Skin Skin { get; set; }
		public int CurrentInputMode { get; set; }
		public int DefaultInputMode { get; set; }
		public string DeviceModel { get; set; }
		public string GameVersion { get; set; }
		public int DeviceOS { get; set; }
		public string DeviceId { get; set; }
		public int GuiScale { get; set; }
		public int UIProfile { get; set; }
		public int Edition { get; set; }
		public int ProtocolVersion { get; set; }
		public string LanguageCode { get; set; }
		public string PlatformChatId { get; set; }
		public string ThirdPartyName { get; set; }
		public string TenantId { get; set; }
	}

	public class DefaultServerManager : IServerManager
	{
		private readonly MiNetServer _miNetServer;
		private IServer _getServer;

		protected DefaultServerManager()
		{
		}

		public DefaultServerManager(MiNetServer miNetServer)
		{
			_miNetServer = miNetServer;
			_getServer = new DefaultServer(miNetServer);
		}

		public virtual IServer GetServer()
		{
			return _getServer;
		}
	}

	public class DefaultServer : IServer
	{
		private readonly MiNetServer _server;

		protected DefaultServer()
		{
		}

		public DefaultServer(MiNetServer server)
		{
			_server = server;
		}

		public virtual IMcpeMessageHandler CreatePlayer(INetworkHandler session, PlayerInfo playerInfo)
		{
			Player player = _server.PlayerFactory.CreatePlayer(_server, session.GetClientEndPoint(), playerInfo);
			player.NetworkHandler = session;
			player.CertificateData = playerInfo.CertificateData;
			player.Username = playerInfo.Username;
			player.ClientUuid = playerInfo.ClientUuid;
			player.ServerAddress = playerInfo.ServerAddress;
			player.ClientId = playerInfo.ClientId;
			player.Skin = playerInfo.Skin;
			player.PlayerInfo = playerInfo;

			return player;
		}
	}
}