using MiNET.Utils;

namespace MiNET.Blocks.States
{

	public partial class Candles : BlockStateInt
	{
		public override string Name => "candles";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class Lit : BlockStateByte
	{
		public override string Name => "lit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class PillarAxis : BlockStateString
	{
		public override string Name => "pillar_axis";

		protected PillarAxis(string value)
		{
			Value = value;
		}

		public static readonly PillarAxis Y = new PillarAxis("y");
		public static readonly PillarAxis X = new PillarAxis("x");
		public static readonly PillarAxis Z = new PillarAxis("z");

		public static PillarAxis[] Values()
		{
			return [Y, X, Z];
		}

	} // class

	public partial class GroundSignDirection : BlockStateInt
	{
		public override string Name => "ground_sign_direction";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class Direction : BlockStateInt
	{
		public override string Name => "direction";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class DoorHingeBit : BlockStateByte
	{
		public override string Name => "door_hinge_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OpenBit : BlockStateByte
	{
		public override string Name => "open_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class UpperBlockBit : BlockStateByte
	{
		public override string Name => "upper_block_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OldFacingDirection : BlockStateInt
	{
		public override string Name => "facing_direction";

		public static int MaxValue { get; } = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}

	} // class

	public partial class InWallBit : BlockStateByte
	{
		public override string Name => "in_wall_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ButtonPressedBit : BlockStateByte
	{
		public override string Name => "button_pressed_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class UpsideDownBit : BlockStateByte
	{
		public override string Name => "upside_down_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class WeirdoDirection : BlockStateInt
	{
		public override string Name => "weirdo_direction";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class VerticalHalf : BlockStateString
	{
		public override string Name => "minecraft:vertical_half";

		protected VerticalHalf(string value)
		{
			Value = value;
		}

		public static readonly VerticalHalf Bottom = new VerticalHalf("bottom");
		public static readonly VerticalHalf Top = new VerticalHalf("top");

		public static VerticalHalf[] Values()
		{
			return [Bottom, Top];
		}

	} // class

	public partial class RedstoneSignal : BlockStateInt
	{
		public override string Name => "redstone_signal";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class AttachedBit : BlockStateByte
	{
		public override string Name => "attached_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Hanging : BlockStateByte
	{
		public override string Name => "hanging";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CoveredBit : BlockStateByte
	{
		public override string Name => "covered_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Height : BlockStateInt
	{
		public override string Name => "height";

		public static int MaxValue { get; } = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}

	} // class

	public partial class ItemFrameMapBit : BlockStateByte
	{
		public override string Name => "item_frame_map_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ItemFramePhotoBit : BlockStateByte
	{
		public override string Name => "item_frame_photo_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CoralFanDirection : BlockStateInt
	{
		public override string Name => "coral_fan_direction";

		public static int MaxValue { get; } = 1;

		public static int[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Active : BlockStateByte
	{
		public override string Name => "active";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CanSummon : BlockStateByte
	{
		public override string Name => "can_summon";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class SeaGrassType : BlockStateString
	{
		public override string Name => "sea_grass_type";

		protected SeaGrassType(string value)
		{
			Value = value;
		}

		public static readonly SeaGrassType Default = new SeaGrassType("default");
		public static readonly SeaGrassType DoubleTop = new SeaGrassType("double_top");
		public static readonly SeaGrassType DoubleBot = new SeaGrassType("double_bot");

		public static SeaGrassType[] Values()
		{
			return [Default, DoubleTop, DoubleBot];
		}

	} // class

	public partial class Growth : BlockStateInt
	{
		public override string Name => "growth";

		public static int MaxValue { get; } = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}

	} // class

	public partial class CoralDirection : BlockStateInt
	{
		public override string Name => "coral_direction";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class UpdateBit : BlockStateByte
	{
		public override string Name => "update_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BooksStored : BlockStateInt
	{
		public override string Name => "books_stored";

		public static int MaxValue { get; } = 63;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63];
		}

	} // class

	public partial class BlockFace : BlockStateString
	{
		public override string Name => "minecraft:block_face";

		protected BlockFace(string value)
		{
			Value = value;
		}

		public static readonly BlockFace Down = new BlockFace("down");
		public static readonly BlockFace Up = new BlockFace("up");
		public static readonly BlockFace North = new BlockFace("north");
		public static readonly BlockFace South = new BlockFace("south");
		public static readonly BlockFace West = new BlockFace("west");
		public static readonly BlockFace East = new BlockFace("east");

		public static BlockFace[] Values()
		{
			return [Down, Up, North, South, West, East];
		}

	} // class

	public partial class RailDataBit : BlockStateByte
	{
		public override string Name => "rail_data_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class RailDirection : BlockStateInt
	{
		public override string Name => "rail_direction";

		public static int MaxValue { get; } = 9;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
		}

	} // class

	public partial class CardinalDirection : BlockStateString
	{
		public override string Name => "minecraft:cardinal_direction";

		protected CardinalDirection(string value)
		{
			Value = value;
		}

		public static readonly CardinalDirection South = new CardinalDirection("south");
		public static readonly CardinalDirection West = new CardinalDirection("west");
		public static readonly CardinalDirection North = new CardinalDirection("north");
		public static readonly CardinalDirection East = new CardinalDirection("east");

		public static CardinalDirection[] Values()
		{
			return [South, West, North, East];
		}

	} // class

	public partial class OutputLitBit : BlockStateByte
	{
		public override string Name => "output_lit_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OutputSubtractBit : BlockStateByte
	{
		public override string Name => "output_subtract_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class AgeBit : BlockStateByte
	{
		public override string Name => "age_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Age : BlockStateInt
	{
		public override string Name => "age";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class WallConnectionTypeEast : BlockStateString
	{
		public override string Name => "wall_connection_type_east";

		protected WallConnectionTypeEast(string value)
		{
			Value = value;
		}

		public static readonly WallConnectionTypeEast None = new WallConnectionTypeEast("none");
		public static readonly WallConnectionTypeEast Short = new WallConnectionTypeEast("short");
		public static readonly WallConnectionTypeEast Tall = new WallConnectionTypeEast("tall");

		public static WallConnectionTypeEast[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class WallConnectionTypeNorth : BlockStateString
	{
		public override string Name => "wall_connection_type_north";

		protected WallConnectionTypeNorth(string value)
		{
			Value = value;
		}

		public static readonly WallConnectionTypeNorth None = new WallConnectionTypeNorth("none");
		public static readonly WallConnectionTypeNorth Short = new WallConnectionTypeNorth("short");
		public static readonly WallConnectionTypeNorth Tall = new WallConnectionTypeNorth("tall");

		public static WallConnectionTypeNorth[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class WallConnectionTypeSouth : BlockStateString
	{
		public override string Name => "wall_connection_type_south";

		protected WallConnectionTypeSouth(string value)
		{
			Value = value;
		}

		public static readonly WallConnectionTypeSouth None = new WallConnectionTypeSouth("none");
		public static readonly WallConnectionTypeSouth Short = new WallConnectionTypeSouth("short");
		public static readonly WallConnectionTypeSouth Tall = new WallConnectionTypeSouth("tall");

		public static WallConnectionTypeSouth[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class WallConnectionTypeWest : BlockStateString
	{
		public override string Name => "wall_connection_type_west";

		protected WallConnectionTypeWest(string value)
		{
			Value = value;
		}

		public static readonly WallConnectionTypeWest None = new WallConnectionTypeWest("none");
		public static readonly WallConnectionTypeWest Short = new WallConnectionTypeWest("short");
		public static readonly WallConnectionTypeWest Tall = new WallConnectionTypeWest("tall");

		public static WallConnectionTypeWest[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class WallPostBit : BlockStateByte
	{
		public override string Name => "wall_post_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class PoweredBit : BlockStateByte
	{
		public override string Name => "powered_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class SpongeType : BlockStateString
	{
		public override string Name => "sponge_type";

		protected SpongeType(string value)
		{
			Value = value;
		}

		public static readonly SpongeType Dry = new SpongeType("dry");
		public static readonly SpongeType Wet = new SpongeType("wet");

		public static SpongeType[] Values()
		{
			return [Dry, Wet];
		}

	} // class

	public partial class RespawnAnchorCharge : BlockStateInt
	{
		public override string Name => "respawn_anchor_charge";

		public static int MaxValue { get; } = 4;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4];
		}

	} // class

	public partial class Deprecated : BlockStateInt
	{
		public override string Name => "deprecated";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class PersistentBit : BlockStateByte
	{
		public override string Name => "persistent_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class TorchFacingDirection : BlockStateString
	{
		public override string Name => "torch_facing_direction";

		protected TorchFacingDirection(string value)
		{
			Value = value;
		}

		public static readonly TorchFacingDirection Unknown = new TorchFacingDirection("unknown");
		public static readonly TorchFacingDirection West = new TorchFacingDirection("west");
		public static readonly TorchFacingDirection East = new TorchFacingDirection("east");
		public static readonly TorchFacingDirection North = new TorchFacingDirection("north");
		public static readonly TorchFacingDirection South = new TorchFacingDirection("south");
		public static readonly TorchFacingDirection Top = new TorchFacingDirection("top");

		public static TorchFacingDirection[] Values()
		{
			return [Unknown, West, East, North, South, Top];
		}

	} // class

	public partial class VineDirectionBits : BlockStateInt
	{
		public override string Name => "vine_direction_bits";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class BrushedProgress : BlockStateInt
	{
		public override string Name => "brushed_progress";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class WallBlockType : BlockStateString
	{
		public override string Name => "wall_block_type";

		protected WallBlockType(string value)
		{
			Value = value;
		}

		public static readonly WallBlockType Cobblestone = new WallBlockType("cobblestone");
		public static readonly WallBlockType MossyCobblestone = new WallBlockType("mossy_cobblestone");
		public static readonly WallBlockType Granite = new WallBlockType("granite");
		public static readonly WallBlockType Diorite = new WallBlockType("diorite");
		public static readonly WallBlockType Andesite = new WallBlockType("andesite");
		public static readonly WallBlockType Sandstone = new WallBlockType("sandstone");
		public static readonly WallBlockType Brick = new WallBlockType("brick");
		public static readonly WallBlockType StoneBrick = new WallBlockType("stone_brick");
		public static readonly WallBlockType MossyStoneBrick = new WallBlockType("mossy_stone_brick");
		public static readonly WallBlockType NetherBrick = new WallBlockType("nether_brick");
		public static readonly WallBlockType EndBrick = new WallBlockType("end_brick");
		public static readonly WallBlockType Prismarine = new WallBlockType("prismarine");
		public static readonly WallBlockType RedSandstone = new WallBlockType("red_sandstone");
		public static readonly WallBlockType RedNetherBrick = new WallBlockType("red_nether_brick");

		public static WallBlockType[] Values()
		{
			return [Cobblestone, MossyCobblestone, Granite, Diorite, Andesite, Sandstone, Brick, StoneBrick, MossyStoneBrick, NetherBrick, EndBrick, Prismarine, RedSandstone, RedNetherBrick];
		}

	} // class

	public partial class FacingDirection : BlockStateString
	{
		public override string Name => "minecraft:facing_direction";

		protected FacingDirection(string value)
		{
			Value = value;
		}

		public static readonly FacingDirection Down = new FacingDirection("down");
		public static readonly FacingDirection Up = new FacingDirection("up");
		public static readonly FacingDirection North = new FacingDirection("north");
		public static readonly FacingDirection South = new FacingDirection("south");
		public static readonly FacingDirection West = new FacingDirection("west");
		public static readonly FacingDirection East = new FacingDirection("east");

		public static FacingDirection[] Values()
		{
			return [Down, Up, North, South, West, East];
		}

	} // class

	public partial class Stability : BlockStateInt
	{
		public override string Name => "stability";

		public static int MaxValue { get; } = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}

	} // class

	public partial class StabilityCheck : BlockStateByte
	{
		public override string Name => "stability_check";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class HugeMushroomBits : BlockStateInt
	{
		public override string Name => "huge_mushroom_bits";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class Bloom : BlockStateByte
	{
		public override string Name => "bloom";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BambooLeafSize : BlockStateString
	{
		public override string Name => "bamboo_leaf_size";

		protected BambooLeafSize(string value)
		{
			Value = value;
		}

		public static readonly BambooLeafSize NoLeaves = new BambooLeafSize("no_leaves");
		public static readonly BambooLeafSize SmallLeaves = new BambooLeafSize("small_leaves");
		public static readonly BambooLeafSize LargeLeaves = new BambooLeafSize("large_leaves");

		public static BambooLeafSize[] Values()
		{
			return [NoLeaves, SmallLeaves, LargeLeaves];
		}

	} // class

	public partial class BambooStalkThickness : BlockStateString
	{
		public override string Name => "bamboo_stalk_thickness";

		protected BambooStalkThickness(string value)
		{
			Value = value;
		}

		public static readonly BambooStalkThickness Thin = new BambooStalkThickness("thin");
		public static readonly BambooStalkThickness Thick = new BambooStalkThickness("thick");

		public static BambooStalkThickness[] Values()
		{
			return [Thin, Thick];
		}

	} // class

	public partial class LiquidDepth : BlockStateInt
	{
		public override string Name => "liquid_depth";

		public static int MaxValue { get; } = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}

	} // class

	public partial class MoisturizedAmount : BlockStateInt
	{
		public override string Name => "moisturized_amount";

		public static int MaxValue { get; } = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}

	} // class

	public partial class StrippedBit : BlockStateByte
	{
		public override string Name => "stripped_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class StructureVoidType : BlockStateString
	{
		public override string Name => "structure_void_type";

		protected StructureVoidType(string value)
		{
			Value = value;
		}

		public static readonly StructureVoidType Void = new StructureVoidType("void");
		public static readonly StructureVoidType Air = new StructureVoidType("air");

		public static StructureVoidType[] Values()
		{
			return [Void, Air];
		}

	} // class

	public partial class SculkSensorPhase : BlockStateInt
	{
		public override string Name => "sculk_sensor_phase";

		public static int MaxValue { get; } = 2;

		public static int[] Values()
		{
			return [0, 1, 2];
		}

	} // class

	public partial class RepeaterDelay : BlockStateInt
	{
		public override string Name => "repeater_delay";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class GrowingPlantAge : BlockStateInt
	{
		public override string Name => "growing_plant_age";

		public static int MaxValue { get; } = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}

	} // class

	public partial class Rotation : BlockStateInt
	{
		public override string Name => "rotation";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class ComposterFillLevel : BlockStateInt
	{
		public override string Name => "composter_fill_level";

		public static int MaxValue { get; } = 8;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8];
		}

	} // class

	public partial class KelpAge : BlockStateInt
	{
		public override string Name => "kelp_age";

		public static int MaxValue { get; } = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}

	} // class

	public partial class WeepingVinesAge : BlockStateInt
	{
		public override string Name => "weeping_vines_age";

		public static int MaxValue { get; } = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}

	} // class

	public partial class MultiFaceDirectionBits : BlockStateInt
	{
		public override string Name => "multi_face_direction_bits";

		public static int MaxValue { get; } = 63;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63];
		}

	} // class

	public partial class TwistingVinesAge : BlockStateInt
	{
		public override string Name => "twisting_vines_age";

		public static int MaxValue { get; } = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}

	} // class

	public partial class HoneyLevel : BlockStateInt
	{
		public override string Name => "honey_level";

		public static int MaxValue { get; } = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}

	} // class

	public partial class DragDown : BlockStateByte
	{
		public override string Name => "drag_down";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Extinguished : BlockStateByte
	{
		public override string Name => "extinguished";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ClusterCount : BlockStateInt
	{
		public override string Name => "cluster_count";

		public static int MaxValue { get; } = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}

	} // class

	public partial class DeadBit : BlockStateByte
	{
		public override string Name => "dead_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BigDripleafHead : BlockStateByte
	{
		public override string Name => "big_dripleaf_head";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BigDripleafTilt : BlockStateString
	{
		public override string Name => "big_dripleaf_tilt";

		protected BigDripleafTilt(string value)
		{
			Value = value;
		}

		public static readonly BigDripleafTilt None = new BigDripleafTilt("none");
		public static readonly BigDripleafTilt Unstable = new BigDripleafTilt("unstable");
		public static readonly BigDripleafTilt PartialTilt = new BigDripleafTilt("partial_tilt");
		public static readonly BigDripleafTilt FullTilt = new BigDripleafTilt("full_tilt");

		public static BigDripleafTilt[] Values()
		{
			return [None, Unstable, PartialTilt, FullTilt];
		}

	} // class

	public partial class EndPortalEyeBit : BlockStateByte
	{
		public override string Name => "end_portal_eye_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Ominous : BlockStateByte
	{
		public override string Name => "ominous";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class VaultState : BlockStateString
	{
		public override string Name => "vault_state";

		protected VaultState(string value)
		{
			Value = value;
		}

		public static readonly VaultState Inactive = new VaultState("inactive");
		public static readonly VaultState Active = new VaultState("active");
		public static readonly VaultState Unlocking = new VaultState("unlocking");
		public static readonly VaultState Ejecting = new VaultState("ejecting");

		public static VaultState[] Values()
		{
			return [Inactive, Active, Unlocking, Ejecting];
		}

	} // class

	public partial class StructureBlockType : BlockStateString
	{
		public override string Name => "structure_block_type";

		protected StructureBlockType(string value)
		{
			Value = value;
		}

		public static readonly StructureBlockType Data = new StructureBlockType("data");
		public static readonly StructureBlockType Save = new StructureBlockType("save");
		public static readonly StructureBlockType Load = new StructureBlockType("load");
		public static readonly StructureBlockType Corner = new StructureBlockType("corner");
		public static readonly StructureBlockType Invalid = new StructureBlockType("invalid");
		public static readonly StructureBlockType Export = new StructureBlockType("export");

		public static StructureBlockType[] Values()
		{
			return [Data, Save, Load, Corner, Invalid, Export];
		}

	} // class

	public partial class LeverDirection : BlockStateString
	{
		public override string Name => "lever_direction";

		protected LeverDirection(string value)
		{
			Value = value;
		}

		public static readonly LeverDirection DownEastWest = new LeverDirection("down_east_west");
		public static readonly LeverDirection East = new LeverDirection("east");
		public static readonly LeverDirection West = new LeverDirection("west");
		public static readonly LeverDirection South = new LeverDirection("south");
		public static readonly LeverDirection North = new LeverDirection("north");
		public static readonly LeverDirection UpNorthSouth = new LeverDirection("up_north_south");
		public static readonly LeverDirection UpEastWest = new LeverDirection("up_east_west");
		public static readonly LeverDirection DownNorthSouth = new LeverDirection("down_north_south");

		public static LeverDirection[] Values()
		{
			return [DownEastWest, East, West, South, North, UpNorthSouth, UpEastWest, DownNorthSouth];
		}

	} // class

	public partial class ConditionalBit : BlockStateByte
	{
		public override string Name => "conditional_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class HeadPieceBit : BlockStateByte
	{
		public override string Name => "head_piece_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OccupiedBit : BlockStateByte
	{
		public override string Name => "occupied_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class AllowUnderwaterBit : BlockStateByte
	{
		public override string Name => "allow_underwater_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ExplodeBit : BlockStateByte
	{
		public override string Name => "explode_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ToggleBit : BlockStateByte
	{
		public override string Name => "toggle_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Attachment : BlockStateString
	{
		public override string Name => "attachment";

		protected Attachment(string value)
		{
			Value = value;
		}

		public static readonly Attachment Standing = new Attachment("standing");
		public static readonly Attachment Hanging = new Attachment("hanging");
		public static readonly Attachment Side = new Attachment("side");
		public static readonly Attachment Multiple = new Attachment("multiple");

		public static Attachment[] Values()
		{
			return [Standing, Hanging, Side, Multiple];
		}

	} // class

	public partial class PropaguleStage : BlockStateInt
	{
		public override string Name => "propagule_stage";

		public static int MaxValue { get; } = 4;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4];
		}

	} // class

	public partial class CrackedState : BlockStateString
	{
		public override string Name => "cracked_state";

		protected CrackedState(string value)
		{
			Value = value;
		}

		public static readonly CrackedState NoCracks = new CrackedState("no_cracks");
		public static readonly CrackedState Cracked = new CrackedState("cracked");
		public static readonly CrackedState MaxCracked = new CrackedState("max_cracked");

		public static CrackedState[] Values()
		{
			return [NoCracks, Cracked, MaxCracked];
		}

	} // class

	public partial class InfiniburnBit : BlockStateByte
	{
		public override string Name => "infiniburn_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BiteCounter : BlockStateInt
	{
		public override string Name => "bite_counter";

		public static int MaxValue { get; } = 6;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6];
		}

	} // class

	public partial class ChemistryTableType : BlockStateString
	{
		public override string Name => "chemistry_table_type";

		protected ChemistryTableType(string value)
		{
			Value = value;
		}

		public static readonly ChemistryTableType CompoundCreator = new ChemistryTableType("compound_creator");
		public static readonly ChemistryTableType MaterialReducer = new ChemistryTableType("material_reducer");
		public static readonly ChemistryTableType ElementConstructor = new ChemistryTableType("element_constructor");
		public static readonly ChemistryTableType LabTable = new ChemistryTableType("lab_table");

		public static ChemistryTableType[] Values()
		{
			return [CompoundCreator, MaterialReducer, ElementConstructor, LabTable];
		}

	} // class

	public partial class TriggeredBit : BlockStateByte
	{
		public override string Name => "triggered_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class DripstoneThickness : BlockStateString
	{
		public override string Name => "dripstone_thickness";

		protected DripstoneThickness(string value)
		{
			Value = value;
		}

		public static readonly DripstoneThickness Tip = new DripstoneThickness("tip");
		public static readonly DripstoneThickness Frustum = new DripstoneThickness("frustum");
		public static readonly DripstoneThickness Middle = new DripstoneThickness("middle");
		public static readonly DripstoneThickness Base = new DripstoneThickness("base");
		public static readonly DripstoneThickness Merge = new DripstoneThickness("merge");

		public static DripstoneThickness[] Values()
		{
			return [Tip, Frustum, Middle, Base, Merge];
		}

	} // class

	public partial class DisarmedBit : BlockStateByte
	{
		public override string Name => "disarmed_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class SuspendedBit : BlockStateByte
	{
		public override string Name => "suspended_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CauldronLiquid : BlockStateString
	{
		public override string Name => "cauldron_liquid";

		protected CauldronLiquid(string value)
		{
			Value = value;
		}

		public static readonly CauldronLiquid Water = new CauldronLiquid("water");
		public static readonly CauldronLiquid Lava = new CauldronLiquid("lava");
		public static readonly CauldronLiquid PowderSnow = new CauldronLiquid("powder_snow");

		public static CauldronLiquid[] Values()
		{
			return [Water, Lava, PowderSnow];
		}

	} // class

	public partial class FillLevel : BlockStateInt
	{
		public override string Name => "fill_level";

		public static int MaxValue { get; } = 6;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6];
		}

	} // class

	public partial class ColorBit : BlockStateByte
	{
		public override string Name => "color_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrewingStandSlotABit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_a_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrewingStandSlotBBit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_b_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrewingStandSlotCBit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_c_bit";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ChiselType : BlockStateString
	{
		public override string Name => "chisel_type";

		protected ChiselType(string value)
		{
			Value = value;
		}

		public static readonly ChiselType Default = new ChiselType("default");
		public static readonly ChiselType Chiseled = new ChiselType("chiseled");
		public static readonly ChiselType Lines = new ChiselType("lines");
		public static readonly ChiselType Smooth = new ChiselType("smooth");

		public static ChiselType[] Values()
		{
			return [Default, Chiseled, Lines, Smooth];
		}

	} // class

	public partial class PortalAxis : BlockStateString
	{
		public override string Name => "portal_axis";

		protected PortalAxis(string value)
		{
			Value = value;
		}

		public static readonly PortalAxis Unknown = new PortalAxis("unknown");
		public static readonly PortalAxis X = new PortalAxis("x");
		public static readonly PortalAxis Z = new PortalAxis("z");

		public static PortalAxis[] Values()
		{
			return [Unknown, X, Z];
		}

	} // class

	public partial class Crafting : BlockStateByte
	{
		public override string Name => "crafting";

		public static byte MaxValue { get; } = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Orientation : BlockStateString
	{
		public override string Name => "orientation";

		protected Orientation(string value)
		{
			Value = value;
		}

		public static readonly Orientation DownEast = new Orientation("down_east");
		public static readonly Orientation DownNorth = new Orientation("down_north");
		public static readonly Orientation DownSouth = new Orientation("down_south");
		public static readonly Orientation DownWest = new Orientation("down_west");
		public static readonly Orientation UpEast = new Orientation("up_east");
		public static readonly Orientation UpNorth = new Orientation("up_north");
		public static readonly Orientation UpSouth = new Orientation("up_south");
		public static readonly Orientation UpWest = new Orientation("up_west");
		public static readonly Orientation WestUp = new Orientation("west_up");
		public static readonly Orientation EastUp = new Orientation("east_up");
		public static readonly Orientation NorthUp = new Orientation("north_up");
		public static readonly Orientation SouthUp = new Orientation("south_up");

		public static Orientation[] Values()
		{
			return [DownEast, DownNorth, DownSouth, DownWest, UpEast, UpNorth, UpSouth, UpWest, WestUp, EastUp, NorthUp, SouthUp];
		}

	} // class

	public partial class TurtleEggCount : BlockStateString
	{
		public override string Name => "turtle_egg_count";

		protected TurtleEggCount(string value)
		{
			Value = value;
		}

		public static readonly TurtleEggCount OneEgg = new TurtleEggCount("one_egg");
		public static readonly TurtleEggCount TwoEgg = new TurtleEggCount("two_egg");
		public static readonly TurtleEggCount ThreeEgg = new TurtleEggCount("three_egg");
		public static readonly TurtleEggCount FourEgg = new TurtleEggCount("four_egg");

		public static TurtleEggCount[] Values()
		{
			return [OneEgg, TwoEgg, ThreeEgg, FourEgg];
		}

	} // class

	public partial class TrialSpawnerState : BlockStateInt
	{
		public override string Name => "trial_spawner_state";

		public static int MaxValue { get; } = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}

	} // class
}
