#region LICENSE

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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using MiNET.Net;
using MiNET.Utils.Skins;
using MiNET.Utils.Vectors;

namespace MiNET.Utils
{
	public class Records : List<BlockCoordinates>
	{
		public Records()
		{
		}
		public Records(IEnumerable<BlockCoordinates> coordinates) : base(coordinates)
		{
		}
	}

	public class PlayerRecord
	{
		public UUID ClientUuid { get; set; }

		public long EntityId { get; set; }

		public string Username { get; set; }

		public string Xuid { get; set; }

		public string PlatformChatId { get; set; }

		public int DeviceOS { get; set; }

		public Skin Skin { get; set; }

		public bool IsTeacher { get; set; } = false;

		public bool IsHost { get; set; } = false;
	}

	public abstract class PlayerRecords : List<PlayerRecord>, IPacketDataObject
	{
		public abstract PlayerRecordType Type { get; }

		public PlayerRecords()
		{

		}

		public PlayerRecords(IEnumerable<PlayerRecord> records) : base(records)
		{

		}

		public void Write(Packet packet)
		{
			packet.Write((byte) Type);
			packet.WriteUnsignedVarInt((uint) Count);

			WriteData(packet);
		}

		protected virtual void WriteData(Packet packet) { }

		public static PlayerRecords Read(Packet packet)
		{
			var type = (PlayerRecordType) packet.ReadByte();

			return type switch
			{
				PlayerRecordType.Add => PlayerAddRecords.ReadData(packet),
				PlayerRecordType.Remove => PlayerRemoveRecords.ReadData(packet),
				_ => throw new Exception($"Unknown player record type [{type}]")
			};
		}
	}

	public class PlayerAddRecords : PlayerRecords
	{
		public override PlayerRecordType Type => PlayerRecordType.Add;

		public PlayerAddRecords()
		{

		}

		public PlayerAddRecords(Player player) : this(new Player[] { player })
		{

		}

		public PlayerAddRecords(IEnumerable<PlayerRecord> records) : base(records)
		{

		}

		public PlayerAddRecords(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				Add(new PlayerRecord()
				{
					ClientUuid = player.ClientUuid,
					EntityId = player.EntityId,
					Username = player.DisplayName ?? player.Username,
					Xuid = player.CertificateData?.ExtraData?.Xuid ?? null,
					PlatformChatId = player.PlayerInfo.PlatformChatId,
					DeviceOS = player.PlayerInfo.DeviceOS,
					Skin = player.Skin,
					IsTeacher = false,
					IsHost = false
				});
			}
		}

		protected override void WriteData(Packet packet)
		{
			foreach (var record in this)
			{
				packet.Write(record.ClientUuid);
				packet.WriteEntityId(record.EntityId);
				packet.Write(record.Username);
				packet.Write(record.Xuid);
				packet.Write(record.PlatformChatId);
				packet.Write(record.DeviceOS);
				packet.Write(record.Skin);
				packet.Write(record.IsTeacher);
				packet.Write(record.IsHost);
			}

			foreach (var record in this)
			{
				packet.Write(record.Skin.IsVerified);
			}
		}

		internal static PlayerRecords ReadData(Packet packet)
		{
			var records = new PlayerAddRecords();

			var count = packet.ReadUnsignedVarInt();
			for (var i = 0; i < count; i++)
			{
				records.Add(ReadRecord(packet));
			}

			foreach (var record in records)
			{
				record.Skin.IsVerified = packet.ReadBool();
			}

			return records;
		}

		private static PlayerRecord ReadRecord(Packet packet)
		{
			return new PlayerRecord()
			{
				ClientUuid = packet.ReadUUID(),
				EntityId = packet.ReadEntityId(),
				Username = packet.ReadString(),
				Xuid = packet.ReadString(),
				PlatformChatId = packet.ReadString(),
				DeviceOS = packet.ReadInt(),
				Skin = packet.ReadSkin(),
				IsTeacher = packet.ReadBool(),
				IsHost = packet.ReadBool()
			};
		}
	}

	public class PlayerRemoveRecords : PlayerRecords
	{
		public override PlayerRecordType Type => PlayerRecordType.Remove;

		public PlayerRemoveRecords()
		{

		}

		public PlayerRemoveRecords(IEnumerable<PlayerRecord> records) : base(records)
		{

		}

		public PlayerRemoveRecords(Player player) : this(new Player[] { player })
		{

		}

		public PlayerRemoveRecords(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				Add(new PlayerRecord()
				{
					ClientUuid = player.ClientUuid
				});
			}
		}

		protected override void WriteData(Packet packet)
		{
			foreach (var record in this)
			{
				packet.Write(record.ClientUuid);
			}
		}

		internal static PlayerRecords ReadData(Packet packet)
		{
			var records = new PlayerRemoveRecords();

			var count = packet.ReadUnsignedVarInt();
			for (var i = 0; i < count; i++)
			{
				records.Add(ReadRecord(packet));
			}

			return records;
		}

		private static PlayerRecord ReadRecord(Packet packet)
		{
			return new PlayerRecord()
			{
				ClientUuid = packet.ReadUUID()
			};
		}
	}
}