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
using System.Linq;
using System.Text;
using log4net;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;

namespace MiNET
{
	[Flags]
	public enum EnchantmentFlag
	{
		None = 0,
		All = 0xffff,

		Armour = Helmet | Chestplate | Leggings | Boots,
		Helmet = 0x1,
		Chestplate = 0x2,
		Leggings = 0x4,
		Boots = 0x8,
		Sword = 0x10,
		Bow = 0x20,

		ToolOther = Hoe | Shears | FlintAndSteel,
		Hoe = 0x40,
		Shears = 0x80,
		FlintAndSteel = 0x100,

		Dig = Axe | Pickaxe | Shovel,
		Axe = 0x200,
		Pickaxe = 0x400,
		Shovel = 0x800,
		FishingRod = 0x1000,
		CarrotOnAStick = 0x2000,
		Elytra = 0x4000,
		Trident = 0x8000,
	}

	public static class Enchantment
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Enchantment));

		public static void SendEmptyEnchantments(Player player)
		{
			var enchantOptions = McpePlayerEnchantOptions.CreateObject();
			enchantOptions.enchantOptions = new EnchantOptions
			{
				new EnchantOption
				{
					Cost = 0,
					Flags = 0,
					Name = null,
					OptionId = 0,
				},
				new EnchantOption
				{
					Cost = 0,
					Flags = 0,
					Name = null,
					OptionId = 0,
				},
				new EnchantOption
				{
					Cost = 0,
					Flags = 0,
					Name = null,
					OptionId = 0,
				},
			};

			player.SendPacket(enchantOptions);
		}

		public static void SendEnchantments(Player player, Item itemToEnchant)
		{
			if (itemToEnchant.ExtraData != null)
			{
				SendEmptyEnchantments(player);
				return;
			}

			var rnd = new Random();
			var itemType = itemToEnchant.ItemType;

			var noBookshelves = 15;
			var baseLevel = (int) (rnd.Next(1, 9) + Math.Floor(noBookshelves / 2.0) + rnd.Next(0, noBookshelves + 1));
			var topSlotLevel = (int) Math.Max(Math.Floor(baseLevel / 3.0), 1);
			var middleSlotLevel = (int) Math.Floor((baseLevel * 2) / 3.0) + 1;
			var bottomSlotLevel = Math.Max(baseLevel, noBookshelves * 2);
			Log.Warn($"Enchanting values; baseLevel:{baseLevel}, top:{topSlotLevel}, middle:{middleSlotLevel}, bottom:{bottomSlotLevel}");

			// armor
			var armorTypes = new List<ItemType>
			{
				ItemType.Helmet,
				ItemType.Chestplate,
				ItemType.Leggings,
				ItemType.Boots
			};

			var enchantability = itemToEnchant switch
			{
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Leather => 15,
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Chain => 12,
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Iron => 9,
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Gold => 25,
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Diamond => 10,
				{ } item when armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Netherite => 15,

				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Wood => 15,
				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Stone => 5,
				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Iron => 14,
				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Gold => 22,
				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Diamond => 10,
				{ } item when !armorTypes.Contains(item.ItemType) && item.ItemMaterial == ItemMaterial.Netherite => 15,

				_ => 1
			};

			var randomEnchantability = 1 + rnd.Next(0, (int) Math.Floor(enchantability / 4.0) + 1) + rnd.Next(0, (int) Math.Floor(enchantability / 4.0) + 1);
			var randomBonus = 1 + (rnd.NextDouble() + rnd.NextDouble() - 1) * 0.15;
			var finalTopLevel = (int) Math.Round((topSlotLevel + randomEnchantability) * randomBonus);
			var finalMiddleLevel = (int) Math.Round((middleSlotLevel + randomEnchantability) * randomBonus);
			var finalBottomLevel = (int) Math.Round((bottomSlotLevel + randomEnchantability) * randomBonus);

			Log.Warn($"Enchantability: {enchantability}");
			Log.Warn($"Random Bonus: {randomBonus}");
			Log.Warn($"Final: top:{finalTopLevel}, middle:{finalMiddleLevel}, bottom:{finalBottomLevel}");

			var topFinalEnchants = new Enchants();
			var middleFinalEnchants = new Enchants();
			var bottomFinalEnchants = new Enchants();

			var topEnchant = SelectEnchant(GetPossibleEnchantsForItem(itemType, finalTopLevel), rnd);
			topFinalEnchants.Add(topEnchant);
			var middleEnchant = SelectEnchant(GetPossibleEnchantsForItem(itemType, finalMiddleLevel), rnd);
			middleFinalEnchants.Add(middleEnchant);
			var bottomEnchant = SelectEnchant(GetPossibleEnchantsForItem(itemType, finalBottomLevel), rnd);
			bottomFinalEnchants.Add(bottomEnchant);

			AddBonusEnchants(topFinalEnchants, rnd, finalTopLevel, itemType);
			AddBonusEnchants(middleFinalEnchants, rnd, finalMiddleLevel, itemType);
			AddBonusEnchants(bottomFinalEnchants, rnd, finalBottomLevel, itemType);

			if (topFinalEnchants.Count > 1) Log.Warn($"Got {topFinalEnchants.Count} enchants on top");
			if (middleFinalEnchants.Count > 1) Log.Warn($"Got {middleFinalEnchants.Count} enchants in middle");
			if (bottomFinalEnchants.Count > 1) Log.Warn($"Got {bottomFinalEnchants.Count} enchants at bottom");

			// do bonus

			var enchantOptions = McpePlayerEnchantOptions.CreateObject();
			enchantOptions.enchantOptions = new EnchantOptions
			{
				new EnchantOption
				{
					Cost = topEnchant.Cost,
					Name = GetRandomName(rnd),
					OptionId = 1,
					EquipActivatedEnchantments = topFinalEnchants,
				},
				new EnchantOption
				{
					Cost = middleEnchant.Cost,
					Name = GetRandomName(rnd),
					OptionId = 2,
					EquipActivatedEnchantments = middleFinalEnchants,
				},
				new EnchantOption
				{
					Cost = bottomEnchant.Cost,
					Name = GetRandomName(rnd),
					OptionId = 3,
					EquipActivatedEnchantments = bottomFinalEnchants,
				},
			};

			player.SendPacket(enchantOptions);
		}

		private static void AddBonusEnchants(Enchants enchants, Random rnd, int inLevel, ItemType itemType)
		{
			var level = (inLevel + 1) / 2;

			Enchant enchant;
			do
			{
				var possibleEnchantsForItem = GetPossibleEnchantsForItem(itemType, level);
				PurgeConflictingEnchants(enchants, possibleEnchantsForItem);

				enchant = SelectEnchant(possibleEnchantsForItem, rnd);
				if (enchant != null)
				{
					if (enchant.Id == (int) EnchantingType.SilkTouch) Log.Error("Got silk!");

					enchants.Add(enchant);
				}

				level = (level + 1) / 2;
			} while (enchant != null);
		}

		private static void PurgeConflictingEnchants(List<Enchant> enchants, List<Enchant> possibleEnchantsForItem)
		{
			var damage = new List<EnchantingType>()
			{
				EnchantingType.Sharpness,
				EnchantingType.Smite,
				EnchantingType.BaneOfArthropods
			};

			var protection = new List<EnchantingType>()
			{
				EnchantingType.Protection,
				EnchantingType.BlastProtection,
				EnchantingType.FireProtection,
				EnchantingType.ProjectileProtection
			};

			var silkAndFortune = new List<EnchantingType>()
			{
				EnchantingType.SilkTouch,
				EnchantingType.Fortune
			};

			foreach (var enchant in possibleEnchantsForItem.ToList())
			{
				//Every enchantment conflicts with itself. (The player can't get a tool with two copies of the Efficiency enchantment.)
				if (enchants.Any(e => e.Id == enchant.Id))
				{
					possibleEnchantsForItem.Remove(enchant);
				}

				//All damage enchantments (Sharpness, Smite, and Bane of Arthropods) conflict with each other.
				if (damage.Contains((EnchantingType) enchant.Id) && enchants.Any(e => damage.Contains((EnchantingType) e.Id)))
				{
					possibleEnchantsForItem.Remove(enchant);
				}

				//All protection enchantments (Protection, Blast Protection, Fire Protection, Projectile Protection) conflict with each other.
				if (protection.Contains((EnchantingType) enchant.Id) && enchants.Any(e => protection.Contains((EnchantingType) e.Id)))
				{
					possibleEnchantsForItem.Remove(enchant);
				}

				//Silk Touch and Fortune conflict with each other.
				if (silkAndFortune.Contains((EnchantingType) enchant.Id) && enchants.Any(e => silkAndFortune.Contains((EnchantingType) e.Id)))
				{
					possibleEnchantsForItem.Remove(enchant);
				}

				//Depth Strider and Frost Walker conflict with each other.
				//Mending and Infinity conflict with each other.
				//Loyalty and Riptide conflict with each other.
				//Channeling and Riptide conflict with each other.
				//Multishot and Piercing conflict with each other.
			}
		}

		private static Enchant SelectEnchant(Enchants enchants, Random rnd)
		{
			var w = rnd.Next(0, enchants.Sum(e => e.Weight) / 2);
			foreach (var enchant in enchants.OrderBy(e => e.Weight))
			{
				w -= enchant.Weight;
				if (w < 0)
				{
					if (enchant.Id == (int) EnchantingType.SilkTouch) Log.Error("Got silk!");

					return enchant;
				}
			}

			var last = enchants.LastOrDefault();
			if (last != null && last.Id == (int) EnchantingType.SilkTouch) Log.Error("Got silk!");

			return last;
		}

		public static Enchants GetPossibleEnchantsForItem(ItemType itemType, int level)
		{
			var enchants = new Enchants();
			switch (itemType)
			{
				case ItemType.Helmet:
					enchants.Add(new Enchant(EnchantingType.AquaAffinity));
					enchants.Add(new Enchant(EnchantingType.Respiration));
					break;
				case ItemType.Chestplate:
					enchants.Add(new Enchant(EnchantingType.Thorns));
					break;
				case ItemType.Boots:
					enchants.Add(new Enchant(EnchantingType.DepthStrider));
					enchants.Add(new Enchant(EnchantingType.FeatherFalling));
					break;
				case ItemType.Sword:
					enchants.Add(new Enchant(EnchantingType.BaneOfArthropods));
					enchants.Add(new Enchant(EnchantingType.FireAspect));
					enchants.Add(new Enchant(EnchantingType.Knockback));
					enchants.Add(new Enchant(EnchantingType.Looting));
					enchants.Add(new Enchant(EnchantingType.Sharpness));
					enchants.Add(new Enchant(EnchantingType.Smite));
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					break;
				case ItemType.Hoe:
					enchants.Add(new Enchant(EnchantingType.Efficiency));
					enchants.Add(new Enchant(EnchantingType.Fortune));
					enchants.Add(new Enchant(EnchantingType.SilkTouch));
					break;	
				case ItemType.FishingRod:
					enchants.Add(new Enchant(EnchantingType.LuckOfTheSea));
					enchants.Add(new Enchant(EnchantingType.Lure));
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					break;
				case ItemType.Trident:
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					break;
				case ItemType.Book:
					enchants.Add(new Enchant(EnchantingType.AquaAffinity));
					enchants.Add(new Enchant(EnchantingType.Respiration));
					enchants.Add(new Enchant(EnchantingType.Thorns));
					enchants.Add(new Enchant(EnchantingType.DepthStrider));
					enchants.Add(new Enchant(EnchantingType.FeatherFalling));
					enchants.Add(new Enchant(EnchantingType.BaneOfArthropods));
					enchants.Add(new Enchant(EnchantingType.FireAspect));
					enchants.Add(new Enchant(EnchantingType.Knockback));
					enchants.Add(new Enchant(EnchantingType.Looting));
					enchants.Add(new Enchant(EnchantingType.Sharpness));
					enchants.Add(new Enchant(EnchantingType.Smite));
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					enchants.Add(new Enchant(EnchantingType.Efficiency));
					enchants.Add(new Enchant(EnchantingType.Fortune));
					enchants.Add(new Enchant(EnchantingType.SilkTouch));
					enchants.Add(new Enchant(EnchantingType.LuckOfTheSea));
					enchants.Add(new Enchant(EnchantingType.Lure));
					enchants.Add(new Enchant(EnchantingType.Protection));
					enchants.Add(new Enchant(EnchantingType.BlastProtection));
					enchants.Add(new Enchant(EnchantingType.FireProtection));
					enchants.Add(new Enchant(EnchantingType.ProjectileProtection));
					break;
				case ItemType.Axe:
				case ItemType.PickAxe:
				case ItemType.Shovel:
					enchants.Add(new Enchant(EnchantingType.Efficiency));
					enchants.Add(new Enchant(EnchantingType.Fortune));
					enchants.Add(new Enchant(EnchantingType.SilkTouch));
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					break;
				case ItemType.Bow:
				case ItemType.Sheers:
				case ItemType.FlintAndSteel:
				case ItemType.CarrotOnAStick:
				case ItemType.Elytra:
					break;
			}

			switch (itemType)
			{
				case ItemType.Helmet:
				case ItemType.Chestplate:
				case ItemType.Leggings:
				case ItemType.Boots:
					enchants.Add(new Enchant(EnchantingType.Protection));
					enchants.Add(new Enchant(EnchantingType.BlastProtection));
					enchants.Add(new Enchant(EnchantingType.FireProtection));
					enchants.Add(new Enchant(EnchantingType.ProjectileProtection));
					enchants.Add(new Enchant(EnchantingType.Unbreaking));
					break;
			}

			// enchantments
			enchants = new Enchants(enchants.GroupBy(p => p.Id).Select(grp => grp.FirstOrDefault()).Where(e => e != null && e.Levels.Count(l => l.MinLevel <= level && l.MaxLevel > level) > 0).ToList());
			foreach (var enchant in enchants)
			{
				enchant.Level = enchant.Levels.OrderByDescending(l => l.MaxLevel).First(l => l.MinLevel <= level && l.MaxLevel > level).Level;
				enchant.Cost = (uint) enchant.Levels.OrderByDescending(l => l.MaxLevel).First(l => l.MinLevel <= level && l.MaxLevel > level).MinLevel;
			}
			return enchants;
		}

		private static string GetRandomName(Random rnd)
		{
			var chars = new byte[rnd.Next(7, 20)];
			for (int i = 0; i < chars.Length; i++)
			{
				chars[i] = (byte) rnd.Next('a', 'z');
			}

			return Encoding.ASCII.GetString(chars);
		}
	}
}