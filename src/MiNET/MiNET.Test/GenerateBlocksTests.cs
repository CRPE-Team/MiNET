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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Blocks.States;
using MiNET.Items;

namespace MiNET.Test
{
	[TestClass]
	[Ignore("Manual code generation")]
	public class GenerateBlocksTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(GenerateBlocksTests));


		[TestMethod]
		public void GetItemByName()
		{
			foreach (var keyValuePair in ItemFactory.IdToType)
			{
				if (keyValuePair.Key.Equals("minecraft:sapling"))
				{
					Console.WriteLine(keyValuePair.Key);
				}
			}

			Item item = ItemFactory.GetItem("minecraft:oak_sapling");
			Assert.AreEqual("minecraft:oak_sapling", item.Id);
			Assert.IsInstanceOfType(item, typeof(ItemBlock));

			var itemBlock = item as ItemBlock;
			Assert.IsInstanceOfType(itemBlock.Block, typeof(OakSapling));
		}

		[TestMethod]
		public void GenerateItemConstructorsWithNames()
		{
			string fileName = Path.GetTempPath() + "Items_constructors_" + Guid.NewGuid() + ".txt";
			using FileStream file = File.OpenWrite(fileName);
			var writer = new IndentedTextWriter(new StreamWriter(file), "\t");

			writer.Indent += 2;
			writer.WriteLine();

			var itemStates = ItemFactory.ItemStates;
			foreach (var state in itemStates)
			{
				Item item = ItemFactory.GetItem(state.Value.RuntimeId);
				if(!string.IsNullOrEmpty(item.Id)) continue;

				string clazzName = GenerationUtils.CodeName(state.Key.Replace("minecraft:", ""), true);
				string minecraftName = state.Key;


				writer.WriteLine($"public Item{clazzName}() : base(\"{minecraftName}\", {state.Value.RuntimeId})");
			}

			writer.Flush();
		}

		[TestMethod]
		public void GenerateMissingItemsFromItemsStates()
		{
			string fileName = Path.GetTempPath() + "MissingItems_" + Guid.NewGuid() + ".txt";
			using FileStream file = File.OpenWrite(fileName);
			var writer = new IndentedTextWriter(new StreamWriter(file), "\t");

			var itemStates = ItemFactory.ItemStates;
			var newItems = new Dictionary<string, ItemState>();
			foreach (var state in itemStates)
			{
				var item = ItemFactory.GetItem(state.Value.RuntimeId);
				if (item.GetType() == typeof(Item))
				{
					newItems.Add(state.Key, state.Value);
					Console.WriteLine($"New item: {state.Value.RuntimeId}, {state.Key}");
					string clazzName = GenerationUtils.CodeName(state.Key.Replace("minecraft:", ""), true);

					string baseClazz = "Item";
					baseClazz = clazzName.EndsWith("Axe") ? "ItemAxe" : baseClazz;
					baseClazz = clazzName.EndsWith("Shovel") ? "ItemShovel" : baseClazz;
					baseClazz = clazzName.EndsWith("Pickaxe") ? "ItemPickaxe" : baseClazz;
					baseClazz = clazzName.EndsWith("Hoe") ? "ItemHoe" : baseClazz;
					baseClazz = clazzName.EndsWith("Sword") ? "ItemSword" : baseClazz;
					baseClazz = clazzName.EndsWith("Helmet") ? "ArmorHelmetBase" : baseClazz;
					baseClazz = clazzName.EndsWith("Chestplate") ? "ArmorChestplateBase" : baseClazz;
					baseClazz = clazzName.EndsWith("Leggings") ? "ArmorLeggingsBase" : baseClazz;
					baseClazz = clazzName.EndsWith("Boots") ? "ArmorBootsBase" : baseClazz;

					baseClazz = clazzName.EndsWith("Door") ? "ItemWoodenDoor" : baseClazz;

					writer.WriteLine($"public class Item{clazzName} : {baseClazz} {{ public Item{clazzName}() : base({state.Value.RuntimeId}) {{}} }}");
				}
			}
			writer.Flush();

			foreach (var state in newItems.OrderBy(s => s.Value.RuntimeId))
			{
				string clazzName = GenerationUtils.CodeName(state.Key.Replace("minecraft:", ""), true);
				writer.WriteLine($"else if (id == {state.Value.RuntimeId}) item = new Item{clazzName}();");
			}

			writer.Flush();
		}


		[TestMethod]
		public void BlcoksWithBlockstates()
		{
			List<string> blocksWithStates = new List<string>();
			BlockPalette blockPalette = BlockFactory.BlockPalette;
			foreach (var stateContainer in blockPalette)
			{
				if (stateContainer.States.Any())
				{
					if (stateContainer.States.Count(s => s.Name.Contains("direction")) > 0) blocksWithStates.Add(stateContainer.Id);
					if (stateContainer.States.Count(s => s.Name.Contains("face")) > 0) blocksWithStates.Add(stateContainer.Id);
				}
			}

			foreach (string name in blocksWithStates.OrderBy(n => n).Distinct())
			{
				Console.WriteLine($"{name}");
				foreach (var state in BlockFactory.GetBlockById(name).States)
				{
					if (state.Name.Contains("direction")) Console.WriteLine($"\t{state.Name}");
					if (state.Name.Contains("face")) Console.WriteLine($"\t{state.Name}");
				}
			}
		}

		[TestMethod]
		public void GenerateMissingBlocks()
		{
			foreach (var block in BlockFactory.BlockStates)
			{
				var b = BlockFactory.GetBlockById(block.Id);
				if (b == null)
				{
					Console.WriteLine($"Missing {block.Id}");
					continue;
				}


				b.SetStates(block.States);
				//block.RuntimeId
			}
		}

		[TestMethod]
		public void GeneratePartialBlockStatesFromBlockstates()
		{
			var assembly = typeof(Block).Assembly;

			var blockPalette = BlockFactory.BlockPalette;
			var blockStates = blockPalette
				.SelectMany(blockState => blockState.States/*.Where(s => s is not BlockStateByte)*/)
				.DistinctBy(pair => $"{pair.Name}_{pair.GetValue()}")
				.GroupBy(pair => pair.Name)
				.OrderBy(pair => pair.Key)
				.ToArray();

			var fileName = Path.GetTempPath() + "MissingBlockStates_" + Guid.NewGuid() + ".txt";
			using (var file = File.OpenWrite(fileName))
			{
				var writer = new IndentedTextWriter(new StreamWriter(file), "\t");

				Console.WriteLine($"Directory:\n{Path.GetTempPath()}");
				Console.WriteLine($"Filename:\n{fileName}");
				Log.Warn($"Writing blocks to filename:\n{fileName}");

				writer.WriteLine($"namespace MiNET.Blocks.States");
				writer.WriteLine($"{{");
				writer.Indent++;

				foreach (var blockState in blockStates)
				{
					var stateObj = blockState.First();
					var type = stateObj.GetType();

					var name = blockState.Key.Replace("minecraft:", "");
					var className = GenerationUtils.CodeName(name, true);
					var baseName = type.Name;

					if (blockState.Key == "facing_direction" || blockState.Key == "direction")
					{
						className = $"Old{className}";
					}

					var baseClassPart = string.Empty;
					var existingType = assembly.GetType($"MiNET.Blocks.{className}");
					var baseType = assembly.GetType($"MiNET.Blocks.{baseName}");

					var stateTypeName = stateObj switch
					{
						BlockStateInt => "int",
						BlockStateString => "string",
						BlockStateByte => "byte",
						_ => throw new Exception($"Unexpected block state type [{type}]")
					};

					writer.WriteLineNoTabs($"");
					writer.WriteLine($"public partial class {className} : {baseName}");
					writer.WriteLine("{");
					writer.Indent++;

					writer.WriteLine($"public override string Name => \"{blockState.Key}\";");
					writer.WriteLineNoTabs($"");

					//if (type == typeof(BlockStateString))
					//{
					//	writer.WriteLine("public override string Value { get; }");
					//	writer.WriteLineNoTabs($"");
					//}

					if (type == typeof(BlockStateString))
					{
						// constructor
						writer.WriteLine($"protected {className}({stateTypeName} value)");
						writer.WriteLine("{");
						writer.Indent++;
						writer.WriteLine($"Value = value;");
						writer.Indent--;
						writer.WriteLine("}");
						writer.WriteLineNoTabs($"");

						// consts generation
						foreach (var state in blockState)
						{
							var value = state.GetValue().ToString();
							var fieldName = GenerationUtils.CodeName(value, true);

							writer.WriteLine($"protected const string {fieldName}Value = \"{value}\";");
						}

						writer.WriteLineNoTabs($"");

						// fields generation
						foreach (var state in blockState)
						{
							var value = state.GetValue().ToString();
							var fieldName = GenerationUtils.CodeName(value, true);

							writer.WriteLine($"public static readonly {className} {fieldName} = new {className}({fieldName}Value);");
						}

						var values = blockState.Select(s => GenerationUtils.CodeName(s.GetValue().ToString(), true));

						// values
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public static {className}[] Values()");
						writer.WriteLine("{");
						writer.Indent++;
						writer.WriteLine($"return [{string.Join(", ", values)}];");
						writer.Indent--;
						writer.WriteLine("}");
						writer.WriteLineNoTabs($"");
					}
					else
					{

						var values = blockState.Select(s => s.GetValue());

						if (blockState.Key == "direction")
						{
							values = values.Where(v => v is int i && i < 4);
						}

						//writer.WriteLine($"public static {stateTypeName} MinValue {{ get; }} = {values.Min()};");
						writer.WriteLine($"public const {stateTypeName} MaxValue = {values.Max()};");

						// values
						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public static {stateTypeName}[] Values()");
						writer.WriteLine("{");
						writer.Indent++;
						writer.WriteLine($"return [{string.Join(", ", values)}];");
						writer.Indent--;
						writer.WriteLine("}");
						writer.WriteLineNoTabs($"");

						if (type == typeof(BlockStateInt))
						{
							// validator
							writer.WriteLineNoTabs($"");
							writer.WriteLine($"protected override void ValidateValue(int value)");
							writer.WriteLine("{");
							writer.Indent++;
							writer.WriteLine($"if (value < 0 || value > MaxValue)");
							writer.WriteLine("{");
							writer.Indent++;
							writer.WriteLine($"ThrowArgumentException(value);");
							writer.Indent--;
							writer.WriteLine("}");
							writer.Indent--;
							writer.WriteLine("}");
							writer.WriteLineNoTabs($"");
						}
					}

					writer.Indent--;
					writer.WriteLine($"}} // class");
				}

				writer.Indent--;
				writer.WriteLine($"}}");

				writer.Flush();
			}
		}

		[TestMethod]
		public void GeneratePartialBlocksFromBlockstates()
		{
			var assembly = typeof(Block).Assembly;

			var blockPalette = BlockFactory.BlockPalette;
			var blockRecordsGrouping = blockPalette
					.OrderBy(record => record.Id)
					.ThenBy(record => record.Data)
					.GroupBy(record => record.Id)
					.ToArray();

			var idToTag = ItemFactory.ItemTags
				.SelectMany(tag => tag.Value.Select(itemId => (itemId, tag: tag.Key)))
				.GroupBy(tag => tag.itemId)
				.ToDictionary(pairs => pairs.Key, pairs => pairs.Select(pair => pair.tag).ToArray());

			var fileName = Path.GetTempPath() + "MissingBlocks_" + Guid.NewGuid() + ".txt";
			using (var file = File.OpenWrite(fileName))
			{
				var writer = new IndentedTextWriter(new StreamWriter(file), "\t");

				Console.WriteLine($"Directory:\n{Path.GetTempPath()}");
				Console.WriteLine($"Filename:\n{fileName}");
				Log.Warn($"Writing blocks to filename:\n{fileName}");

				writer.WriteLine("using System;");
				writer.WriteLine("using System.Collections.Generic;");
				writer.WriteLineNoTabs($"");

				writer.WriteLine($"namespace MiNET.Blocks");
				writer.WriteLine($"{{");
				writer.Indent++;

				foreach (var blockstateGrouping in blockRecordsGrouping)
				{
					var currentBlockState = blockstateGrouping.First();
					var defaultBlockState = blockstateGrouping.FirstOrDefault(bs => bs.Data == 0);
					if (defaultBlockState == null)
					{
						defaultBlockState = blockstateGrouping.OrderBy(bs => bs.Data).First();
						if (defaultBlockState != null)
						{
							Console.WriteLine($"Unexpected not zero block state data id [{defaultBlockState}]");
						}
					}

					var id = currentBlockState.Id;
					var name = id.Replace("minecraft:", "");
					var className = GenerationUtils.CodeName(name, true);

					var baseName = GetBaseTypeByKnownBlockIds(id, idToTag);

					var baseClassPart = string.Empty;
					var existingType = assembly.GetType($"MiNET.Blocks.{className}");
					var baseType = assembly.GetType($"MiNET.Blocks.{baseName}");
					if (existingType == null
						|| existingType.BaseType.IsAssignableFrom(baseType)
						|| existingType.BaseType == typeof(object)
						|| existingType.BaseType == typeof(Block))
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

					// fields generation
					foreach (var state in currentBlockState.States)
					{
						var typeName = GetFieldTypeName(baseType, id, state);
						var fieldName = $"_{GenerationUtils.CodeName(state.Name, false)}";
						string valuePart;

						switch (state)
						{
							case BlockStateByte blockStateByte:
							{
								valuePart = GetDefaultStateValue<byte>(defaultBlockState, state.Name, 0).ToString();
								break;
							}
							case BlockStateInt blockStateInt:
							{
								valuePart = GetDefaultStateValue(defaultBlockState, state.Name, 0).ToString();
								break;
							}
							case BlockStateString blockStateString:
							{
								valuePart = GetDefaultStateValue(defaultBlockState, state.Name, string.Empty);
								break;
							}
							default:
								throw new ArgumentOutOfRangeException(nameof(state));
						}

						if (state is BlockStateString)
						{
							var valueName = GenerationUtils.CodeName(valuePart, true);

							writer.WriteLine($"private MiNET.Blocks.States.{typeName} {fieldName} = (States.{typeName}) MiNET.Blocks.States.{typeName}.{valueName}.Clone();");
						}
						else
						{
							valuePart = valuePart == "0" ? "" : $" {{ Value = {valuePart} }}";
							writer.WriteLine($"private MiNET.Blocks.States.{typeName} {fieldName} = new MiNET.Blocks.States.{typeName}(){valuePart};");
						}
					}

					if (currentBlockState.States.Any()) writer.WriteLineNoTabs($"");
					writer.WriteLine($"public override string Id => \"{currentBlockState.Id}\";");

					// properties generation
					foreach (var state in currentBlockState.States)
					{
						writer.WriteLineNoTabs($"");

						var q = blockstateGrouping.SelectMany(c => c.States);

						var fieldName = $"_{GenerationUtils.CodeName(state.Name, false)}";
						var typeName = GenerationUtils.CodeName(state.Name, true);
						var propertyName = NormalizePropertyName(typeName);
						var existingProperty = baseType.GetProperty(propertyName);

						// If this is on base, skip this property. We need this to implement common functionality.
						var propertyOverride = baseType != null
											&& baseType != typeof(Block)
											&& existingProperty != null
											&& existingProperty.GetMethod.IsVirtual;
						var propertyOverrideModifierPart = propertyOverride ? $" override" : string.Empty;

						switch (state)
						{
							case BlockStateByte blockStateByte:
							{
								var values = GetStateValues<byte>(q, state.Name);

								if (values.Min() == 0 && values.Max() == 1)
								{
									writer.WriteLine($"[StateBit]");
									writer.WriteLine($"public{propertyOverrideModifierPart} bool {propertyName} {{ get => Convert.ToBoolean({fieldName}.Value); set => NotifyStateUpdate({fieldName}, value); }}");
								}
								else
								{
									writer.WriteLine($"[StateRange({values.Min()}, {values.Max()})]");
									writer.WriteLine($"public{propertyOverrideModifierPart} byte {propertyName} {{ get => {fieldName}.Value; set => NotifyStateUpdate({fieldName}, value); }}");
								}
								break;
							}
							case BlockStateInt blockStateInt:
							{
								var values = GetStateValues<int>(q, state.Name);
								var hasKnownType = propertyOverride && existingProperty.PropertyType.IsAssignableTo(typeof(BlockStateInt));

								if (hasKnownType)
								{
									typeName = existingProperty.PropertyType.FullName;
								}
								else
								{
									var knownTypeName = GetIntStateNameByKnownBlockIds(id, state.Name);
									hasKnownType |= knownTypeName != null;

									typeName = hasKnownType
										? $"MiNET.Blocks.States.{knownTypeName}"
										: typeof(int).FullName;
								}

								if (typeName == typeof(int).FullName)
								{
									typeName = "int";
								}

								writer.WriteLine($"[StateRange({values.Min()}, {values.Max()})]");
								writer.WriteLine($"public{propertyOverrideModifierPart} {typeName} {propertyName} {{ get => {fieldName}{(hasKnownType ? string.Empty : ".Value")}; set => NotifyStateUpdate({fieldName}, value{(hasKnownType ? ".Value" : string.Empty)}); }}");
								break;
							}
							case BlockStateString blockStateString:
							{
								var values = GetStateValues<string>(q, state.Name);

								if (values.Length > 1)
								{
									writer.WriteLine($"[StateEnum({string.Join(", ", values.Select(v => $"\"{v}\""))})]");
								}

								writer.WriteLine($"public{propertyOverrideModifierPart} MiNET.Blocks.States.{typeName} {propertyName} {{ get => {fieldName}; set => NotifyStateUpdate({fieldName}, value.Value); }}");
								break;
							}
							default:
								throw new ArgumentOutOfRangeException(nameof(state));
						}
					}

					if (currentBlockState.States.Any())
					{
						#region SetStates

						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override void {nameof(BlockStateContainer.SetStates)}(IEnumerable<{nameof(IBlockState)}> states)");
						writer.WriteLine($"{{");
						writer.Indent++;
						writer.WriteLine($"foreach (var state in states)");
						writer.WriteLine($"{{");
						writer.Indent++;
						writer.WriteLine($"switch (state)");
						writer.WriteLine($"{{");
						writer.Indent++;

						foreach (var state in currentBlockState.States)
						{
							var stateFieldName = $"_{GenerationUtils.CodeName(state.Name, false)}";

							writer.WriteLine($"case {state.GetType().Name} s when s.Name == {stateFieldName}.Name:");
							writer.Indent++;
							writer.WriteLine($"NotifyStateUpdate({stateFieldName}, s.Value);");
							writer.WriteLine($"break;");
							writer.Indent--;
						}

						writer.Indent--;
						writer.WriteLine($"}} // switch");
						writer.Indent--;
						writer.WriteLine($"}} // foreach");
						writer.Indent--;
						writer.WriteLine($"}} // method");

						#endregion

						#region GetStates

						writer.WriteLineNoTabs($"");
						writer.WriteLine($"protected override IEnumerable<{nameof(IBlockState)}> GetStates()");
						writer.WriteLine($"{{");
						writer.Indent++;

						foreach (var state in currentBlockState.States)
						{
							writer.WriteLine($"yield return _{GenerationUtils.CodeName(state.Name, false)};");
						}

						writer.Indent--;
						writer.WriteLine($"}} // method");

						#endregion

						#region GetHashCode

						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override int GetHashCode()");
						writer.WriteLine($"{{");
						writer.Indent++;
						writer.WriteLine($"return HashCode.Combine(Id, {string.Join(", ", currentBlockState.States.Select(state => $"_{GenerationUtils.CodeName(state.Name, false)}"))});");
						writer.Indent--;
						writer.WriteLine($"}} // method");

						#endregion

						#region Clone

						writer.WriteLineNoTabs($"");
						writer.WriteLine($"public override object Clone()");
						writer.WriteLine($"{{");
						writer.Indent++;
						writer.WriteLine($"var block = ({className}) base.Clone();");
						writer.WriteLineNoTabs($"");

						foreach (var state in currentBlockState.States)
						{
							var fieldName = $"_{GenerationUtils.CodeName(state.Name, false)}";
							var typeName = GetFieldTypeName(baseType, id, state);

							writer.WriteLine($"block.{fieldName} = (MiNET.Blocks.States.{typeName}) {fieldName}.Clone();");
						}

						writer.WriteLineNoTabs($"");
						writer.WriteLine($"return block;");
						writer.Indent--;
						writer.WriteLine($"}} // method");

						#endregion
					}

					writer.Indent--;
					writer.WriteLine($"}} // class");
				}

				writer.Indent--;
				writer.WriteLine($"}}");

				writer.Flush();
			}
		}

		private string NormalizePropertyName(string propertyName)
		{
			switch (propertyName)
			{
				case "CardinalDirection":
					return "Direction";
			}

			return propertyName;
		}

		private string GetFieldTypeName(Type baseType, string id, IBlockState state)
		{
			var typeName = GenerationUtils.CodeName(state.Name, true);

			var existingProperty = baseType.GetProperty(typeName);
			if (existingProperty?.PropertyType.IsAssignableTo(typeof(BlockStateInt)) ?? false && existingProperty.GetMethod.IsVirtual)
			{
				return existingProperty.PropertyType.Name;
			}
			else
			{
				var knownTypeName = GetIntStateNameByKnownBlockIds(id, state.Name);
				if (knownTypeName != null) return knownTypeName;

				if (state.Name == "facing_direction" || state.Name == "direction")
				{
					return $"Old{typeName}";
				}
			}

			return typeName;
		}

		private TStateType GetDefaultStateValue<TStateType>(IBlockStateContainer defaultState, string stateName, TStateType defaultValue)
		{
			return (TStateType) defaultState?.States.First(s => s.Name == stateName).GetValue() ?? defaultValue;
		}

		private TStateType[] GetStateValues<TStateType>(IEnumerable<IBlockState> states, string stateName)
		{
			return states.Where(s => s.Name == stateName).Select(d => (TStateType) d.GetValue()).Distinct().OrderBy(s => s).ToArray();
		}

		private string GetBaseTypeByKnownBlockIds(string id, Dictionary<string, string[]> idToTag)
		{
			var name = id.Replace("minecraft:", "");

			if (idToTag.TryGetValue(id, out var tags))
			{
				foreach (var tag in tags)
				{
					switch (tag.Replace("minecraft:", ""))
					{
						case "logs":
							return nameof(LogBase);
						case "wooden_slabs":
							return nameof(WoodenSlabBase);
						case "wooden_stairs":
							return nameof(WoodenStairsBase);
						case "double_wooden_slabs":
							return nameof(DoubleWoodenSlabBase);
						case "double_plants":
							return nameof(DoublePlantBase);
						case "flowers":
							return nameof(FlowerBase);
					}
				}
			}

			if (name.Contains("double_") && name.Contains("_slab"))
			{
				return nameof(DoubleSlabBase);
			}
			else if (id.Contains("_slab"))
			{
				return nameof(SlabBase);
			}
			
			if (id.EndsWith("_wool"))
			{
				return nameof(WoolBase);
			}
			if (id.EndsWith("_carpet"))
			{
				return nameof(CarpetBase);
			}
			if (name.StartsWith("element_") && name != "element_constructor")
			{
				return nameof(ElementBase);
			}
			if (name.StartsWith("infested"))
			{
				return nameof(InfestedBlockBase);
			}
			if (name.StartsWith("colored_torch_"))
			{
				return nameof(ColoredTorchBase);
			}
			if (id.EndsWith("_concrete"))
			{
				return nameof(ConcreteBase);
			}
			if (id.EndsWith("_concrete_powder"))
			{
				return nameof(ConcretePowderBase);
			}
			if (name.StartsWith("hard_") && id.EndsWith("_stained_glass"))
			{
				return nameof(HardStainedGlassBase);
			}
			if (name.StartsWith("hard_") && id.EndsWith("_stained_glass_pane"))
			{
				return nameof(HardStainedGlassPaneBase);
			}
			if (id.EndsWith("_stained_glass"))
			{
				return nameof(StainedGlassBase);
			}
			if (id.EndsWith("_stained_glass_pane"))
			{
				return nameof(StainedGlassPaneBase);
			}
			if (id.EndsWith("_glazed_terracotta"))
			{
				return nameof(GlazedTerracottaBase);
			}
			if (id.EndsWith("_terracotta"))
			{
				return nameof(TerracottaBase);
			}
			if (id.EndsWith("_planks"))
			{
				return nameof(PlanksBase);
			}
			if (id.EndsWith("_leaves"))
			{
				return nameof(LeavesBase);
			}
			if (id.EndsWith("_sapling"))
			{
				return nameof(SaplingBase);
			}
			if (id.EndsWith("_stairs"))
			{
				return nameof(StairsBase);
			}
			if (name.EndsWith("standing_sign"))
			{
				return nameof(StandingSignBase);
			}
			if (name.EndsWith("_hanging_sign"))
			{
				return nameof(HangingSignBase);
			}
			if (name.EndsWith("_button"))
			{
				return nameof(ButtonBase);
			}
			if (name.EndsWith("_door"))
			{
				return nameof(DoorBase);
			}
			if (name.EndsWith("trapdoor"))
			{
				return nameof(TrapdoorBase);
			}
			if (name.EndsWith("wall_sign"))
			{
				return nameof(WallSignBase);
			}
			if (name.EndsWith("coral_wall_fan"))
			{
				return nameof(CoralWallFanBase);
			}
			if (name.EndsWith("coral_fan"))
			{
				return nameof(CoralFanBase);
			}
			if (name.EndsWith("anvil"))
			{
				return nameof(AnvilBase);
			}
			if (name.EndsWith("fence"))
			{
				return nameof(FenceBase);
			}
			if (name.EndsWith("fence_gate"))
			{
				return nameof(FenceGateBase);
			}
			if (name.EndsWith("_head") || name.EndsWith("_skull"))
			{
				return nameof(HeadBase);
			}

			return nameof(Block);
		}

		private string GetIntStateNameByKnownBlockIds(string id, string stateName)
		{
			var name = id.Replace("minecraft:", "");

			return stateName switch
			{
				"facing_direction" => name switch
				{
					"lightning_rod" or "dropper" or
					"hopper" or "dispenser" or "barrel" or
					"attached_pumpkin_stem" or "pumpkin_stem" or
					"repeating_command_block" or "chain_command_block" or
					"command_block" or
					"attached_melon_stem" or "melon_stem" => nameof(OldFacingDirection1),
					_ when name.EndsWith("fence_gate") => nameof(OldFacingDirection4),

					"end_rod" or "piston" or "sticky_piston" or "piston_head" or
					"piston_arm_collision" or "sticky_piston_arm_collision" => nameof(OldFacingDirection3),

					"ladder" or "frame" or 
					"glow_frame" or "jigsaw" or "wall_banner" => nameof(OldFacingDirection4),
					_ when name.EndsWith("_button") || name.EndsWith("wall_sign") || 
						name.EndsWith("hanging_sign") || name.EndsWith("_glazed_terracotta")
						 || name.EndsWith("_head") || name.EndsWith("_skull") => nameof(OldFacingDirection4),

					_ => null
				},
				"direction" => name switch
				{
					"beehive" or "bee_nest" or "loom" or 
					"grindstone" or "chiseled_bookshelf" or 
					"bed" or "tripwire_hook" or "cocoa" => nameof(OldDirection1),

					"decorated_pot" or "bell" => nameof(OldDirection2),

					_ when name.EndsWith("_door") => nameof(OldDirection3),

					"trapdoor" => nameof(OldDirection2),

					"chalkboard" => nameof(ChalkboardDirection),
					_ => null
				},
				"vine_direction_bits" => nameof(VineDirectionBits),
				_ => null
			};
		}
	}
}