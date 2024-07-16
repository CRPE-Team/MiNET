using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Items;

namespace MiNET.Test
{
	[TestClass]
	[Ignore("Manual code generation")]
	public class GenerateItemsTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(GenerateItemsTests));

		[TestMethod]
		public void GeneratePartialItemsFromItemStates()
		{
			var assembly = typeof(Item).Assembly;

			var itemStates = ItemFactory.Itemstates;

			var idToTag = ItemFactory.ItemTags
				.SelectMany(tag => tag.Value.Select(itemId => (itemId, tag: tag.Key)))
				.GroupBy(tag => tag.itemId)
				.ToDictionary(pairs => pairs.Key, pairs => pairs.Select(pair => pair.tag).ToArray());

			string fileName = Path.GetTempPath() + "MissingItems_" + Guid.NewGuid() + ".txt";
			using (FileStream file = File.OpenWrite(fileName))
			{
				var writer = new IndentedTextWriter(new StreamWriter(file));

				Console.WriteLine($"Directory:\n{Path.GetTempPath()}");
				Console.WriteLine($"Filename:\n{fileName}");
				Log.Warn($"Writing items to filename:\n{fileName}");

				writer.WriteLine("using MiNET.Blocks;");
				writer.WriteLine();
				writer.WriteLine($"namespace MiNET.Items");
				writer.WriteLine($"{{");
				writer.Indent++;

				foreach (var state in itemStates)
				{
					var id = state.Key;

					if (itemStates.ContainsKey(id.Replace("minecraft:", "minecraft:item.")))
					{
						continue;
					}

					var associatedBlockId = BlockFactory.GetBlockIdFromItemId(id);
					if (associatedBlockId != null && itemStates.ContainsKey(associatedBlockId)) id = associatedBlockId;

					var className = $"Item{GenerationUtils.CodeName(id.Replace("minecraft:", ""), true)}";

					var genInfo = GetGenInfoByKnownItemIds(id, idToTag, associatedBlockId);

					var blockClassName = associatedBlockId == null ? null : GenerationUtils.CodeName(associatedBlockId.Replace("minecraft:", ""), true);
					var baseName = genInfo.BaseName;
					
					var baseClassPart = string.Empty;
					var existingType = assembly.GetType($"MiNET.Items.{className}");
					var baseType = assembly.GetType($"MiNET.Items.{baseName}");
					var blockType = string.IsNullOrEmpty(blockClassName) ? null : assembly.GetType($"MiNET.Blocks.{blockClassName}");
					var baseBlockGenericType = blockType == null ? null : typeof(ItemBlock<>).MakeGenericType(blockType);
					var baseGenericType = baseType.IsGenericType && blockType != null ? baseType.MakeGenericType(blockType) : null;

					if (baseGenericType != null && baseGenericType.IsAssignableTo(baseBlockGenericType))
					{
						baseType = baseGenericType;
						baseName = baseName.Replace("`1", $"<{blockClassName}>");
					}
					else if (baseType == typeof(ItemBlock) && baseBlockGenericType != null)
					{
						baseType = baseBlockGenericType;
						baseName += $"<{blockClassName}>";
					}

					if (existingType == null 
						|| existingType.BaseType == baseType
						|| existingType.BaseType == typeof(object)
						|| existingType.BaseType == typeof(Item)
						|| (existingType.BaseType == typeof(ItemBlock) && (baseType?.IsAssignableTo(typeof(ItemBlock)) ?? false))
						|| (existingType.BaseType.GetGenericTypeDefinition() == typeof(ItemBlock<>) && (baseType?.IsAssignableTo(typeof(ItemBlock)) ?? false)))
					{
						baseClassPart = $" : {baseName}";
					}
					else
					{
						baseType = existingType.BaseType;
					}

					writer.WriteLineNoTabs($"");
					writer.WriteLine($"public partial class {className}{baseClassPart}");
					writer.WriteLine($"{{");
					writer.Indent++;
					
					writer.WriteLine($"public override string {nameof(Item.Id)} {{ get; protected set; }} = \"{id}\";");

					if (!baseType.IsGenericType && blockType != null && baseType.IsAssignableTo(typeof(ItemBlock)) && !string.IsNullOrEmpty(baseClassPart))
					{
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override {nameof(Block)} {nameof(ItemBlock.Block)} {{ get; protected set; }} = new {blockClassName}();");
					}

					if (genInfo.ItemType != null)
					{
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override {nameof(ItemType)} {nameof(Item.ItemType)} {{ get; set; }} = {nameof(ItemType)}.{genInfo.ItemType};");
					}

					if (genInfo.ItemMaterial != null)
					{
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override {nameof(ItemMaterial)} {nameof(Item.ItemMaterial)} {{ get; set; }} = {nameof(ItemMaterial)}.{genInfo.ItemMaterial};");
					}

					if (genInfo.MaxStackSize != 64)
					{
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override int {nameof(Item.MaxStackSize)} {{ get; set; }} = {genInfo.MaxStackSize};");
					}

					writer.Indent--;
					writer.WriteLine($"}}");
				}

				writer.Indent--;
				writer.WriteLine($"}}");

				writer.Flush();
			}
		}

		private GenerationItemInfo GetGenInfoByKnownItemIds(string id, Dictionary<string, string[]> idToTag, string associatedBlockId)
		{
			var name = id.Replace("minecraft:", "");

			if (id.EndsWith("_carpet"))
			{
				return new GenerationItemInfo(typeof(ItemCarpetBase).Name);
			}

			var genInfo = new GenerationItemInfo(associatedBlockId == null ? "Item" : "ItemBlock");

			if (idToTag.TryGetValue(id, out var tags))
			{
				foreach (var tag in tags)
				{
					switch (tag.Replace("minecraft:", ""))
					{
						case "boat":
						case "boats":
							genInfo.BaseName = typeof(ItemBoatBase).Name;
							break;
						case "spawn_egg":
							genInfo.BaseName = typeof(ItemSpawnEggBase).Name;
							break;
						case "bookshelf_books":
							genInfo.ItemType = ItemType.Book;
							break;
						case "door":
							genInfo.BaseName = typeof(ItemDoorBase).Name;
							break;
						case "is_axe":
							genInfo.BaseName = typeof(ItemAxeBase).Name;
							genInfo.ItemType = ItemType.Axe;
							break;
						case "is_hoe":
							genInfo.BaseName = typeof(ItemHoeBase).Name;
							genInfo.ItemType = ItemType.Hoe;
							break;
						case "is_pickaxe":
							genInfo.BaseName = typeof(ItemPickaxeBase).Name;
							genInfo.ItemType = ItemType.PickAxe;
							break;
						case "is_shovel":
							genInfo.BaseName = typeof(ItemShovelBase).Name;
							genInfo.ItemType = ItemType.Shovel;
							break;
						case "is_sword":
							genInfo.BaseName = typeof(ItemSwordBase).Name;
							genInfo.ItemType = ItemType.Sword;
							break;
						case "is_armor":
							genInfo.MaxStackSize = 1;
							genInfo.BaseName = Enum.Parse<ItemType>(name.Split('_').Last(), true) switch
							{
								ItemType.Helmet => typeof(ItemArmorHelmetBase).Name,
								ItemType.Chestplate => typeof(ItemArmorChestplateBase).Name,
								ItemType.Leggings => typeof(ItemArmorLeggingsBase).Name,
								ItemType.Boots => typeof(ItemArmorBootsBase).Name,
								_ => genInfo.BaseName
							};
							break;
						case "horse_armor":
							genInfo.BaseName = typeof(ItemHorseArmorBase).Name;
							genInfo.MaxStackSize = 1;
							genInfo.ItemMaterial = id switch
							{
								"minecraft:diamond_horse_armor" => ItemMaterial.Diamond,
								"minecraft:golden_horse_armor" => ItemMaterial.Gold,
								"minecraft:iron_horse_armor" => ItemMaterial.Iron,
								"minecraft:leather_horse_armor" => ItemMaterial.Leather,
							};
							break;

						case "diamond_tier":
							genInfo.ItemMaterial = ItemMaterial.Diamond;
							break;
						case "golden_tier":
							genInfo.ItemMaterial = ItemMaterial.Gold;
							break;
						case "leather_tier":
							genInfo.ItemMaterial = ItemMaterial.Leather;
							break;
						case "stone_tier":
							genInfo.ItemMaterial = ItemMaterial.Stone;
							break;
						case "chainmail_tier":
							genInfo.ItemMaterial = ItemMaterial.Chain;
							break;
						case "iron_tier":
							genInfo.ItemMaterial = ItemMaterial.Iron;
							break;
						case "netherite_tier":
							genInfo.ItemMaterial = ItemMaterial.Netherite;
							break;
						case "wooden_tier":
							genInfo.ItemMaterial = ItemMaterial.Wood;
							break;
						case "is_tool":
							genInfo.MaxStackSize = 1;
							break;
						case "sign":
							genInfo.BaseName = typeof(ItemSignBase).Name;
							break;
						case "is_food":
							genInfo.BaseName = typeof(FoodItemBase).Name;
							break;
					}

					if (genInfo.ItemType == null)
					{
						if (Enum.TryParse<ItemType>(name.Split('_').Last(), true, out var itemType))
						{
							genInfo.ItemType = itemType;
						}
					}
				}
			}

			return genInfo;
		}

		private class GenerationItemInfo
		{
			public GenerationItemInfo(string baseName)
			{
				BaseName = baseName;
			}

			public int MaxStackSize { get; set; } = 64;

			public ItemType? ItemType { get; set; }

			public ItemMaterial? ItemMaterial { get; set; }

			public string BaseName { get; set; }
		}
	}
}
