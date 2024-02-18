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
using MiNET.Net;

namespace MiNET
{
	public class AttributeModifier : IPacketDataObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public float Amount { get; set; }
		public int Operations { get; set; }
		public int Operand { get; set; }
		public bool Serializable { get; set; }

		public void Write(Packet packet)
		{
			packet.Write(Id);
			packet.Write(Name);
			packet.Write(Amount);
			packet.Write(Operations); // unknown
			packet.Write(Operand);
			packet.Write(Serializable);
		}

		public static AttributeModifier Read(Packet packet)
		{
			return new AttributeModifier()
			{
				Id = packet.ReadString(),
				Name = packet.ReadString(),
				Amount = packet.ReadFloat(),
				Operations = packet.ReadInt(),
				Operand = packet.ReadInt(),
				Serializable = packet.ReadBool()
			};
		}

		public override string ToString()
		{
			return $"{{Id: {Id}, Name: {Name}, Amount: {Amount}, Operations: {Operations}, Operand: {Operand}, Serializable: {Serializable}}}";
		}
	}

	public class PlayerAttribute : IPacketDataObject
	{
		public string Name { get; set; }
		public float MinValue { get; set; }
		public float MaxValue { get; set; }
		public float Value { get; set; }
		public float Default { get; set; }
		public AttributeModifiers Modifiers { get; set; }
		
		public void Write(Packet packet)
		{
			packet.Write(MinValue);
			packet.Write(MaxValue);
			packet.Write(Value);
			packet.Write(Default); // unknown
			packet.Write(Name);
			packet.Write(Modifiers);
		}

		public static PlayerAttribute Read(Packet packet)
		{
			return new PlayerAttribute()
			{
				MinValue = packet.ReadFloat(),
				MaxValue = packet.ReadFloat(),
				Value = packet.ReadFloat(),
				Default = packet.ReadFloat(),
				Name = packet.ReadString(),
				Modifiers = packet.ReadAttributeModifiers()
			};
		}

		public override string ToString()
		{
			return $"{{Name: {Name}, MinValue: {MinValue}, MaxValue: {MaxValue}, Value: {Value}, Default: {Default}}}";
		}

	}

	public class EntityAttribute : IPacketDataObject
	{
		public string Name { get; set; }
		public float MinValue { get; set; }
		public float MaxValue { get; set; }
		public float Value { get; set; }

		public void Write(Packet packet)
		{
			packet.Write(Name);
			packet.Write(MinValue);
			packet.Write(Value);
			packet.Write(MaxValue);
		}

		public static EntityAttribute Read(Packet packet)
		{
			return new EntityAttribute
			{
				Name = packet.ReadString(),
				MinValue = packet.ReadFloat(),
				Value = packet.ReadFloat(),
				MaxValue = packet.ReadFloat(),
			};
		}

		public override string ToString()
		{
			return $"{{Name: {Name}, MinValue: {MinValue}, MaxValue: {MaxValue}, Value: {Value}}}";
		}
	}


	public enum GameRulesEnum
	{
		CommandblockOutput,
		DoDaylightcycle,
		DoEntitydrops,
		DoFiretick,
		DoMobloot,
		DoMobspawning,
		DoTiledrops,
		DoWeathercycle,
		DrowningDamage,
		Falldamage,
		Firedamage,
		KeepInventory,
		Mobgriefing,
		Pvp,
		ShowCoordinates,
		NaturalRegeneration,
		TntExplodes,
		SendCommandfeedback,
		ExperimentalGameplay,
		// int,
		DoInsomnia,
		CommandblocksEnabled,
		// int,
		DoImmediateRespawn,
		ShowDeathmessages,
		// int,
	}

	public abstract class GameRule : IPacketDataObject
	{
		public string Name { get; }
		public bool IsPlayerModifiable { get; set; } = true;

		protected GameRule(string name)
		{
			Name = name;
		}

		public void Write(Packet packet)
		{
			packet.Write(Name.ToLower());
			packet.Write(IsPlayerModifiable); // bool isPlayerModifiable

			WriteData(packet);
		}

		protected virtual void WriteData(Packet packet) { }

		public static GameRule Read(Packet packet)
		{
			var name = packet.ReadString();
			var isPlayerModifiable = packet.ReadBool();
			var type = packet.ReadUnsignedVarInt();
			return type switch
			{
				1 => GameRule<bool>.ReadData(packet, name, isPlayerModifiable),
				2 => GameRule<int>.ReadData(packet, name, isPlayerModifiable),
				3 => GameRule<float>.ReadData(packet, name, isPlayerModifiable)
			};
		}

		protected bool Equals(GameRule other)
		{
			return string.Equals(Name, other.Name);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((GameRule) obj);
		}

		public override int GetHashCode()
		{
			return Name != null ? Name.GetHashCode() : 0;
		}
	}

	public class GameRule<T> : GameRule
	{
		public T Value { get; set; }

		public GameRule(GameRulesEnum rule, T value) : this(rule.ToString(), value)
		{

		}

		public GameRule(string name, T value) : base(name)
		{
			Value = value;
		}

		protected override void WriteData(Packet packet)
		{
			if (Value is bool)
			{
				packet.WriteUnsignedVarInt(1);
				packet.Write((this as GameRule<bool>).Value);
			}
			else if (Value is int)
			{
				packet.WriteUnsignedVarInt(2);
				packet.WriteVarInt((this as GameRule<int>).Value);
			}
			else if (Value is float)
			{
				packet.WriteUnsignedVarInt(3);
				packet.Write((this as GameRule<float>).Value);
			}
		}

		internal static GameRule ReadData(Packet packet, string name, bool isPlayerModifiable)
		{
			if (typeof(T) == typeof(bool))
			{
				return new GameRule<bool>(name, packet.ReadBool())
				{
					IsPlayerModifiable = isPlayerModifiable
				};
			}
			else if (typeof(T) == typeof(int))
			{
				return new GameRule<int>(name, packet.ReadVarInt())
				{
					IsPlayerModifiable = isPlayerModifiable
				};
			}
			else if (typeof(T) == typeof(float))
			{
				return new GameRule<float>(name, packet.ReadFloat())
				{
					IsPlayerModifiable = isPlayerModifiable
				};
			}
			else
			{
				throw new Exception($"Trying to read a game rule with type that was not written, type={typeof(T)}");
			}
		}
	}
}