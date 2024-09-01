using System;
using System.Collections.Generic;
using System.Linq;
using fNbt;
using log4net;
using MiNET.BlockEntities;
using MiNET.Blocks;
using MiNET.Utils;

namespace MiNET.Worlds.Anvil
{
	public class AnvilPaletteConverter
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(AnvilPaletteConverter));

		private const string AnvilIncompatibleBitName = "anvil_incompatible_bit";

		private static readonly AnvilToBedrockStateMapper _mapper = new AnvilToBedrockStateMapper();

		private static readonly Dictionary<string, string> _anvilBedrockBiomesMap = new Dictionary<string, string>
		{
			{ "old_growth_pine_taiga", "mega_taiga" },
			{ "old_growth_spruce_taiga", "redwood_taiga_mutated" },
			{ "stony_shore", "stone_beach" },
			{ "snowy_beach", "cold_beach" },
			{ "snowy_taiga", "cold_taiga" },
		};

		private static readonly HashSet<string> _seaBlocks = new HashSet<string>
		{
			"minecraft:seagrass",
			"minecraft:tall_seagrass",
			"minecraft:bubble_column",
			"minecraft:kelp"
		};

		private static readonly string[] _woodList =
		[
			"oak",
			"spruce",
			"birch",
			"jungle",
			"acacia",
			"dark_oak",
			"mangrove",
			"cherry",
			"bamboo",
			"crimson",
			"warped",
			"azalea"
		];

		private static readonly string[] _colorsList =
		[
			"white",
			"orange",
			"magenta",
			"light_blue",
			"yellow",
			"lime",
			"pink",
			"gray",
			"light_gray",
			"cyan",
			"purple",
			"blue",
			"brown",
			"green",
			"red",
			"black"
		];

		private static readonly string[] _infestedStoneList =
		[
			"stone",
			"cobblestone",
			"stone_brick",
			"mossy_stone_brick",
			"cracked_stone_brick",
			"chiseled_stone_brick"
		];

		private static readonly string[] _skullsList =
		[
			"skeleton_skull",
			"wither_skeleton_skull",
			"zombie_head",
			"player_head",
			"creeper_head",
			"dragon_head",
			"piglin_head",
			"skeleton_wall_skull",
			"wither_skeleton_wall_skull",
			"zombie_wall_head",
			"player_wall_head",
			"creeper_wall_head",
			"dragon_wall_head",
			"piglin_wall_head"
		];

		private static readonly string[] _copperList =
		[
			"copper",
			"exposed_copper",
			"weathered_copper",
			"oxidized_copper",
			"waxed_copper",
			"waxed_exposed_copper",
			"waxed_weathered_copper",
			"waxed_oxidized_copper"
		];

		private static readonly string[] _slabMaterialsList = new[]
		{
			"bamboo_mosaic",
			"stone",
			"granite",
			"polished_granite",
			"diorite",
			"polished_diorite",
			"andesite",
			"polished_andesite",
			"cobblestone",
			"mossy_cobblestone",
			"stone_brick",
			"mossy_stone_brick",
			"brick",
			"end_brick",
			"end_stone_brick",
			"nether_brick",
			"red_nether_brick",
			"sandstone",
			"smooth_stone",
			"smooth_sandstone",
			"red_sandstone",
			"cut_sandstone",
			"cut_red_sandstone",
			"smooth_red_sandstone",
			"quartz",
			"smooth_quartz",
			"purpur",
			"prismarine",
			"prismarine_brick",
			"dark_prismarine",
			"blackstone",
			"polished_blackstone",
			"polished_blackstone_brick",
			"cut_copper",
			"exposed_cut_copper",
			"weathered_cut_copper",
			"oxidized_cut_copper",
			"waxed_cut_copper",
			"waxed_exposed_cut_copper",
			"waxed_weathered_cut_copper",
			"waxed_oxidized_cut_copper",
			"cobbled_deepslate",
			"polished_deepslate",
			"deepslate_brick",
			"deepslate_tile",
			"mud_brick",
			"tuff",
			"polished_tuff",
			"tuff_brick",
			"petrified_oak"
		}
			.Concat(_woodList)
			.ToArray();

		private static readonly string[] _doorMaterialsList = new[]
		{
			"iron"
		}
			.Concat(_copperList)
			.Concat(_woodList)
			.ToArray();

		private static readonly string[] _pottedPlantsList = new[]
		{
			"dandelion",
			"poppy",
			"blue_orchid",
			"allium",
			"azure_bluet",
			"red_tulip",
			"orange_tulip",
			"white_tulip",
			"pink_tulip",
			"oxeye_daisy",
			"cornflower",
			"lily_of_the_valley",
			"wither_rose",
			"oak_sapling",
			"spruce_sapling",
			"birch_sapling",
			"jungle_sapling",
			"acacia_sapling",
			"cherry_sapling",
			"dark_oak_sapling",
			"red_mushroom",
			"brown_mushroom",
			"fern",
			"dead_bush",
			"cactus",
			"bamboo",
			"azalea_bush",
			"flowering_azalea_bush",
			"crimson_fungus",
			"warped_fungus",
			"crimson_roots",
			"warped_roots",
			"mangrove_propagule",
			"torchflower"
		};

		static AnvilPaletteConverter()
		{
			var poweredSkipMap = 
				new SkipPropertyStateMapper("powered");
			var powerSkipMap =
				new SkipPropertyStateMapper("power");
			var faceDirectionSkipMap = new BlockStateMapper(
				new SkipPropertyStateMapper("down"),
				new SkipPropertyStateMapper("up"),
				new SkipPropertyStateMapper("north"),
				new SkipPropertyStateMapper("south"),
				new SkipPropertyStateMapper("west"),
				new SkipPropertyStateMapper("east"));
			var upperBlockBitMap = new PropertyStateMapper("half", "upper_block_bit",
					new PropertyValueStateMapper("upper", "true"),
					new PropertyValueStateMapper("lower", "false"));
			var upsideDownBitMap = new PropertyStateMapper("half", "upside_down_bit",
					new PropertyValueStateMapper("top", "1"),
					new PropertyValueStateMapper("bottom", "0"));
			var oldFacingDirectionMap = new PropertyStateMapper("facing", "facing_direction",
					new PropertyValueStateMapper("down", "0"),
					new PropertyValueStateMapper("up", "1"),
					new PropertyValueStateMapper("north", "2"),
					new PropertyValueStateMapper("south", "3"),
					new PropertyValueStateMapper("east", "4"),
					new PropertyValueStateMapper("west", "5"));
			var oldFacingDirectionMap2 = new PropertyStateMapper("facing", "facing_direction",
					new PropertyValueStateMapper("down", "0"),
					new PropertyValueStateMapper("up", "1"),
					new PropertyValueStateMapper("south", "2"),
					new PropertyValueStateMapper("north", "3"),
					new PropertyValueStateMapper("west", "4"),
					new PropertyValueStateMapper("east", "5"));
			var oldFacingDirectionMap3 = new PropertyStateMapper("facing", "facing_direction",
					new PropertyValueStateMapper("down", "0"),
					new PropertyValueStateMapper("up", "1"),
					new PropertyValueStateMapper("south", "2"),
					new PropertyValueStateMapper("north", "3"),
					new PropertyValueStateMapper("east", "4"),
					new PropertyValueStateMapper("west", "5"));
			var oldFacingDirectionMap4 = new PropertyStateMapper("facing", "facing_direction",
					new PropertyValueStateMapper("down", "0"),
					new PropertyValueStateMapper("up", "1"),
					new PropertyValueStateMapper("north", "2"),
					new PropertyValueStateMapper("south", "3"),
					new PropertyValueStateMapper("west", "4"),
					new PropertyValueStateMapper("east", "5"));
			var directionMap = new PropertyStateMapper("facing", "direction",
					new PropertyValueStateMapper("south", "0"),
					new PropertyValueStateMapper("west", "1"),
					new PropertyValueStateMapper("north", "2"),
					new PropertyValueStateMapper("east", "3"));
			var directionMap2 = new PropertyStateMapper("facing", "direction",
					new PropertyValueStateMapper("north", "0"),
					new PropertyValueStateMapper("east", "1"),
					new PropertyValueStateMapper("south", "2"),
					new PropertyValueStateMapper("west", "3"));
			var cardinalDirectionMap = new PropertyStateMapper("facing", "minecraft:cardinal_direction");
			var facingDirectionMap = new PropertyStateMapper("facing", "minecraft:facing_direction");
			var blockFaceMap = new PropertyStateMapper("facing", "minecraft:block_face");
			var hatchMap = new PropertyStateMapper("hatch", "cracked_state",
					new PropertyValueStateMapper("0", "no_cracks"),
					new PropertyValueStateMapper("1", "cracked"),
					new PropertyValueStateMapper("2", "max_cracked"));
			var attachmentMap = new PropertyStateMapper("attachment", "attachment",
				new PropertyValueStateMapper("floor", "standing"),
				new PropertyValueStateMapper("ceiling", "hanging"),
				new PropertyValueStateMapper("wall", "side"),
				new PropertyValueStateMapper("single_wall", "side"),
				new PropertyValueStateMapper("double_wall", "multiple"));
			var faceAttachmentMap = attachmentMap.Clone();
			faceAttachmentMap.AnvilName = "face";
			var ageGrowthMap = new PropertyStateMapper("age",
					(_, _, property) => new NbtString("growth", (int.Parse(property.Value) * 2 + 1).ToString()));

			var multiFaceDirectonMap = new BlockStateMapper(
				context =>
				{
					var faceDirection = 0;

					faceDirection |= context.Properties["down"].StringValue == "true" ? 1 << 0 : 0;
					faceDirection |= context.Properties["up"].StringValue == "true" ? 1 << 1 : 0;
					faceDirection |= context.Properties["south"].StringValue == "true" ? 1 << 2 : 0;
					faceDirection |= context.Properties["west"].StringValue == "true" ? 1 << 3 : 0;
					faceDirection |= context.Properties["north"].StringValue == "true" ? 1 << 4 : 0;
					faceDirection |= context.Properties["east"].StringValue == "true" ? 1 << 5 : 0;

					if (faceDirection == 0) faceDirection = 0x3F;

					context.Properties.Clear();
					context.Properties.Add(new NbtString("multi_face_direction_bits", faceDirection.ToString()));
				});
			var litMap = new BlockStateMapper(
				context =>
				{
					var litName = context.Properties["lit"].StringValue == "true" ? context.AnvilName.Replace("minecraft:", "minecraft:lit_") : context.AnvilName;
					context.Properties.Remove("lit");

					return litName;
				});

			_mapper.AddDefault(new BlockStateMapper(cardinalDirectionMap));
			_mapper.AddDefault(new BlockStateMapper(upperBlockBitMap));
			_mapper.AddDefault(new BlockStateMapper(
				new PropertyStateMapper("axis", "pillar_axis")));
			_mapper.AddDefault(new BlockStateMapper(
				new SkipPropertyStateMapper("waterlogged"),
				new SkipPropertyStateMapper("snowy"),
				poweredSkipMap));

			_mapper.Add(new BlockStateMapper("minecraft:magma_block", "minecraft:magma"));
			_mapper.Add(new BlockStateMapper("minecraft:cobweb", "minecraft:web"));
			_mapper.Add(new BlockStateMapper("minecraft:cave_air", "minecraft:air"));
			_mapper.Add(new BlockStateMapper("minecraft:void_air", "minecraft:air"));
			_mapper.Add(new BlockStateMapper("minecraft:spawner", "minecraft:mob_spawner"));
			_mapper.Add(new BlockStateMapper("minecraft:dirt_path", "minecraft:grass_path"));
			_mapper.Add(new BlockStateMapper("minecraft:rooted_dirt", "minecraft:dirt_with_roots"));
			_mapper.Add(new BlockStateMapper("minecraft:snow_block", "minecraft:snow"));
			_mapper.Add(new BlockStateMapper("minecraft:sugar_cane", "minecraft:reeds"));
			_mapper.Add(new BlockStateMapper("minecraft:bricks", "minecraft:brick_block"));
			_mapper.Add(new BlockStateMapper("minecraft:dead_bush", "minecraft:deadbush"));
			_mapper.Add(new BlockStateMapper("minecraft:terracotta", "minecraft:hardened_clay"));
			_mapper.Add(new BlockStateMapper("minecraft:lily_pad", "minecraft:waterlily"));
			_mapper.Add(new BlockStateMapper("minecraft:nether_bricks", "minecraft:nether_brick"));
			_mapper.Add(new BlockStateMapper("minecraft:red_nether_bricks", "minecraft:red_nether_brick"));
			_mapper.Add(new BlockStateMapper("minecraft:slime_block", "minecraft:slime"));
			_mapper.Add(new BlockStateMapper("minecraft:melon", "minecraft:melon_block"));
			_mapper.Add(new BlockStateMapper("minecraft:nether_quartz_ore", "minecraft:quartz_ore"));
			_mapper.Add(new BlockStateMapper("minecraft:end_stone_bricks", "minecraft:end_bricks"));
			_mapper.Add(new BlockStateMapper("minecraft:stonecutter", "minecraft:stonecutter_block"));
			_mapper.Add(new BlockStateMapper("minecraft:azalea_bush", "minecraft:azalea"));
			_mapper.Add(new BlockStateMapper("minecraft:flowering_azalea_bush", "minecraft:flowering_azalea"));
			_mapper.Add(new BlockStateMapper("minecraft:frogspawn", "minecraft:frog_spawn"));
			_mapper.Add(new BlockStateMapper("minecraft:waxed_copper_block", "minecraft:waxed_copper"));

			_mapper.Add(new BlockStateMapper("minecraft:nether_portal", "minecraft:portal",
				new PropertyStateMapper("axis", "portal_axis")));

			_mapper.Add(new BlockStateMapper("minecraft:purpur_pillar", "minecraft:purpur_block",
				new AdditionalPropertyStateMapper("chisel_type", "lines")));

			_mapper.Add(new BlockStateMapper("minecraft:note_block", "minecraft:noteblock",
				new SkipPropertyStateMapper("instrument"),
				new SkipPropertyStateMapper("note"),
				poweredSkipMap));

			_mapper.Add(new BlockStateMapper("minecraft:jukebox",
				new SkipPropertyStateMapper("has_record")));

			_mapper.Add(new BlockStateMapper("minecraft:bubble_column",
				new PropertyStateMapper("drag", "drag_down")));

			_mapper.Add(new BlockStateMapper("minecraft:tnt",
				new PropertyStateMapper("unstable", "explode_bit")));

			_mapper.Add(new BlockStateMapper("minecraft:jack_o_lantern", "minecraft:lit_pumpkin"));

			_mapper.Add(new BlockStateMapper("minecraft:sculk_shrieker",
				new PropertyStateMapper("shrieking", "active")));

			_mapper.Add(new BlockStateMapper("minecraft:composter",
				new PropertyStateMapper("level", "composter_fill_level")));

			_mapper.Add(new BlockStateMapper("minecraft:twisting_vines_plant", "minecraft:twisting_vines",
				new AdditionalPropertyStateMapper("twisting_vines_age", "25")));
			_mapper.Add(new BlockStateMapper("minecraft:twisting_vines",
				new PropertyStateMapper("age", "twisting_vines_age")));

			_mapper.Add(new BlockStateMapper("minecraft:weeping_vines_plant", "minecraft:weeping_vines",
				new AdditionalPropertyStateMapper("weeping_vines_age", "25")));
			_mapper.Add(new BlockStateMapper("minecraft:weeping_vines",
				new PropertyStateMapper("age", "weeping_vines_age")));

			_mapper.Add(new BlockStateMapper("minecraft:crafter",
				new PropertyStateMapper("orientation",
					new PropertyValueStateMapper("up_north", "up_south"),
					new PropertyValueStateMapper("up_south", "up_north"),
					new PropertyValueStateMapper("up_east", "up_west"),
					new PropertyValueStateMapper("up_west", "up_east")),
				new BitPropertyStateMapper("triggered")));

			_mapper.Add(new BlockStateMapper("minecraft:snow", "minecraft:snow_layer",
				new PropertyStateMapper("layers",
					(_, _, property) => new NbtString("height", (int.Parse(property.Value) - 1).ToString()))));

			_mapper.Add(new BlockStateMapper("minecraft:small_dripleaf", "minecraft:small_dripleaf_block"));

			var suspiciousMap = new BlockStateMapper(
				new PropertyStateMapper("dusted", "brushed_progress"));
			_mapper.Add("minecraft:suspicious_sand", suspiciousMap);
			_mapper.Add("minecraft:suspicious_gravel", suspiciousMap);

			_mapper.Add(new BlockStateMapper("minecraft:farmland",
				new PropertyStateMapper("moisture", "moisturized_amount")));

			_mapper.Add(new BlockStateMapper("minecraft:sponge", new AdditionalPropertyStateMapper("sponge_type", "dry")));
			_mapper.Add(new BlockStateMapper("minecraft:wet_sponge", "minecraft:sponge", new AdditionalPropertyStateMapper("sponge_type", "wet")));

			_mapper.Add(new BlockStateMapper("minecraft:mangrove_propagule",
				new PropertyStateMapper("hanging"),
				new PropertyStateMapper("age", "propagule_stage"),
				new SkipPropertyStateMapper("stage")));

			_mapper.Add(new BlockStateMapper("minecraft:pointed_dripstone",
				new PropertyStateMapper("thickness", "dripstone_thickness",
					new PropertyValueStateMapper("tip_merge", "merge")),
				new PropertyStateMapper("vertical_direction", "hanging",
					new PropertyValueStateMapper("up", "false"),
					new PropertyValueStateMapper("down", "true"))));

			_mapper.Add(new BlockStateMapper("minecraft:pitcher_plant"));
			_mapper.Add(new BlockStateMapper("minecraft:pitcher_crop",
				new PropertyStateMapper("age", "growth")));

			_mapper.Add(new BlockStateMapper("minecraft:torchflower_crop",
                new SkipPropertyStateMapper("age")));

			_mapper.Add(new BlockStateMapper("minecraft:sniffer_egg", hatchMap));
			_mapper.Add(new BlockStateMapper("minecraft:turtle_egg",
                new PropertyStateMapper("eggs", "turtle_egg_count",
					new PropertyValueStateMapper("1", "one_egg"),
					new PropertyValueStateMapper("2", "two_egg"),
					new PropertyValueStateMapper("3", "three_egg"),
					new PropertyValueStateMapper("4", "four_egg")),
				hatchMap));

			_mapper.Add(new BlockStateMapper("minecraft:bamboo",
                new PropertyStateMapper("age", "bamboo_stalk_thickness",
					new PropertyValueStateMapper("0", "thin"),
					new PropertyValueStateMapper("1", "thick")),
				new PropertyStateMapper("leaves", "bamboo_leaf_size",
					new PropertyValueStateMapper("none", "no_leaves"),
					new PropertyValueStateMapper("small", "small_leaves"),
					new PropertyValueStateMapper("large", "large_leaves")),
				new PropertyStateMapper("stage", "age_bit",
					new PropertyValueStateMapper("0", "false"),
					new PropertyValueStateMapper("1", "true"))));

			_mapper.Add("minecraft:chorus_plant", faceDirectionSkipMap);

			_mapper.Add("minecraft:lectern", new BlockStateMapper(
				new BitPropertyStateMapper("powered"),
				new SkipPropertyStateMapper("has_book")));

			_mapper.Add(new BlockStateMapper("minecraft:target", powerSkipMap));

			_mapper.Add("minecraft:furnace", litMap);
			_mapper.Add("minecraft:blast_furnace", litMap);
			_mapper.Add("minecraft:smoker", litMap);
			_mapper.Add("minecraft:deepslate_redstone_ore", litMap);
			_mapper.Add("minecraft:redstone_ore", litMap);
			_mapper.Add("minecraft:redstone_lamp", litMap);

			_mapper.Add(new BlockStateMapper("minecraft:scaffolding",
				new PropertyStateMapper("distance", "stability"),
				new PropertyStateMapper("bottom", "stability_check")));

			_mapper.Add(new BlockStateMapper("minecraft:end_portal_frame",
				new PropertyStateMapper("eye", "end_portal_eye_bit")));

			_mapper.Add(new BlockStateMapper("minecraft:structure_block",
				new PropertyStateMapper("mode", "structure_block_type")));

			_mapper.Add(new BlockStateMapper("minecraft:vault"));

			_mapper.Add(new BlockStateMapper("minecraft:ender_chest"));
			_mapper.Add(new BlockStateMapper("minecraft:chest",
				new SkipPropertyStateMapper("type")));
			_mapper.Add(new BlockStateMapper("minecraft:trapped_chest",
				new SkipPropertyStateMapper("type")));

			_mapper.Add(new BlockStateMapper("minecraft:respawn_anchor",
				new PropertyStateMapper("charges", "respawn_anchor_charge")));

			var lightBlockMap = new BlockStateMapper(context =>
			{
				var lightLevel = context.Properties["level"].StringValue;
				context.Properties.Clear();
				return $"minecraft:light_block_{lightLevel}";
			});
			_mapper.Add("minecraft:light", lightBlockMap);

			#region Facing

			_mapper.Add(new BlockStateMapper("minecraft:beehive", directionMap));
			_mapper.Add(new BlockStateMapper("minecraft:bee_nest", directionMap));
			_mapper.Add(new BlockStateMapper("minecraft:loom", directionMap));
			_mapper.Add(new BlockStateMapper("minecraft:decorated_pot",
				directionMap2,
				new SkipPropertyStateMapper("cracked")));


			_mapper.Add("minecraft:glow_lichen", multiFaceDirectonMap);
			_mapper.Add("minecraft:sculk_vein", multiFaceDirectonMap);


			_mapper.Add(new BlockStateMapper("minecraft:bell",
				attachmentMap,
				directionMap2,
				new PropertyStateMapper("powered", "toggle_bit")));

			_mapper.Add(new BlockStateMapper("minecraft:grindstone", 
				faceAttachmentMap, 
				directionMap));

			_mapper.Add(new BlockStateMapper("minecraft:small_amethyst_bud", blockFaceMap));
			_mapper.Add(new BlockStateMapper("minecraft:medium_amethyst_bud", blockFaceMap));
			_mapper.Add(new BlockStateMapper("minecraft:large_amethyst_bud", blockFaceMap));
			_mapper.Add(new BlockStateMapper("minecraft:amethyst_cluster", blockFaceMap));

			_mapper.Add(new BlockStateMapper("minecraft:ladder", oldFacingDirectionMap));
			_mapper.Add(new BlockStateMapper("minecraft:lightning_rod", oldFacingDirectionMap));
			_mapper.Add(new BlockStateMapper("minecraft:dropper",
				oldFacingDirectionMap, 
				new BitPropertyStateMapper("triggered")));
			_mapper.Add(new BlockStateMapper("minecraft:hopper",
				oldFacingDirectionMap,
				new PropertyStateMapper("enabled", "toggle_bit",
					new PropertyValueStateMapper("true", "false"),
					new PropertyValueStateMapper("false", "true"))));
			_mapper.Add(new BlockStateMapper("minecraft:dispenser",
				oldFacingDirectionMap,
				new BitPropertyStateMapper("triggered")));
			_mapper.Add(new BlockStateMapper("minecraft:barrel",
				oldFacingDirectionMap,
				new BitPropertyStateMapper("open")));

			_mapper.Add(new BlockStateMapper("minecraft:observer",
				facingDirectionMap,
				new BitPropertyStateMapper("powered")));

			_mapper.Add("sniffer_egg", new BlockStateMapper(
				new PropertyStateMapper("hatch", "cracked_state",
					new PropertyValueStateMapper("0", "no_cracks"),
					new PropertyValueStateMapper("1", "cracked"),
					new PropertyValueStateMapper("2", "max_cracked"))));

			_mapper.Add("minecraft:fire", faceDirectionSkipMap);
			_mapper.Add("minecraft:iron_bars", faceDirectionSkipMap);
			_mapper.Add("minecraft:glass_pane", faceDirectionSkipMap);
			_mapper.Add("minecraft:nether_brick_fence", faceDirectionSkipMap);
			foreach (var wood in _woodList)
			{
				_mapper.Add($"minecraft:{wood}_fence", faceDirectionSkipMap);
			}

			foreach (var copper in _copperList)
			{
				_mapper.Add(new BlockStateMapper($"minecraft:{copper}_bulb", 
					new BitPropertyStateMapper("powered")));
			}

			var bigDripleafMap = new BlockStateMapper("minecraft:big_dripleaf",
				new AdditionalPropertyStateMapper("big_dripleaf_head", (name, _) => name == "minecraft:big_dripleaf" ? "true" : "false"),
				new PropertyStateMapper("tilt", "big_dripleaf_tilt",
					new PropertyValueStateMapper("partial", "partial_tilt"),
					new PropertyValueStateMapper("full", "full_tilt")));

			_mapper.Add(bigDripleafMap);
			_mapper.Add("minecraft:big_dripleaf_stem", bigDripleafMap);

			var fenceGateMap = new BlockStateMapper(
				directionMap,
				new BitPropertyStateMapper("in_wall"),
				new BitPropertyStateMapper("open"));

			var oakFenceGateMap = fenceGateMap.Clone();
			oakFenceGateMap.BedrockName = "minecraft:fence_gate";
			_mapper.Add("minecraft:oak_fence_gate", oakFenceGateMap);

			foreach (var wood in _woodList)
			{
				_mapper.TryAdd($"minecraft:{wood}_fence_gate", fenceGateMap);
			}

			_mapper.Add(new BlockStateMapper("minecraft:cocoa", directionMap));

			_mapper.Add(new BlockStateMapper("minecraft:brewing_stand",
				new PropertyStateMapper("has_bottle_0", "brewing_stand_slot_a_bit"),
				new PropertyStateMapper("has_bottle_1", "brewing_stand_slot_b_bit"),
				new PropertyStateMapper("has_bottle_2", "brewing_stand_slot_c_bit")));
			
			var commandBlockMap = new BlockStateMapper(
				oldFacingDirectionMap,
				new BitPropertyStateMapper("conditional"));
			_mapper.Add("minecraft:command_block", commandBlockMap);
			_mapper.Add("minecraft:chain_command_block", commandBlockMap);
			_mapper.Add("minecraft:repeating_command_block", commandBlockMap);

			_mapper.Add(new BlockStateMapper("minecraft:end_rod", oldFacingDirectionMap3));

			#endregion

			#region Colored

			_mapper.Add(new BlockStateMapper($"minecraft:light_gray_glazed_terracotta", "minecraft:silver_glazed_terracotta", oldFacingDirectionMap));
			foreach (var color in _colorsList)
			{
				_mapper.TryAdd(new BlockStateMapper($"minecraft:{color}_glazed_terracotta", oldFacingDirectionMap));
			}

			_mapper.Add(new BlockStateMapper($"minecraft:shulker_box", "minecraft:undyed_shulker_box",
					context => context.Properties.Clear()));
			foreach (var color in _colorsList)
			{
				_mapper.TryAdd(new BlockStateMapper($"minecraft:{color}_shulker_box",
					new SkipPropertyStateMapper("facing")));
			}


			for (var i = 0; i < _colorsList.Length; i++)
			{
				var color = _colorsList[i];
				var blockEntity = new BedBlockEntity() { Color = (byte) i };

				_mapper.Add(new BlockStateMapper($"minecraft:{color}_bed", "minecraft:bed",
					context => context.BlockEntityTemplate = blockEntity,
					new PropertyStateMapper("part", "head_piece_bit",
						new PropertyValueStateMapper("foot", "false"),
						new PropertyValueStateMapper("head", "true")),
					directionMap,
					new BitPropertyStateMapper("occupied")));
			}

			for (var i = 0; i < _colorsList.Length; i++)
			{
				var color = _colorsList[i];
				var blockEntity = new BannerBlockEntity() { Base = 15 - i };

				var banerMap = new BlockStateMapper(context =>
				{
					var name = context.AnvilName.Replace("minecraft:", "");

					context.BlockEntityTemplate = blockEntity;

					return context.AnvilName.Contains("_wall_banner") ? "minecraft:wall_banner" : "minecraft:standing_banner";
				},
					oldFacingDirectionMap,
					new PropertyStateMapper("rotation", "ground_sign_direction"));

				_mapper.Add($"minecraft:{color}_banner", banerMap);
				_mapper.Add($"minecraft:{color}_wall_banner", banerMap);
			}

			foreach (var color in _colorsList)
			{
				_mapper.TryAdd(new BlockStateMapper($"minecraft:{color}_stained_glass"));
				_mapper.TryAdd(new BlockStateMapper($"minecraft:{color}_stained_glass_pane",
					context => context.Properties.Clear()));
			}

			var candlesMap = new PropertyStateMapper("candles", (_, _, property) => new NbtString("candles", (int.Parse(property.StringValue) - 1).ToString()));
			_mapper.Add(new BlockStateMapper($"minecraft:candle", candlesMap));
			foreach (var color in _colorsList)
			{
				_mapper.Add(new BlockStateMapper($"minecraft:{color}_candle", candlesMap));
			}

			#endregion

			#region Liquid

			var liquidMap = new BlockStateMapper(
				new PropertyStateMapper("level", "liquid_depth"),
				new SkipPropertyStateMapper("falling"));

			_mapper.Add("minecraft:water", liquidMap);
			_mapper.Add("minecraft:lava", liquidMap);
			_mapper.Add("minecraft:flowing_water", liquidMap);
			_mapper.Add("minecraft:flowing_lava", liquidMap);

			#endregion

			#region Leaves and Saplings

			var leavesMap = new BlockStateMapper(
				new BitPropertyStateMapper("persistent"),
				new AdditionalPropertyStateMapper("update_bit", (_, nbt) => nbt["persistent_bit"].StringValue == "true" ? "false" : "true"),
				new SkipPropertyStateMapper("distance"));

			foreach (var wood in _woodList)
			{
				_mapper.Add($"minecraft:{wood}_leaves", leavesMap);
			}

			var floweredAzaleaLeavesMap = leavesMap.Clone();
			floweredAzaleaLeavesMap.BedrockName = "minecraft:azalea_leaves_flowered";
			_mapper.Add("minecraft:flowering_azalea_leaves", floweredAzaleaLeavesMap);

			var saplingsMap = new BlockStateMapper(
				new PropertyStateMapper("stage", "age_bit",
					new PropertyValueStateMapper("0", "false"),
					new PropertyValueStateMapper("1", "true")));

			foreach (var wood in _woodList)
			{
				_mapper.Add($"minecraft:{wood}_sapling", saplingsMap);
			}

			#endregion

			#region Signs

			var signMap = new BlockStateMapper(
				context =>
				{
					var name = context.AnvilName.Replace("minecraft:", "");

					name = name.Replace("dark_oak", "darkoak");

					if (name.Replace("_sign", "").Split('_').Length == 1)
					{
						name = name.Replace("_sign", "_standing_sign");
					}

					if (!name.Contains("darkoak"))
					{
						name = name.Replace("oak_", "");
					}

					return $"minecraft:{name}";
				},
				oldFacingDirectionMap4,
				new PropertyStateMapper("rotation", "ground_sign_direction"));


			var hangingSignMap = new BlockStateMapper(
				context =>
				{
					var name = context.AnvilName.Replace("minecraft:", "");
					context.Properties.Add(new NbtString("hanging", "true"));

					if (context.Properties["attached"].StringValue != "true")
					{
						var rotation = int.Parse(context.Properties["rotation"].StringValue);

						if (rotation % 4 == 0)
						{
							context.Properties.Remove("rotation");

							var direction = rotation switch
							{
								12 => "east",
								8 => "north",
								4 => "west",
								_ => "south"
							};

							context.Properties.Add(new NbtString("facing", direction));
						}
						else
						{
							context.Properties["attached"] = new NbtString("attached", "true");
						}
					}

					return context.AnvilName;
				},
				oldFacingDirectionMap4,
				new BitPropertyStateMapper("attached"),
				new PropertyStateMapper("rotation", "ground_sign_direction"));

			foreach (var wood in _woodList)
			{
				_mapper.Add($"minecraft:{wood}_sign", signMap);
				_mapper.Add($"minecraft:{wood}_wall_sign", signMap);
				_mapper.Add($"minecraft:{wood}_hanging_sign", hangingSignMap);
				_mapper.Add(new BlockStateMapper($"minecraft:{wood}_wall_hanging_sign", $"minecraft:{wood}_hanging_sign", oldFacingDirectionMap4));
			}

			#endregion

			#region Doors and Trapdoors

			var trapdoorMap = new BlockStateMapper(
				upsideDownBitMap,
				new PropertyStateMapper("facing", "direction",
					new PropertyValueStateMapper("east", "0"),
					new PropertyValueStateMapper("west", "1"),
					new PropertyValueStateMapper("south", "2"),
					new PropertyValueStateMapper("north", "3")),
				new PropertyStateMapper("open", "open_bit"));

			var oakTrapdoorMap = trapdoorMap.Clone();
			oakTrapdoorMap.BedrockName = "minecraft:trapdoor";
			_mapper.Add($"minecraft:oak_trapdoor", oakTrapdoorMap);
			foreach (var material in _doorMaterialsList)
			{
				_mapper.TryAdd($"minecraft:{material}_trapdoor", trapdoorMap);
			}

			var doorFacingDirectionMap = new PropertyStateMapper("facing", "direction",
					new PropertyValueStateMapper("east", "0"),
					new PropertyValueStateMapper("south", "1"),
					new PropertyValueStateMapper("west", "2"),
					new PropertyValueStateMapper("north", "3"));

			var doorMap = new BlockStateMapper(
				doorFacingDirectionMap,
				new PropertyStateMapper("open", "open_bit"),
				new PropertyStateMapper("hinge",
					(name, properties, _) => new NbtString("door_hinge_bit", (int.Parse(properties["direction"]?.StringValue ?? doorFacingDirectionMap.Resolve(name, properties, properties["facing"] as NbtString).Value) % 2).ToString())));

			var oakDoorMap = doorMap.Clone();
			oakDoorMap.BedrockName = "minecraft:wooden_door";
			_mapper.Add($"minecraft:oak_door", oakDoorMap);
			foreach (var material in _doorMaterialsList)
			{
				_mapper.TryAdd($"minecraft:{material}_door", doorMap);
			}

			var buttonMap = new BlockStateMapper(
				context =>
				{
					var face = context.Properties["face"].StringValue;
					var facing = context.Properties["facing"].StringValue;

					var direction = (face, facing) switch
					{
						("ceiling", _) => "0",
						("floor", _) => "1",
						("wall", "north") => "2",
						("wall", "south") => "3",
						("wall", "west") => "4",
						("wall", "east") => "5",
						_ => "0"
					};

					context.Properties.Add(new NbtString("facing_direction", direction));
				},
				new PropertyStateMapper("powered", "button_pressed_bit"),
				new SkipPropertyStateMapper("face"),
				new SkipPropertyStateMapper("facing"));

			_mapper.TryAdd($"minecraft:stone_button", buttonMap);
			_mapper.TryAdd($"minecraft:polished_blackstone_button", buttonMap);
			var oakButtonMap = buttonMap.Clone();
			oakButtonMap.BedrockName = "minecraft:wooden_button";
			_mapper.TryAdd($"minecraft:oak_button", oakButtonMap);
			foreach (var wood in _woodList)
				_mapper.TryAdd($"minecraft:{wood}_button", buttonMap);

			#endregion

			#region Torch

			var torchMap = new BlockStateMapper(
				context =>
				{
					if (context.AnvilName.Contains("wall_"))
					{
						context.AnvilName = context.AnvilName.Replace("wall_", "");
					}
					else
					{
						context.Properties["facing"] = new NbtString("facing", "top");
					}

					return context.Properties["lit"]?.StringValue == "false" ? $"minecraft:unlit_{context.AnvilName.Replace("minecraft:", "")}" : context.AnvilName;
				},
				new PropertyStateMapper("facing", "torch_facing_direction",
					new PropertyValueStateMapper("west", "east"),
					new PropertyValueStateMapper("east", "west"),
					new PropertyValueStateMapper("north", "south"),
					new PropertyValueStateMapper("south", "north")),
				new SkipPropertyStateMapper("lit"));

			_mapper.Add("minecraft:torch", torchMap);
			_mapper.Add("minecraft:wall_torch", torchMap);
			_mapper.Add("minecraft:soul_torch", torchMap);
			_mapper.Add("minecraft:soul_wall_torch", torchMap);
			_mapper.Add("minecraft:redstone_torch", torchMap);
			_mapper.Add("minecraft:redstone_wall_torch", torchMap);

			#endregion

			#region Pressure plates

			var pressurePlateMap = new BlockStateMapper(
				new PropertyStateMapper("powered", "redstone_signal",
					new PropertyValueStateMapper("false", "0"),
					new PropertyValueStateMapper("true", "1")));

			var woodenPressurePlateMap = pressurePlateMap.Clone();
			woodenPressurePlateMap.BedrockName = "minecraft:wooden_pressure_plate";

			_mapper.Add($"minecraft:oak_pressure_plate", woodenPressurePlateMap);
			_mapper.Add($"minecraft:stone_pressure_plate", pressurePlateMap);
			_mapper.Add($"minecraft:polished_blackstone_pressure_plate", pressurePlateMap);
			foreach (var wood in _woodList)
			{
				_mapper.TryAdd($"minecraft:{wood}_pressure_plate", pressurePlateMap);
			}

			var weightedPressurePlateMap = new BlockStateMapper(
				new PropertyStateMapper("power", "redstone_signal"));

			_mapper.Add($"minecraft:light_weighted_pressure_plate", weightedPressurePlateMap);
			_mapper.Add($"minecraft:heavy_weighted_pressure_plate", weightedPressurePlateMap);

			#endregion

			#region Campfire

			var campFireMap = new BlockStateMapper(
				new PropertyStateMapper("lit", "extinguished",
					new PropertyValueStateMapper("true", "false"),
					new PropertyValueStateMapper("false", "true")),
				new SkipPropertyStateMapper("signal_fire"));

			_mapper.Add("minecraft:campfire", campFireMap);
			_mapper.Add("minecraft:soul_campfire", campFireMap);

			#endregion

			#region Flower pot

			var flowerPotMap = new BlockStateMapper(
				context =>
				{
					var plantType = context.AnvilName.Replace("potted_", "")
						.Replace("dead_bush", "deadbush")
						.Replace("azalea_bush", "azalea");

					var block = BlockFactory.GetBlockById(plantType);
					if (block?.IsValidStates ?? false)
					{
						context.Properties.Add(new NbtString("update_bit", "true"));
					}
					else
					{
						block = null;
						Log.Warn($"Can't find plant block for flower pot [{plantType}]");
					}

					context.BlockEntityTemplate = new FlowerPotBlockEntity()
					{
						PlantBlock = block
					};

					return "minecraft:flower_pot";
				});

			foreach (var plant in _pottedPlantsList)
			{
				_mapper.Add($"minecraft:potted_{plant}", flowerPotMap);
			}

			#endregion

			#region Growth

			var growthMap = new BlockStateMapper(
				new PropertyStateMapper("age", "growth"));
			var attachedGrowthMap = new BlockStateMapper(context => context.AnvilName.Replace("attached_", ""),
				oldFacingDirectionMap,
				new AdditionalPropertyStateMapper("growth", "7"));

			_mapper.Add("minecraft:wheat", growthMap);
			_mapper.Add("minecraft:carrots", growthMap);
			_mapper.Add("minecraft:potatoes", growthMap);
			_mapper.Add("minecraft:melon_stem", growthMap);
			_mapper.Add("minecraft:attached_melon_stem", attachedGrowthMap);
			_mapper.Add("minecraft:pumpkin_stem", growthMap);
			_mapper.Add("minecraft:attached_pumpkin_stem", attachedGrowthMap);
			_mapper.Add("minecraft:sweet_berry_bush", growthMap);

			_mapper.Add(new BlockStateMapper("minecraft:beetroots", "minecraft:beetroot", ageGrowthMap));

			#endregion

			#region Rails

			var railDirection = new PropertyStateMapper("shape", "rail_direction",
					new PropertyValueStateMapper("north_south", "0"),
					new PropertyValueStateMapper("east_west", "1"),
					new PropertyValueStateMapper("ascending_east", "2"),
					new PropertyValueStateMapper("ascending_west", "3"),
					new PropertyValueStateMapper("ascending_north", "4"),
					new PropertyValueStateMapper("ascending_south", "5"),
					new PropertyValueStateMapper("south_east", "6"),
					new PropertyValueStateMapper("south_west", "7"),
					new PropertyValueStateMapper("north_west", "8"),
					new PropertyValueStateMapper("north_east", "9"));

			var railDataBit = new PropertyStateMapper("powered", "rail_data_bit");

			_mapper.Add(new BlockStateMapper("minecraft:rail", railDirection));
			_mapper.Add(new BlockStateMapper("minecraft:activator_rail", railDirection, railDataBit));
			_mapper.Add(new BlockStateMapper("minecraft:detector_rail", railDirection, railDataBit));
			_mapper.Add(new BlockStateMapper("minecraft:powered_rail", "minecraft:golden_rail", railDirection, railDataBit));

			#endregion

			#region Mushroom blocks

			var mushroomBlockMap = new BlockStateMapper(
				context =>
				{
					var nameOnly = context.AnvilName.Replace("minecraft:", "");
					var down = context.Properties["down"].StringValue == "true" ? 1 : 0;
					var up = context.Properties["up"].StringValue == "true" ? 1 : 0;
					var south = context.Properties["south"].StringValue == "true" ? 1 : 0;
					var west = context.Properties["west"].StringValue == "true" ? 1 : 0;
					var north = context.Properties["north"].StringValue == "true" ? 1 : 0;
					var east = context.Properties["east"].StringValue == "true" ? 1 : 0;

					var faceDirection = nameOnly switch
					{
						"mushroom_stem" => (down, up, south, west, north, east) switch
						{
							(0, 0, 1, 1, 1, 1) => 10,
							(1, 1, 1, 1, 1, 1) => 15,
							_ => 0
						},

						_ when nameOnly.Contains("mushroom_block") => (down, up, south, west, north, east) switch
						{
							(0, _, 0, 1, 1, 0) => 1,
							(0, _, 0, 0, 1, 0) => 2,
							(0, _, 0, 0, 1, 1) => 3,
							(0, _, 0, 1, 0, 0) => 4,
							(0, 1, 0, 0, 0, 0) => 5,
							(0, _, 0, 0, 0, 1) => 6,
							(0, _, 1, 1, 0, 0) => 7,
							(0, _, 1, 0, 0, 0) => 8,
							(0, _, 1, 0, 0, 1) => 9,
							(1, 1, 1, 1, 1, 1) => 14,
							_ => 0
						},

						_ => 0
					};

					context.Properties.Clear();
					context.Properties.Add(new NbtString("huge_mushroom_bits", faceDirection.ToString()));

					if (nameOnly == "mushroom_stem")
					{
						return "minecraft:brown_mushroom_block";
					}

					return context.AnvilName;
				});

			_mapper.Add("minecraft:brown_mushroom_block", mushroomBlockMap);
			_mapper.Add("minecraft:red_mushroom_block", mushroomBlockMap);
			_mapper.Add("minecraft:mushroom_stem", mushroomBlockMap);

			#endregion

			#region Slabs

			var slabMapFunc = (string slabName, string doubleSlabName, NbtCompound properties) =>
			{
				bool doubleSlab = false;

				var type = properties["type"].StringValue;
				if (type == "double")
				{
					doubleSlab = true;
				}
				else
				{
					properties.Add(new NbtString("minecraft:vertical_half", type));
				}

				return doubleSlab ? doubleSlabName : slabName;
			};

			foreach (var material in _slabMaterialsList)
			{
				var slabName = $"minecraft:{material}_slab";
				var doubleSlabName = SlabBase.SlabToDoubleSlabMap.GetValueOrDefault(slabName);
				if (BlockFactory.Ids.Contains(slabName) && BlockFactory.Ids.Contains(doubleSlabName))
				{
					_mapper.Add(slabName, new BlockStateMapper(
						context => slabMapFunc(slabName, doubleSlabName, context.Properties),
						new SkipPropertyStateMapper("type")));
				}
			}

			_mapper.Add("minecraft:stone_slab", new BlockStateMapper(
						context => slabMapFunc("minecraft:normal_stone_slab", "minecraft:normal_stone_double_slab", context.Properties),
						new SkipPropertyStateMapper("type")));

			#endregion

			#region Cauldron

			var cauldronLevelMap = new PropertyStateMapper("level", (_, _, property) =>
			{
				var level = int.Parse(property.Value) * 2;
				return new NbtString("fill_level", level.ToString());
			});

			Func<BlockStateMapperContext, string> cauldronMapFunc = context =>
			{
				context.BlockEntityTemplate = new CauldronBlockEntity();

				return "minecraft:cauldron";
			};
 
			_mapper.Add(new BlockStateMapper("minecraft:lava_cauldron", null, cauldronMapFunc,
				new AdditionalPropertyStateMapper("fill_level", "6"),
				new AdditionalPropertyStateMapper("cauldron_liquid", "lava"),
				new SkipPropertyStateMapper("level")));

			_mapper.Add(new BlockStateMapper("minecraft:powder_snow_cauldron", null, cauldronMapFunc,
				cauldronLevelMap,
				new AdditionalPropertyStateMapper("cauldron_liquid", "powder_snow")));

			_mapper.Add(new BlockStateMapper("minecraft:water_cauldron", null, cauldronMapFunc,
				cauldronLevelMap,
				new AdditionalPropertyStateMapper("cauldron_liquid", "water")));

			#endregion

			#region Cakes

			var bitesMap = new PropertyStateMapper("bites", "bite_counter");

			_mapper.Add(new BlockStateMapper("minecraft:cake", bitesMap));
			_mapper.Add(new BlockStateMapper("minecraft:candle_cake", bitesMap));
			foreach (var color in _colorsList)
			{
				_mapper.Add(new BlockStateMapper($"minecraft:{color}_candle_cake", bitesMap));
			}

			#endregion

			#region Skull

			var skullMap = new BlockStateMapper(context =>
			{
				var rotation = byte.Parse(context.Properties["rotation"]?.StringValue ?? "0");
				if (!context.AnvilName.Contains("_wall"))
				{
					context.Properties["facing"] = new NbtString("facing", "up");
				} 

				var skullType = context.AnvilName.Replace("minecraft:", "").Replace("_head", "").Replace("_wall", "");

				var skullTypeBit = skullType switch
				{
					"skeleton_skull" => 0,
					"wither_skeleton_skull" => 1,
					"zombie" => 2,
					"player" => 3,
					"creeper" => 4,
					"dragon" => 5,
					"piglin" => 6,
					_ => -1
				};

				if (skullTypeBit == -1)
				{
					Log.Error($"Unknown skull type [{skullType}]");
					skullTypeBit = 0;
				}

				context.BlockEntityTemplate = new SkullBlockEntity()
				{
					Rotation = rotation,
					SkullType = (byte) skullTypeBit,
					MouthMoving = context.Properties["powered"]?.StringValue == "true"
				};

				return "minecraft:skull";
			},
			oldFacingDirectionMap,
			poweredSkipMap,
			new SkipPropertyStateMapper("rotation"));

			foreach (var skull in _skullsList)
			{
				_mapper.Add($"minecraft:{skull}", skullMap);
			}

			#endregion

			#region Anvil

			var anvilMap = new BlockStateMapper(cardinalDirectionMap);
			_mapper.Add("minecraft:anvil", anvilMap);
			_mapper.Add("minecraft:chipped_anvil", anvilMap);
			_mapper.Add("minecraft:damaged_anvil", anvilMap);

			#endregion

			#region Pistons

			var pistonMap = new BlockStateMapper(
				oldFacingDirectionMap3,
				new SkipPropertyStateMapper("extended"));

			_mapper.Add("minecraft:piston", pistonMap);
			_mapper.Add("minecraft:sticky_piston", pistonMap);

			_mapper.Add("minecraft:piston_head", new BlockStateMapper(
				context =>
				{
					return context.Properties["type"].StringValue == "sticky" ? "minecraft:sticky_piston_arm_collision" : "minecraft:piston_arm_collision";
				},
				oldFacingDirectionMap3,
				new SkipPropertyStateMapper("short"),
				new SkipPropertyStateMapper("type")));

			_mapper.Add(new BlockStateMapper("minecraft:moving_piston", "minecraft:moving_block",
				new SkipPropertyStateMapper("facing"),
				new SkipPropertyStateMapper("type")));

			#endregion

			#region Bookshelfs

			_mapper.Add(new BlockStateMapper("minecraft:chiseled_bookshelf",
				context =>
				{
					var booksStored = 0;

					for (var i = 0; i < 6; i++)
					{
						var name = $"slot_{i}_occupied";
						booksStored |= context.Properties[name].StringValue == "true" ? 1 << i : 0;
						context.Properties.Remove(name);
					}

					context.Properties.Add(new NbtString("books_stored", booksStored.ToString()));

					return context.AnvilName;
				},
				directionMap));

			#endregion

			// minecraft:cave_vines
			_mapper.Add("minecraft:cave_vines", new BlockStateMapper(
				context => context.Properties["berries"]?.StringValue == "true"
					? "minecraft:cave_vines_head_with_berries"
					: context.AnvilName,
				new PropertyStateMapper("age", "growing_plant_age"),
				new SkipPropertyStateMapper("berries")));

			_mapper.Add("minecraft:cave_vines_plant", new BlockStateMapper(
				context => context.Properties["berries"]?.StringValue == "true"
					? "minecraft:cave_vines_body_with_berries"
					: "minecraft:cave_vines",
				new AdditionalPropertyStateMapper("growing_plant_age", "25"),
				new SkipPropertyStateMapper("berries")));


			// minecraft:vine
			_mapper.Add("minecraft:vine", new BlockStateMapper(
				context =>
				{
					var faceDirection = 0;

					faceDirection |= context.Properties["south"].StringValue == "true" ? 1 << 0 : 0;
					faceDirection |= context.Properties["west"].StringValue == "true" ? 1 << 1 : 0;
					faceDirection |= context.Properties["north"].StringValue == "true" ? 1 << 2 : 0;
					faceDirection |= context.Properties["east"].StringValue == "true" ? 1 << 3 : 0;
					var up = context.Properties["up"].StringValue == "true";

					context.Properties.Clear();

					if (faceDirection == 0)
					{
						if (up)
						{
							return "minecraft:air";
						}

						faceDirection = 0xF;
					}

					context.Properties.Add(new NbtString("vine_direction_bits", faceDirection.ToString()));

					return context.AnvilName;
				}));


			// minecraft:kelp
			var kelpMap = new BlockStateMapper("minecraft:kelp",
				context =>
				{
					if (context.Properties["age"] != null)
						context.Properties["age"] = new NbtString("age", Math.Min(15, int.Parse(context.Properties["age"].StringValue)).ToString());
				},
				new PropertyStateMapper("age", "kelp_age"));

			_mapper.Add("minecraft:kelp", kelpMap);
			_mapper.Add("minecraft:kelp_plant", kelpMap);


			// minecraft:seagrass
			var seagrassMap = new BlockStateMapper("minecraft:seagrass",
				context =>
				{
					var grassType = "default";
					if (context.AnvilName == "minecraft:tall_seagrass")
						grassType = context.Properties["half"].StringValue == "upper" ? "double_top" : "double_bot";

					context.Properties.Add(new NbtString("sea_grass_type", grassType));
				},
				new SkipPropertyStateMapper("half"));

			_mapper.Add("minecraft:seagrass", seagrassMap);
			_mapper.Add("minecraft:tall_seagrass", seagrassMap);


			var sculkSensorPhaseMap = new PropertyStateMapper("sculk_sensor_phase",
					new PropertyValueStateMapper("inactive", "0"),
					new PropertyValueStateMapper("active", "1"),
					new PropertyValueStateMapper("cooldown", "2"));

			// minecraft:sculk_sensor
			_mapper.Add(new BlockStateMapper("minecraft:sculk_sensor",
				sculkSensorPhaseMap,
				powerSkipMap));

			// minecraft:calibrated_sculk_sensor
			_mapper.Add(new BlockStateMapper("minecraft:calibrated_sculk_sensor",
				sculkSensorPhaseMap,
				powerSkipMap));

			// minecraft:*_stairs
			foreach (var material in _slabMaterialsList)
			{
				var bedrockName = material;
				if (material == "stone")
				{
					bedrockName = "normal_stone";
				}
				else if (material == "cobblestone")
				{
					bedrockName = "stone";
				}
				else if (material == "prismarine_brick")
				{
					bedrockName = "prismarine_bricks";
				}
				else if (material == "end_stone_brick")
				{
					bedrockName = "end_brick";
				}

				_mapper.Add(new BlockStateMapper($"minecraft:{material}_stairs", $"minecraft:{bedrockName}_stairs",
					upsideDownBitMap,
					new PropertyStateMapper("facing", "weirdo_direction",
						new PropertyValueStateMapper("east", "0"),
						new PropertyValueStateMapper("west", "1"),
						new PropertyValueStateMapper("south", "2"),
						new PropertyValueStateMapper("north", "3")),
					new SkipPropertyStateMapper("shape")));
			}


			// minecraft:*_wall
			var wallConnectionMap = new PropertyValueStateMapper("low", "short");

			foreach (var material in _slabMaterialsList)
			{
				var addWallBlockType = material switch
				{
					"cobblestone"
					or "mossy_cobblestone"
					or "granite"
					or "diorite" 
					or "andesite"
					or "sandstone"
					or "brick"
					or "stone_brick"
					or "mossy_stone_brick"
					or "nether_brick"
					or "end_stone_brick"
					or "prismarine"
					or "red_sandstone"
					or "red_nether_brick" => true,
					_ => false
				};

				var materialOverride = material switch
				{
					"end_stone_brick" => "end_brick",
					_ => material
				};

				_mapper.Add($"minecraft:{material}_wall", new BlockStateMapper(
					context =>
					{
						if (addWallBlockType)
						{
							context.Properties.Add(new NbtString("wall_block_type", materialOverride));
							return "minecraft:cobblestone_wall";
						}

						return context.AnvilName;
					},
					new PropertyStateMapper("up", "wall_post_bit"),
					new PropertyStateMapper("east", "wall_connection_type_east", wallConnectionMap),
					new PropertyStateMapper("north", "wall_connection_type_north", wallConnectionMap),
					new PropertyStateMapper("south", "wall_connection_type_south", wallConnectionMap),
					new PropertyStateMapper("west", "wall_connection_type_west", wallConnectionMap)));
			}

			// minecraft:lever
			_mapper.Add(new BlockStateMapper("minecraft:lever",
				context =>
				{
					var face = context.Properties["face"].StringValue;
					var facing = context.Properties["facing"].StringValue;

					var direction = (face, facing) switch
					{
						("ceiling", "east" or "west") => "down_east_west",
						("wall", _) => facing,
						("floor", "north" or "south") => "up_north_south",
						("floor", "east" or "west") => "up_east_west",
						("ceiling", "north" or "south") => "down_north_south",
						_ => "down_east_west"
					};

					context.Properties.Add(new NbtString("lever_direction", direction));
				},
				new PropertyStateMapper("powered", "open_bit"),
				new SkipPropertyStateMapper("face"),
				new SkipPropertyStateMapper("facing")));

			//minecraft:redstone_wire
			var redstoneWireMap = faceDirectionSkipMap.Clone();
			redstoneWireMap.PropertiesMap.Add("power", new PropertyStateMapper("power", "redstone_signal"));
			_mapper.Add("minecraft:redstone_wire", redstoneWireMap);

			//minecraft:repeater
			_mapper.Add("minecraft:repeater", new BlockStateMapper(
				context =>
				{
					return context.Properties["powered"].StringValue == "true" ? "minecraft:powered_repeater" : "minecraft:unpowered_repeater";
				},
				new PropertyStateMapper("delay", 
					(name, properties, property) => new NbtString("repeater_delay", (int.Parse(property.Value) - 1).ToString())),
				new SkipPropertyStateMapper("locked"),
				poweredSkipMap));

			//minecraft:comparator
			_mapper.Add("minecraft:comparator", new BlockStateMapper(
				context =>
				{
					return context.Properties["powered"].StringValue == "true" ? "minecraft:powered_comparator" : "minecraft:unpowered_comparator";
				},
				new PropertyStateMapper("mode", "output_subtract_bit",
					new PropertyValueStateMapper("compare", "false"),
					new PropertyValueStateMapper("subtract", "true")),
				new PropertyStateMapper("powered", "output_lit_bit"),
				new SkipPropertyStateMapper("locked")));

			//minecraft:tripwire_hook
			_mapper.Add(new BlockStateMapper("minecraft:tripwire_hook",
				new BitPropertyStateMapper("attached"),
				new BitPropertyStateMapper("powered"),
				directionMap));

			//minecraft:tripwire
			_mapper.Add(new BlockStateMapper("minecraft:tripwire", "minecraft:trip_wire", 
				context =>
				{
					// temp, needs correct logic
					var directionProperties = new[]
					{
						"east",
						"west",
						"south",
						"north"
					};
					var connections = 0;

					foreach (var directionProperty in directionProperties)
					{
						if (context.Properties[directionProperty].StringValue == "true")
						{
							connections++;
						}

						context.Properties.Remove(directionProperty);
					}

					context.Properties.Add(new NbtString("suspended_bit", connections <= 2 ? "1" : "0"));
				},
				new BitPropertyStateMapper("attached"),
				new BitPropertyStateMapper("powered"),
				new BitPropertyStateMapper("disarmed")));

			//minecraft:daylight_detector
			_mapper.Add("minecraft:daylight_detector", new BlockStateMapper(
				context =>
				{
					var invertedPart = context.Properties["inverted"].StringValue == "true" ? "_inverted" : "";

					return $"minecraft:daylight_detector{invertedPart}";
				},
				new PropertyStateMapper("power", "redstone_signal"),
				new SkipPropertyStateMapper("inverted")));

			//TODO: remove after 1.21.20
			var coralMap = new BlockStateMapper(
				new PropertyStateMapper("facing", "coral_direction",
					new PropertyValueStateMapper("west", "0"),
					new PropertyValueStateMapper("east", "1"),
					new PropertyValueStateMapper("north", "2"),
					new PropertyValueStateMapper("south", "3")));

			var coralFans = new[]
			{
				"minecraft:tube_coral_fan",
				"minecraft:brain_coral_fan",
				"minecraft:bubble_coral_fan",
				"minecraft:fire_coral_fan",
				"minecraft:horn_coral_fan",
				"minecraft:tube_coral_wall_fan",
				"minecraft:brain_coral_wall_fan",
				"minecraft:bubble_coral_wall_fan",
				"minecraft:fire_coral_wall_fan",
				"minecraft:horn_coral_wall_fan",
				"minecraft:dead_tube_coral_fan",
				"minecraft:dead_brain_coral_fan",
				"minecraft:dead_bubble_coral_fan",
				"minecraft:dead_fire_coral_fan",
				"minecraft:dead_horn_coral_fan",
				"minecraft:dead_tube_coral_wall_fan",
				"minecraft:dead_brain_coral_wall_fan",
				"minecraft:dead_bubble_coral_wall_fan",
				"minecraft:dead_fire_coral_wall_fan",
				"minecraft:dead_horn_coral_wall_fan"
			};
			foreach (var coralFan in coralFans)
			{
				_mapper.Add(coralFan, coralMap);
			}

			//minecraft:sea_pickle
			_mapper.Add(new BlockStateMapper("minecraft:sea_pickle",
				new PropertyStateMapper("pickles", (_, _, property) => new NbtString("cluster_count", (int.Parse(property.StringValue) - 1).ToString()))));

			//minecraft:pink_petals
			_mapper.Add(new BlockStateMapper("minecraft:pink_petals",
				new PropertyStateMapper("flower_amount", (_, _, property) => new NbtString("growth", (int.Parse(property.StringValue) - 1).ToString()))));

			//minecraft:trial_spawner
			_mapper.Add(new BlockStateMapper("minecraft:trial_spawner",
				new PropertyStateMapper("trial_spawner_state",
					new PropertyValueStateMapper("inactive", "0"),
					new PropertyValueStateMapper("waiting_for_players", "1"),
					new PropertyValueStateMapper("active", "2"),
					new PropertyValueStateMapper("waiting_for_reward_ejection", "3"),
					new PropertyValueStateMapper("ejecting_reward", "4"),
					new PropertyValueStateMapper("cooldown", "5"))));

			//minecraft:jigsaw
			_mapper.Add(new BlockStateMapper("minecraft:jigsaw",
				context =>
				{
					var orientation = context.Properties["orientation"].StringValue;

					var (direction, rotation) = orientation switch
					{
						"down_north" => ("0", "0"),
						"down_west" => ("0", "1"),
						"down_south" => ("0", "2"),
						"down_east" => ("0", "3"),
						"up_north" => ("1", "0"),
						"up_east" => ("1", "1"),
						"up_south" => ("1", "2"),
						"up_west" => ("1", "3"),
						"north_up" => ("2", "0"),
						"south_up" => ("3", "0"),
						"west_up" => ("4", "0"),
						"east_up" => ("5", "0"),
						_ => ("0", "0")
					};

					context.Properties.Add(new NbtString("facing_direction", direction));
					context.Properties.Add(new NbtString("rotation", rotation));
				}
				, new SkipPropertyStateMapper("orientation")));
		}

		public static int GetRuntimeIdByPalette(NbtCompound palette, out BlockEntity blockEntity)
		{
			var name = palette["Name"].StringValue;
			var properties = (NbtCompound) (palette["Properties"] as NbtCompound)?.Clone() ?? new NbtCompound();

			blockEntity = null;
			var context = new BlockStateMapperContext(name, properties);

			try
			{
				name = _mapper.Resolve(context);

				var block = BlockFactory.GetBlockById(name);

				if (block == null)
				{
					Log.Warn($"Can't find block [{name}] with props1 [{palette["Properties"]}], props2 [{properties}]");
					return new InfoUpdate().RuntimeId;
				}

				var states = block.States.ToList();

				var result = FillProperties(states, properties);
				if (!result)
				{
					Log.Warn($"Can't find block [{name}] with props1 [{palette["Properties"]}], props2 [{properties}]");
					return new InfoUpdate().RuntimeId;
				}

				var container = new PaletteBlockStateContainer(block.Id, states);
				if (!BlockFactory.BlockStates.TryGetValue(container, out var blockstate))
				{
					Log.Warn($"Did not find block state for {block}, {container}");
					return new InfoUpdate().RuntimeId;
				}

				blockEntity = context.BlockEntityTemplate;
				return blockstate.RuntimeId;
			}
			catch (Exception e)
			{
				Log.Warn($"Can't find block [{name}] with props1 [{palette["Properties"]}], props2 [{properties}]");
				Log.Error(e);
				return new InfoUpdate().RuntimeId;
			}
		}

		public static bool IsSeaBlock(string name)
		{
			return _seaBlocks.Contains(name);
		}

		public static Biome GetBiomeByName(string name)
		{
			var biomeName = name.Split(':').Last();

			var biome = BiomeUtils.GetBiome(_anvilBedrockBiomesMap.GetValueOrDefault(biomeName, biomeName));
			if (biome == null)
			{
				Log.Warn($"Missing biome [{name}]");
				return BiomeUtils.GetBiome(1);
			}

			return biome;
		}

		private static bool FillProperties(List<IBlockState> states, NbtCompound propertiesTag)
		{
			foreach (var prop in propertiesTag)
			{
				// workaround for incompatible mapping from anvil
				if (prop.Name == AnvilIncompatibleBitName)
				{
					states.Add(new BlockStateByte() { Name = prop.Name, Value = 1 });
					continue;
				}

				var state = states.FirstOrDefault(state => state.Name == prop.Name);

				if (state == null) return false;

				var value = prop.StringValue;
				switch (state)
				{
					case BlockStateString blockStateString:
						blockStateString.Value = value;
						break;
					case BlockStateByte blockStateByte:
						blockStateByte.Value = value switch
						{
							"false" => 0,
							"true" => 1,
							_ => byte.Parse(value)
						};
						break;
					case BlockStateInt blockStateByte:
						blockStateByte.Value = int.Parse(value);
						break;
				}
			}

			return true;
		}

		public class AnvilToBedrockStateMapper
		{
			private readonly Dictionary<string, BlockStateMapper> _map = new Dictionary<string, BlockStateMapper>();
			private readonly List<BlockStateMapper> _defaultMap = new List<BlockStateMapper>();

			public void Add(BlockStateMapper map)
			{
				Add(map.AnvilName, map);
			}

			public void Add(string name, BlockStateMapper map)
			{
				_map.Add(name, map);
			}

			public bool TryAdd(BlockStateMapper map)
			{
				return TryAdd(map.AnvilName, map);
			}

			public bool TryAdd(string name, BlockStateMapper map)
			{
				return _map.TryAdd(name, map);
			}

			public void AddDefault(BlockStateMapper map)
			{
				_defaultMap.Add(map);
			}

			public string Resolve(BlockStateMapperContext context)
			{
				if (_map.TryGetValue(context.AnvilName, out var map))
				{
					context.AnvilName = map.Resolve(context);
				}

				foreach (var defMap in _defaultMap)
				{
					defMap.ResolveDefault(context);
				}

				return context.AnvilName;
			}
		}

		public class BlockStateMapperContext
		{
			public string AnvilName { get; set; }
			public string BedrockName { get; set; }
			public NbtCompound Properties { get; set; }
			public BlockEntity BlockEntityTemplate { get; set; }

			public BlockStateMapperContext(string anvilName, NbtCompound properties)
			{
				AnvilName = anvilName;
				Properties = properties;
			}
		}

		public class BlockStateMapper
		{
			public string AnvilName { get; set; }
			public string BedrockName { get; set; }

			public Dictionary<string, PropertyStateMapper> PropertiesMap { get; } = new Dictionary<string, PropertyStateMapper>();
			public List<AdditionalPropertyStateMapper> AdditionalProperties { get; } = new List<AdditionalPropertyStateMapper>();
			public Dictionary<string, SkipPropertyStateMapper> SkipProperties { get; } = new Dictionary<string, SkipPropertyStateMapper>();

			private readonly Func<BlockStateMapperContext, string> _func;

			public BlockStateMapper(Func<BlockStateMapperContext, string> func)
			{
				_func = func;
			}

			public BlockStateMapper(Action<BlockStateMapperContext> func, params IPropertyStateMapper[] propertiesMap)
				: this(null, null, func, propertiesMap)
			{

			}

			public BlockStateMapper(Func<BlockStateMapperContext, string> func, params IPropertyStateMapper[] propertiesMap)
				: this(null, null, func, propertiesMap)
			{

			}

			public BlockStateMapper(string anvilName, Action<BlockStateMapperContext> func)
				: this(anvilName, anvilName, func)
			{

			}

			public BlockStateMapper(string anvilName, Func<BlockStateMapperContext, string> func)
				: this(anvilName, anvilName, func)
			{

			}

			public BlockStateMapper(string anvilName, Action<BlockStateMapperContext> func, params IPropertyStateMapper[] propertiesMap)
				: this(anvilName, anvilName, func, propertiesMap)
			{

			}

			public BlockStateMapper(string anvilName, Func<BlockStateMapperContext, string> func, params IPropertyStateMapper[] propertiesMap)
				: this(anvilName, anvilName, func, propertiesMap)
			{

			}

			public BlockStateMapper(params IPropertyStateMapper[] propertiesMap)
				: this(anvilName: null, propertiesMap)
			{

			}

			public BlockStateMapper(string anvilName, params IPropertyStateMapper[] propertiesMap)
				: this(anvilName, anvilName, null, propertiesMap)
			{

			}

			public BlockStateMapper(string anvilName, string bedrockName, params IPropertyStateMapper[] propertiesMap)
				: this(anvilName, bedrockName, null, propertiesMap)
			{

			}

			public BlockStateMapper(string anvilName, string bedrockName, Action<BlockStateMapperContext> func, params IPropertyStateMapper[] propertiesMap)
				: this(anvilName, bedrockName, context =>
				{
					func(context);
					return context.AnvilName;
				}, propertiesMap)
			{
			}

			public BlockStateMapper(string anvilName, string bedrockName, Func<BlockStateMapperContext, string> func, params IPropertyStateMapper[] propertiesMap)
			{
				AnvilName = anvilName;
				BedrockName = bedrockName;

				_func = func;

				foreach (var map in propertiesMap)
				{
					if (map is PropertyStateMapper propertyStateMapper)
					{
						PropertiesMap.Add(propertyStateMapper.AnvilName ?? propertyStateMapper.GetHashCode().ToString(), propertyStateMapper);
					}
					else if (map is AdditionalPropertyStateMapper additionalPropertyStateMapper)
					{
						AdditionalProperties.Add(additionalPropertyStateMapper);
					}
					else if (map is SkipPropertyStateMapper skipPropertyStateMapper)
					{
						SkipProperties.Add(skipPropertyStateMapper.Name ?? skipPropertyStateMapper.GetHashCode().ToString(), skipPropertyStateMapper);
					}
				}
			}

			public string Resolve(BlockStateMapperContext context)
			{
				if (_func != null) context.AnvilName = _func(context);

				foreach (NbtString prop in context.Properties.ToArray())
				{
					if (SkipProperties.TryGetValue(prop.Name, out var skipMap) && skipMap.Resolve(context.AnvilName, context.Properties, prop))
					{
						context.Properties.Remove(prop.Name);
					}
					else if (PropertiesMap.TryGetValue(prop.Name, out var propMap))
					{
						context.Properties.Remove(prop.Name);
						context.Properties.Add(propMap.Resolve(context.AnvilName, context.Properties, prop));
					}
				}

				foreach (var prop in AdditionalProperties)
				{
					context.Properties[prop.Name] = prop.Resolve(context.AnvilName, context.Properties);
				}

				return BedrockName ?? context.AnvilName;
			}

			public string ResolveDefault(BlockStateMapperContext context)
			{
				if (_func != null) return _func(context);

				foreach (NbtString prop in context.Properties.ToArray())
				{
					var skipMap = SkipProperties.Values.FirstOrDefault(map => map.Name == prop.Name || map.Name == null);
					if (skipMap != null && skipMap.Resolve(context.AnvilName, context.Properties, prop))
					{
						context.Properties.Remove(prop.Name);
					}
					else
					{
						var propMap = PropertiesMap.Values.FirstOrDefault(map => map.AnvilName == prop.Name || map.AnvilName == null);

						if (propMap != null)
						{
							context.Properties.Remove(prop.Name);
							context.Properties.Add(propMap.Resolve(context.AnvilName, context.Properties, prop));
						}
					}
				}

				foreach (var prop in AdditionalProperties)
				{
					context.Properties[prop.Name] = prop.Resolve(context.AnvilName, context.Properties);
				}

				return BedrockName ?? context.AnvilName;
			}

			public BlockStateMapper Clone()
			{
				var propertiesMap = new List<IPropertyStateMapper>();

				foreach (var property in PropertiesMap.Values)
					propertiesMap.Add(property.Clone());

				foreach (var property in AdditionalProperties)
					propertiesMap.Add(property.Clone());

				foreach (var property in SkipProperties.Values)
					propertiesMap.Add(property.Clone());

				return new BlockStateMapper(AnvilName, BedrockName, (Func<BlockStateMapperContext, string>) _func?.Clone(), propertiesMap.ToArray());
			}
		}

		public interface IPropertyStateMapper { }

		public class BitPropertyStateMapper : PropertyStateMapper
		{
			public BitPropertyStateMapper(string anvilName)
				: base(anvilName, $"{anvilName}_bit",
					  new PropertyValueStateMapper("false", "0"),
					  new PropertyValueStateMapper("true", "1"))
			{

			}
		}

		public class PropertyStateMapper : IPropertyStateMapper
		{
			public string AnvilName { get; set; }
			public string BedrockName { get; set; }

			public Dictionary<string, PropertyValueStateMapper> ValuesMap { get; } = new Dictionary<string, PropertyValueStateMapper>();

			private readonly Func<string, NbtCompound, NbtString, NbtString> _func;

			public PropertyStateMapper(params PropertyValueStateMapper[] propertiesNameMap)
				: this(anvilName: null, bedrockName: null, propertiesNameMap)
			{

			}

			public PropertyStateMapper(Func<string, NbtCompound, NbtString, NbtString> func)
				: this(anvilName: null, func)
			{

			}

			public PropertyStateMapper(string anvilName, params PropertyValueStateMapper[] propertiesNameMap)
				: this(anvilName, bedrockName: null, propertiesNameMap)
			{

			}

			public PropertyStateMapper(string anvilName, string bedrockName, params PropertyValueStateMapper[] propertiesNameMap)
			{
				AnvilName = anvilName;
				BedrockName = bedrockName;

				foreach (var map in propertiesNameMap)
					ValuesMap.Add(map.AnvilName, map);
			}

			public PropertyStateMapper(string anvilName, Func<string, NbtCompound, NbtString, NbtString> func, params PropertyValueStateMapper[] propertiesNameMap)
			{
				AnvilName = anvilName;
				_func = func;

				foreach (var map in propertiesNameMap)
					ValuesMap.Add(map.AnvilName, map);
			}

			public NbtString Resolve(string anvilName, NbtCompound properties, NbtString property)
			{
				return _func?.Invoke(anvilName, properties, property)
					?? new NbtString(BedrockName ?? property.Name, ValuesMap.GetValueOrDefault(property.StringValue)?.Resolve(anvilName, properties) ?? property.StringValue);
			}

			public PropertyStateMapper Clone()
			{
				return new PropertyStateMapper(
					AnvilName,
					(Func<string, NbtCompound, NbtString, NbtString>) _func?.Clone(),
					ValuesMap.Values.Select(v => v.Clone()).ToArray())
				{
					BedrockName = BedrockName
				};
			}
		}

		public class AdditionalPropertyStateMapper : IPropertyStateMapper
		{
			public string Name { get; set; }
			public string Value { get; set; }

			private readonly Func<string, NbtCompound, string> _func;

			public AdditionalPropertyStateMapper(string name, string value)
			{
				Name = name;
				Value = value;
			}

			public AdditionalPropertyStateMapper(string name, Func<string, NbtCompound, string> func)
			{
				Name = name;
				_func = func;
			}

			public NbtString Resolve(string anvilName, NbtCompound properties)
			{
				return new NbtString(Name, _func?.Invoke(anvilName, properties) ?? Value);
			}

			public AdditionalPropertyStateMapper Clone()
			{
				return new AdditionalPropertyStateMapper(
					Name,
					(Func<string, NbtCompound, string>) _func?.Clone())
				{
					Value = Value
				};
			}
		}

		public class SkipPropertyStateMapper : IPropertyStateMapper
		{
			public string Name { get; set; }

			private readonly Func<string, NbtCompound, NbtString, bool> _func;

			public SkipPropertyStateMapper(string name)
			{
				Name = name;
			}

			public SkipPropertyStateMapper(string name, Func<string, NbtCompound, NbtString, bool> func)
			{
				Name = name;
				_func = func;
			}

			public bool Resolve(string anvilName, NbtCompound properties, NbtString value)
			{
				return _func?.Invoke(anvilName, properties, value) ?? true;
			}

			public SkipPropertyStateMapper Clone()
			{
				return new SkipPropertyStateMapper(Name, (Func<string, NbtCompound, NbtString, bool>) _func?.Clone());
			}
		}

		public class PropertyValueStateMapper
		{
			public string AnvilName { get; set; }
			public string BedrockName { get; set; }

			private readonly Func<string, NbtCompound, string, string> _func;

			public PropertyValueStateMapper(string anvilName, string bedrockName)
			{
				AnvilName = anvilName;
				BedrockName = bedrockName;
			}

			public PropertyValueStateMapper(string anvilName, Func<string, NbtCompound, string, string> func)
			{
				AnvilName = anvilName;
				_func = func;
			}

			public string Resolve(string anvilName, NbtCompound properties)
			{
				return _func?.Invoke(anvilName, properties, AnvilName) ?? BedrockName;
			}

			public PropertyValueStateMapper Clone()
			{
				return new PropertyValueStateMapper(
					AnvilName,
					(Func<string, NbtCompound, string, string>) _func?.Clone())
				{
					BedrockName = BedrockName
				};
			}
		}
	}
}
