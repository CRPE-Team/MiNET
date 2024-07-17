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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using MiNET.Net;

namespace MiNET.Utils
{
	public class EnchantOptions : List<EnchantOption>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.WriteLength(Count);
			foreach (var option in this)
			{
				packet.Write(option);
			}
		}

		public static EnchantOptions Read(Packet packet)
		{
			var options = new EnchantOptions();
			var count = packet.ReadLength();

			for (int i = 0; i < count; i++)
			{
				options.Add(EnchantOption.Read(packet));
			}

			return options;
		}
	}

	public class EnchantOption : IPacketDataObject
	{
		public uint Cost { get; set; }
		public int Flags { get; set; }
		public string Name { get; set; }
		public int OptionId { get; set; }

		public Enchants EquipActivatedEnchantments { get; set; } = new Enchants();
		public Enchants HeldActivatedEnchantments { get; set; } = new Enchants();
		public Enchants SelfActivatedEnchantments { get; set; } = new Enchants();

		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt(Cost);
			packet.Write(Flags);
			packet.Write(EquipActivatedEnchantments);
			packet.Write(HeldActivatedEnchantments);
			packet.Write(SelfActivatedEnchantments);
			packet.Write(Name);
			packet.WriteVarInt(OptionId);
		}

		public static EnchantOption Read(Packet packet)
		{
			return new EnchantOption()
			{
				Cost = packet.ReadUnsignedVarInt(),
				Flags = packet.ReadInt(),
				EquipActivatedEnchantments = Enchants.Read(packet),
				HeldActivatedEnchantments = Enchants.Read(packet),
				SelfActivatedEnchantments = Enchants.Read(packet),
				Name = packet.ReadString(),
				OptionId = packet.ReadVarInt()
			};
		}
	}

	public class Enchants : List<Enchant>, IPacketDataObject
	{
		public Enchants() 
		{ 

		}

		public Enchants(List<Enchant> source) : base(source) 
		{ 

		}

		public void Write(Packet packet)
		{
			packet.WriteLength(Count);
			foreach (var enchant in this)
			{
				packet.Write(enchant);
			}
		}

		public static Enchants Read(Packet packet)
		{
			var enchants = new Enchants();
			var count = packet.ReadLength();

			for (int i = 0; i < count; i++)
			{
				enchants.Add(Enchant.Read(packet));
			}

			return enchants;
		}
	}

	public class Enchant : IPacketDataObject
	{
		public byte Id { get; set; }
		public byte Level { get; set; }
		public uint Cost { get; set; }
		public int Weight { get; set; }
		public List<EnchantmentLevel> Levels { get; set; }

		public Enchant(byte id, byte level = 1)
		{
			Id = id;
			Level = level;
			Levels = GetEnchantmentLevels();
			Weight = GetWeight();
		}

		public Enchant(EnchantingType enchanting, byte level = 1) : this((byte) enchanting, level) 
		{ 

		}


		private int GetWeight()
		{
			return (EnchantingType) Id switch
			{
				EnchantingType.Protection => 10,
				EnchantingType.FeatherFalling => 5,
				EnchantingType.FireProtection => 5,
				EnchantingType.ProjectileProtection => 5,
				EnchantingType.AquaAffinity => 2,
				EnchantingType.BlastProtection => 2,
				EnchantingType.Respiration => 2,
				EnchantingType.DepthStrider => 2,
				EnchantingType.Thorns => 1,

				EnchantingType.Sharpness => 10,
				EnchantingType.BaneOfArthropods => 5,
				EnchantingType.Knockback => 5,
				EnchantingType.Smite => 5,
				EnchantingType.FireAspect => 2,
				EnchantingType.Looting => 2,

				EnchantingType.Efficiency => 10,
				EnchantingType.Fortune => 2,
				EnchantingType.SilkTouch => 1,

				EnchantingType.Power => 10,
				EnchantingType.Flame => 2,
				EnchantingType.Punch => 2,
				EnchantingType.Infinity => 1,

				EnchantingType.LuckOfTheSea => 2,
				EnchantingType.Lure => 2,

				EnchantingType.Unbreaking => 5,
				_ => 0
			};
		}

		private List<EnchantmentLevel> GetEnchantmentLevels()
		{
			var enchantmentLevels = new List<EnchantmentLevel>();
			switch ((EnchantingType) Id)
			{
				case EnchantingType.Protection:
					enchantmentLevels.Add(new EnchantmentLevel(1, 12, 1));
					enchantmentLevels.Add(new EnchantmentLevel(12, 23, 2));
					enchantmentLevels.Add(new EnchantmentLevel(23, 34, 3));
					enchantmentLevels.Add(new EnchantmentLevel(34, 45, 4));
					break;
				case EnchantingType.FireProtection:
					enchantmentLevels.Add(new EnchantmentLevel(10, 18, 1));
					enchantmentLevels.Add(new EnchantmentLevel(18, 26, 2));
					enchantmentLevels.Add(new EnchantmentLevel(26, 34, 3));
					enchantmentLevels.Add(new EnchantmentLevel(34, 45, 4));
					break;
				case EnchantingType.FeatherFalling:
					enchantmentLevels.Add(new EnchantmentLevel(5, 11, 1));
					enchantmentLevels.Add(new EnchantmentLevel(11, 17, 2));
					enchantmentLevels.Add(new EnchantmentLevel(17, 23, 3));
					enchantmentLevels.Add(new EnchantmentLevel(23, 29, 4));
					break;
				case EnchantingType.BlastProtection:
					enchantmentLevels.Add(new EnchantmentLevel(5, 13, 1));
					enchantmentLevels.Add(new EnchantmentLevel(13, 21, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 29, 3));
					enchantmentLevels.Add(new EnchantmentLevel(29, 37, 4));
					break;
				case EnchantingType.ProjectileProtection:
					enchantmentLevels.Add(new EnchantmentLevel(3, 9, 1));
					enchantmentLevels.Add(new EnchantmentLevel(9, 15, 2));
					enchantmentLevels.Add(new EnchantmentLevel(15, 21, 3));
					enchantmentLevels.Add(new EnchantmentLevel(21, 27, 4));
					break;
				case EnchantingType.Respiration:
					enchantmentLevels.Add(new EnchantmentLevel(10, 40, 1));
					enchantmentLevels.Add(new EnchantmentLevel(20, 50, 2));
					enchantmentLevels.Add(new EnchantmentLevel(30, 50, 3));
					break;
				case EnchantingType.AquaAffinity:
					enchantmentLevels.Add(new EnchantmentLevel(1, 41, 1));
					break;
				case EnchantingType.Thorns:
					enchantmentLevels.Add(new EnchantmentLevel(10, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(30, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(50, 81, 3));
					break;
				case EnchantingType.DepthStrider:
					enchantmentLevels.Add(new EnchantmentLevel(10, 25, 1));
					enchantmentLevels.Add(new EnchantmentLevel(20, 35, 2));
					enchantmentLevels.Add(new EnchantmentLevel(30, 45, 3));
					break;
				case EnchantingType.Sharpness:
					enchantmentLevels.Add(new EnchantmentLevel(1, 21, 1));
					enchantmentLevels.Add(new EnchantmentLevel(12, 32, 2));
					enchantmentLevels.Add(new EnchantmentLevel(23, 43, 3));
					enchantmentLevels.Add(new EnchantmentLevel(34, 54, 4));
					enchantmentLevels.Add(new EnchantmentLevel(45, 65, 5));
					break;
				case EnchantingType.Smite:
					enchantmentLevels.Add(new EnchantmentLevel(5, 24, 1));
					enchantmentLevels.Add(new EnchantmentLevel(13, 33, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 41, 3));
					enchantmentLevels.Add(new EnchantmentLevel(29, 49, 4));
					enchantmentLevels.Add(new EnchantmentLevel(37, 57, 5));
					break;
				case EnchantingType.BaneOfArthropods:
					enchantmentLevels.Add(new EnchantmentLevel(5, 24, 1));
					enchantmentLevels.Add(new EnchantmentLevel(13, 33, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 41, 3));
					enchantmentLevels.Add(new EnchantmentLevel(29, 49, 4));
					enchantmentLevels.Add(new EnchantmentLevel(37, 57, 5));
					break;
				case EnchantingType.Knockback:
					enchantmentLevels.Add(new EnchantmentLevel(5, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(25, 71, 2));
					break;
				case EnchantingType.FireAspect:
					enchantmentLevels.Add(new EnchantmentLevel(10, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(30, 71, 2));
					break;
				case EnchantingType.Looting:
					enchantmentLevels.Add(new EnchantmentLevel(15, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(24, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(33, 81, 3));
					break;
				case EnchantingType.Power:
					enchantmentLevels.Add(new EnchantmentLevel(1, 16, 1));
					enchantmentLevels.Add(new EnchantmentLevel(11, 26, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 36, 3));
					enchantmentLevels.Add(new EnchantmentLevel(31, 46, 4));
					enchantmentLevels.Add(new EnchantmentLevel(41, 56, 5));
					break;
				case EnchantingType.Punch:
					enchantmentLevels.Add(new EnchantmentLevel(12, 37, 1));
					enchantmentLevels.Add(new EnchantmentLevel(32, 57, 2));
					break;
				case EnchantingType.Flame:
					enchantmentLevels.Add(new EnchantmentLevel(20, 50, 1));
					break;
				case EnchantingType.Infinity:
					enchantmentLevels.Add(new EnchantmentLevel(20, 50, 1));
					break;
				case EnchantingType.Efficiency:
					enchantmentLevels.Add(new EnchantmentLevel(1, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(11, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 81, 3));
					enchantmentLevels.Add(new EnchantmentLevel(31, 91, 4));
					enchantmentLevels.Add(new EnchantmentLevel(41, 101, 5));
					break;
				case EnchantingType.SilkTouch:
					enchantmentLevels.Add(new EnchantmentLevel(15, 61, 1));
					break;
				case EnchantingType.Fortune:
					enchantmentLevels.Add(new EnchantmentLevel(15, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(24, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(33, 81, 3));
					break;
				case EnchantingType.LuckOfTheSea:
					enchantmentLevels.Add(new EnchantmentLevel(15, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(24, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(33, 81, 3));
					break;
				case EnchantingType.Lure:
					enchantmentLevels.Add(new EnchantmentLevel(15, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(24, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(33, 81, 3));
					break;
				case EnchantingType.Unbreaking:
					enchantmentLevels.Add(new EnchantmentLevel(5, 61, 1));
					enchantmentLevels.Add(new EnchantmentLevel(13, 71, 2));
					enchantmentLevels.Add(new EnchantmentLevel(21, 81, 3));
					break;
			}

			return enchantmentLevels;
		}

		public void Write(Packet packet)
		{
			packet.Write(Id);
			packet.Write(Level);
		}

		public static Enchant Read(Packet packet)
		{
			return new Enchant(packet.ReadByte(), packet.ReadByte());
		}
	}

	public class EnchantmentLevel
	{
		public int MinLevel { get; private set; }
		public int MaxLevel { get; private set; }
		public byte Level { get; private set; }

		public EnchantmentLevel(int minLevel, int maxLevel, byte level)
		{
			MinLevel = minLevel;
			MaxLevel = maxLevel;
			Level = level;
		}
	}
}