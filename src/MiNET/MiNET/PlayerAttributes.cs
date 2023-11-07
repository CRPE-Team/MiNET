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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System.Collections.Generic;
using log4net;
using MiNET.Net;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;

namespace MiNET
{
	public class AttributeModifiers : Dictionary<string, AttributeModifier>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var modifier in Values)
			{
				modifier.Write(packet);
			}
		}

		public static AttributeModifiers Read(Packet packet)
		{
			var modifiers = new AttributeModifiers();
			var count = packet.ReadUnsignedVarInt();
			for (int i = 0; i < count; i++)
			{
				var modifier = AttributeModifier.Read(packet);
				modifiers[modifier.Name] = modifier;
			}

			return modifiers;
		}
	}

	public class PlayerAttributes : Dictionary<string, PlayerAttribute>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var attribute in Values)
			{
				attribute.Write(packet);
			}
		}

		public static PlayerAttributes Read(Packet packet)
		{
			var attributes = new PlayerAttributes();
			var count = packet.ReadUnsignedVarInt();
			for (int i = 0; i < count; i++)
			{
				var attribute = PlayerAttribute.Read(packet);
				attributes[attribute.Name] = attribute;
			}

			return attributes;
		}
	}

	public class EntityAttributes : Dictionary<string, EntityAttribute>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var attribute in Values)
			{
				attribute.Write(packet);
			}
		}

		public static EntityAttributes Read(Packet packet)
		{
			var attributes = new EntityAttributes();
			var count = packet.ReadUnsignedVarInt();
			for (int i = 0; i < count; i++)
			{
				var attribute = EntityAttribute.Read(packet);
				attributes[attribute.Name] = attribute;
			}

			return attributes;
		}
	}

	public class EntityLink : IPacketDataObject
	{
		public long FromEntityId { get; set; }

		public long ToEntityId { get; set; }

		public EntityLinkType Type { get; set; }

		public bool Immediate { get; set; }

		public bool CausedByRider { get; set; }

		public EntityLink(long fromEntityId, long toEntityId, EntityLinkType type, bool immediate, bool causedByRider)
		{
			FromEntityId = fromEntityId;
			ToEntityId = toEntityId;
			Type = type;
			Immediate = immediate;
			CausedByRider = causedByRider;
		}
		
		public enum EntityLinkType : byte
		{
			Remove = 0,
			Rider = 1,
			Passenger = 2
		}

		public void Write(Packet packet)
		{
			packet.WriteVarLong(FromEntityId);
			packet.WriteVarLong(ToEntityId);
			packet.Write((byte) Type);
			packet.Write(Immediate);
			packet.Write(CausedByRider);
		}

		public static EntityLink Read(Packet packet)
		{
			var fromEntityId = packet.ReadVarLong();
			var toEntityId = packet.ReadVarLong();
			var type = (EntityLinkType) packet.ReadByte();
			var immediate = packet.ReadBool();
			var causedByRider = packet.ReadBool();

			return new EntityLink(fromEntityId, toEntityId, type, immediate, causedByRider);
		}
	}

	public class EntityLinks : List<EntityLink>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count); // LE
			foreach (var link in this)
			{
				link.Write(packet);
			}
		}

		public static EntityLinks Read(Packet packet)
		{
			var count = packet.ReadUnsignedVarInt();

			var links = new EntityLinks();
			for (int i = 0; i < count; i++)
			{
				links.Add(EntityLink.Read(packet));
			}

			return links;
		}
	}

	public class GameRules : HashSet<GameRule>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteVarInt(Count);
			foreach (var rule in this)
			{
				rule.Write(packet);
			}
		}

		public static GameRules Read(Packet packet)
		{
			var gameRules = new GameRules();

			var count = packet.ReadVarInt();
			for (int i = 0; i < count; i++)
			{
				gameRules.Add(GameRule.Read(packet));
			}

			return gameRules;
		}
	}

	public class Itemstates : Dictionary<string, Itemstate>, IPacketDataObject
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Itemstates));

		public static Itemstates FromJson(string json)
		{
			return JsonConvert.DeserializeObject<Itemstates>(json);
		}

		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var itemstate in this)
			{
				packet.Write(itemstate.Key);
				itemstate.Value.Write(packet);
			}
		}

		public static Itemstates Read(Packet packet)
		{
			var result = new Itemstates();
			var count = packet.ReadUnsignedVarInt();
			for (int runtimeId = 0; runtimeId < count; runtimeId++)
			{
				var name = packet.ReadString();
				var itemstate = Itemstate.Read(packet);

				if (name == "minecraft:shield")
				{
					Log.Warn($"Got shield with runtime id {runtimeId}, legacy {itemstate.RuntimeId}");
				}

				result.Add(name, itemstate);
			}

			return result;
		}
	}

	public class Itemstate : IPacketDataObject
	{
		[JsonProperty("runtime_id")]
		public short RuntimeId { get; set; }

		[JsonProperty("component_based")]
		public bool ComponentBased { get; set; } = false;

		public void Write(Packet packet)
		{
			packet.Write(RuntimeId);
			packet.Write(ComponentBased);
		}

		public static Itemstate Read(Packet packet)
		{
			var legacyId = packet.ReadShort();
			var component = packet.ReadBool();

			return new Itemstate()
			{
				RuntimeId = legacyId,
				ComponentBased = component
			};
		}
	}
}