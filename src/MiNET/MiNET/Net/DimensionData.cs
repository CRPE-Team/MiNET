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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2022 Niclas Olofsson.
// All Rights Reserved.
#endregion

using System.Collections.Generic;

namespace MiNET.Net
{
	public class DimensionData : IPacketDataObject
	{
		public int MaxHeight { get; set; }
		public int MinHeight { get; set; }
		public int Generator { get; set; }

		public void Write(Packet packet)
		{
			packet.WriteVarInt(MaxHeight);
			packet.WriteVarInt(MinHeight);
			packet.WriteVarInt(Generator);
		}

		public static DimensionData Read(Packet packet)
		{
			return new DimensionData()
			{
				MaxHeight = packet.ReadVarInt(),
				MinHeight = packet.ReadVarInt(),
				Generator = packet.ReadVarInt()
			};
		}
	}

	public class DimensionDefinitions : Dictionary<string, DimensionData>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count);

			foreach (var definition in this)
			{
				packet.Write(definition.Key);
				definition.Value.Write(packet);
			}
		}

		public static DimensionDefinitions Read(Packet packet)
		{
			var definitions = new DimensionDefinitions();

			var count = packet.ReadUnsignedVarInt();
			for (int i = 0; i < count; i++)
			{
				var stringId = packet.ReadString();
				var data = DimensionData.Read(packet);

				definitions.TryAdd(stringId, data);
			}

			return definitions;
		}
	}
}