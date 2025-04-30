using MiNET.Utils;

namespace MiNET.Blocks.States
{

	public partial class Active : BlockStateByte
	{
		public override string Name => "active";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Age : BlockStateInt
	{
		public override string Name => "age";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class AgeBit : BlockStateByte
	{
		public override string Name => "age_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class AttachedBit : BlockStateByte
	{
		public override string Name => "attached_bit";

		public const byte MaxValue = 1;

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

		protected const string StandingValue = "standing";
		protected const string HangingValue = "hanging";
		protected const string SideValue = "side";
		protected const string MultipleValue = "multiple";

		public static readonly Attachment Standing = new Attachment(StandingValue);
		public static readonly Attachment Hanging = new Attachment(HangingValue);
		public static readonly Attachment Side = new Attachment(SideValue);
		public static readonly Attachment Multiple = new Attachment(MultipleValue);

		public static Attachment[] Values()
		{
			return [Standing, Hanging, Side, Multiple];
		}

	} // class

	public partial class BambooLeafSize : BlockStateString
	{
		public override string Name => "bamboo_leaf_size";

		protected BambooLeafSize(string value)
		{
			Value = value;
		}

		protected const string NoLeavesValue = "no_leaves";
		protected const string SmallLeavesValue = "small_leaves";
		protected const string LargeLeavesValue = "large_leaves";

		public static readonly BambooLeafSize NoLeaves = new BambooLeafSize(NoLeavesValue);
		public static readonly BambooLeafSize SmallLeaves = new BambooLeafSize(SmallLeavesValue);
		public static readonly BambooLeafSize LargeLeaves = new BambooLeafSize(LargeLeavesValue);

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

		protected const string ThinValue = "thin";
		protected const string ThickValue = "thick";

		public static readonly BambooStalkThickness Thin = new BambooStalkThickness(ThinValue);
		public static readonly BambooStalkThickness Thick = new BambooStalkThickness(ThickValue);

		public static BambooStalkThickness[] Values()
		{
			return [Thin, Thick];
		}

	} // class

	public partial class BigDripleafHead : BlockStateByte
	{
		public override string Name => "big_dripleaf_head";

		public const byte MaxValue = 1;

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

		protected const string NoneValue = "none";
		protected const string UnstableValue = "unstable";
		protected const string PartialTiltValue = "partial_tilt";
		protected const string FullTiltValue = "full_tilt";

		public static readonly BigDripleafTilt None = new BigDripleafTilt(NoneValue);
		public static readonly BigDripleafTilt Unstable = new BigDripleafTilt(UnstableValue);
		public static readonly BigDripleafTilt PartialTilt = new BigDripleafTilt(PartialTiltValue);
		public static readonly BigDripleafTilt FullTilt = new BigDripleafTilt(FullTiltValue);

		public static BigDripleafTilt[] Values()
		{
			return [None, Unstable, PartialTilt, FullTilt];
		}

	} // class

	public partial class BiteCounter : BlockStateInt
	{
		public override string Name => "bite_counter";

		public const int MaxValue = 6;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Bloom : BlockStateByte
	{
		public override string Name => "bloom";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BooksStored : BlockStateInt
	{
		public override string Name => "books_stored";

		public const int MaxValue = 63;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class BrewingStandSlotABit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_a_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrewingStandSlotBBit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_b_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrewingStandSlotCBit : BlockStateByte
	{
		public override string Name => "brewing_stand_slot_c_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BrushedProgress : BlockStateInt
	{
		public override string Name => "brushed_progress";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class ButtonPressedBit : BlockStateByte
	{
		public override string Name => "button_pressed_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CanSummon : BlockStateByte
	{
		public override string Name => "can_summon";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Candles : BlockStateInt
	{
		public override string Name => "candles";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class CauldronLiquid : BlockStateString
	{
		public override string Name => "cauldron_liquid";

		protected CauldronLiquid(string value)
		{
			Value = value;
		}

		protected const string WaterValue = "water";
		protected const string LavaValue = "lava";
		protected const string PowderSnowValue = "powder_snow";

		public static readonly CauldronLiquid Water = new CauldronLiquid(WaterValue);
		public static readonly CauldronLiquid Lava = new CauldronLiquid(LavaValue);
		public static readonly CauldronLiquid PowderSnow = new CauldronLiquid(PowderSnowValue);

		public static CauldronLiquid[] Values()
		{
			return [Water, Lava, PowderSnow];
		}

	} // class

	public partial class ClusterCount : BlockStateInt
	{
		public override string Name => "cluster_count";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class ComposterFillLevel : BlockStateInt
	{
		public override string Name => "composter_fill_level";

		public const int MaxValue = 8;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class ConditionalBit : BlockStateByte
	{
		public override string Name => "conditional_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CoralDirection : BlockStateInt
	{
		public override string Name => "coral_direction";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class CoralFanDirection : BlockStateInt
	{
		public override string Name => "coral_fan_direction";

		public const int MaxValue = 1;

		public static int[] Values()
		{
			return [0, 1];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class CoveredBit : BlockStateByte
	{
		public override string Name => "covered_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CrackedState : BlockStateString
	{
		public override string Name => "cracked_state";

		protected CrackedState(string value)
		{
			Value = value;
		}

		protected const string NoCracksValue = "no_cracks";
		protected const string CrackedValue = "cracked";
		protected const string MaxCrackedValue = "max_cracked";

		public static readonly CrackedState NoCracks = new CrackedState(NoCracksValue);
		public static readonly CrackedState Cracked = new CrackedState(CrackedValue);
		public static readonly CrackedState MaxCracked = new CrackedState(MaxCrackedValue);

		public static CrackedState[] Values()
		{
			return [NoCracks, Cracked, MaxCracked];
		}

	} // class

	public partial class Crafting : BlockStateByte
	{
		public override string Name => "crafting";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class CreakingHeartState : BlockStateString
	{
		public override string Name => "creaking_heart_state";

		protected CreakingHeartState(string value)
		{
			Value = value;
		}

		protected const string UprootedValue = "uprooted";
		protected const string DormantValue = "dormant";
		protected const string AwakeValue = "awake";

		public static readonly CreakingHeartState Uprooted = new CreakingHeartState(UprootedValue);
		public static readonly CreakingHeartState Dormant = new CreakingHeartState(DormantValue);
		public static readonly CreakingHeartState Awake = new CreakingHeartState(AwakeValue);

		public static CreakingHeartState[] Values()
		{
			return [Uprooted, Dormant, Awake];
		}

	} // class

	public partial class DeadBit : BlockStateByte
	{
		public override string Name => "dead_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Deprecated : BlockStateInt
	{
		public override string Name => "deprecated";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class OldDirection : BlockStateInt
	{
		public override string Name => "direction";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class DisarmedBit : BlockStateByte
	{
		public override string Name => "disarmed_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class DoorHingeBit : BlockStateByte
	{
		public override string Name => "door_hinge_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class DragDown : BlockStateByte
	{
		public override string Name => "drag_down";

		public const byte MaxValue = 1;

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

		protected const string TipValue = "tip";
		protected const string FrustumValue = "frustum";
		protected const string MiddleValue = "middle";
		protected const string BaseValue = "base";
		protected const string MergeValue = "merge";

		public static readonly DripstoneThickness Tip = new DripstoneThickness(TipValue);
		public static readonly DripstoneThickness Frustum = new DripstoneThickness(FrustumValue);
		public static readonly DripstoneThickness Middle = new DripstoneThickness(MiddleValue);
		public static readonly DripstoneThickness Base = new DripstoneThickness(BaseValue);
		public static readonly DripstoneThickness Merge = new DripstoneThickness(MergeValue);

		public static DripstoneThickness[] Values()
		{
			return [Tip, Frustum, Middle, Base, Merge];
		}

	} // class

	public partial class EndPortalEyeBit : BlockStateByte
	{
		public override string Name => "end_portal_eye_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ExplodeBit : BlockStateByte
	{
		public override string Name => "explode_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Extinguished : BlockStateByte
	{
		public override string Name => "extinguished";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OldFacingDirection : BlockStateInt
	{
		public override string Name => "facing_direction";

		public const int MaxValue = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class FillLevel : BlockStateInt
	{
		public override string Name => "fill_level";

		public const int MaxValue = 6;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class GroundSignDirection : BlockStateInt
	{
		public override string Name => "ground_sign_direction";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class GrowingPlantAge : BlockStateInt
	{
		public override string Name => "growing_plant_age";

		public const int MaxValue = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Growth : BlockStateInt
	{
		public override string Name => "growth";

		public const int MaxValue = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Hanging : BlockStateByte
	{
		public override string Name => "hanging";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class HeadPieceBit : BlockStateByte
	{
		public override string Name => "head_piece_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Height : BlockStateInt
	{
		public override string Name => "height";

		public const int MaxValue = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class HoneyLevel : BlockStateInt
	{
		public override string Name => "honey_level";

		public const int MaxValue = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class HugeMushroomBits : BlockStateInt
	{
		public override string Name => "huge_mushroom_bits";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class InWallBit : BlockStateByte
	{
		public override string Name => "in_wall_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class InfiniburnBit : BlockStateByte
	{
		public override string Name => "infiniburn_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ItemFrameMapBit : BlockStateByte
	{
		public override string Name => "item_frame_map_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ItemFramePhotoBit : BlockStateByte
	{
		public override string Name => "item_frame_photo_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class KelpAge : BlockStateInt
	{
		public override string Name => "kelp_age";

		public const int MaxValue = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class LeverDirection : BlockStateString
	{
		public override string Name => "lever_direction";

		protected LeverDirection(string value)
		{
			Value = value;
		}

		protected const string DownEastWestValue = "down_east_west";
		protected const string EastValue = "east";
		protected const string WestValue = "west";
		protected const string SouthValue = "south";
		protected const string NorthValue = "north";
		protected const string UpNorthSouthValue = "up_north_south";
		protected const string UpEastWestValue = "up_east_west";
		protected const string DownNorthSouthValue = "down_north_south";

		public static readonly LeverDirection DownEastWest = new LeverDirection(DownEastWestValue);
		public static readonly LeverDirection East = new LeverDirection(EastValue);
		public static readonly LeverDirection West = new LeverDirection(WestValue);
		public static readonly LeverDirection South = new LeverDirection(SouthValue);
		public static readonly LeverDirection North = new LeverDirection(NorthValue);
		public static readonly LeverDirection UpNorthSouth = new LeverDirection(UpNorthSouthValue);
		public static readonly LeverDirection UpEastWest = new LeverDirection(UpEastWestValue);
		public static readonly LeverDirection DownNorthSouth = new LeverDirection(DownNorthSouthValue);

		public static LeverDirection[] Values()
		{
			return [DownEastWest, East, West, South, North, UpNorthSouth, UpEastWest, DownNorthSouth];
		}

	} // class

	public partial class LiquidDepth : BlockStateInt
	{
		public override string Name => "liquid_depth";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Lit : BlockStateByte
	{
		public override string Name => "lit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class BlockFace : BlockStateString
	{
		public override string Name => "minecraft:block_face";

		protected BlockFace(string value)
		{
			Value = value;
		}

		protected const string DownValue = "down";
		protected const string UpValue = "up";
		protected const string NorthValue = "north";
		protected const string SouthValue = "south";
		protected const string WestValue = "west";
		protected const string EastValue = "east";

		public static readonly BlockFace Down = new BlockFace(DownValue);
		public static readonly BlockFace Up = new BlockFace(UpValue);
		public static readonly BlockFace North = new BlockFace(NorthValue);
		public static readonly BlockFace South = new BlockFace(SouthValue);
		public static readonly BlockFace West = new BlockFace(WestValue);
		public static readonly BlockFace East = new BlockFace(EastValue);

		public static BlockFace[] Values()
		{
			return [Down, Up, North, South, West, East];
		}

	} // class

	public partial class CardinalDirection : BlockStateString
	{
		public override string Name => "minecraft:cardinal_direction";

		protected CardinalDirection(string value)
		{
			Value = value;
		}

		protected const string SouthValue = "south";
		protected const string WestValue = "west";
		protected const string NorthValue = "north";
		protected const string EastValue = "east";

		public static readonly CardinalDirection South = new CardinalDirection(SouthValue);
		public static readonly CardinalDirection West = new CardinalDirection(WestValue);
		public static readonly CardinalDirection North = new CardinalDirection(NorthValue);
		public static readonly CardinalDirection East = new CardinalDirection(EastValue);

		public static CardinalDirection[] Values()
		{
			return [South, West, North, East];
		}

	} // class

	public partial class FacingDirection : BlockStateString
	{
		public override string Name => "minecraft:facing_direction";

		protected FacingDirection(string value)
		{
			Value = value;
		}

		protected const string DownValue = "down";
		protected const string UpValue = "up";
		protected const string NorthValue = "north";
		protected const string SouthValue = "south";
		protected const string WestValue = "west";
		protected const string EastValue = "east";

		public static readonly FacingDirection Down = new FacingDirection(DownValue);
		public static readonly FacingDirection Up = new FacingDirection(UpValue);
		public static readonly FacingDirection North = new FacingDirection(NorthValue);
		public static readonly FacingDirection South = new FacingDirection(SouthValue);
		public static readonly FacingDirection West = new FacingDirection(WestValue);
		public static readonly FacingDirection East = new FacingDirection(EastValue);

		public static FacingDirection[] Values()
		{
			return [Down, Up, North, South, West, East];
		}

	} // class

	public partial class VerticalHalf : BlockStateString
	{
		public override string Name => "minecraft:vertical_half";

		protected VerticalHalf(string value)
		{
			Value = value;
		}

		protected const string BottomValue = "bottom";
		protected const string TopValue = "top";

		public static readonly VerticalHalf Bottom = new VerticalHalf(BottomValue);
		public static readonly VerticalHalf Top = new VerticalHalf(TopValue);

		public static VerticalHalf[] Values()
		{
			return [Bottom, Top];
		}

	} // class

	public partial class MoisturizedAmount : BlockStateInt
	{
		public override string Name => "moisturized_amount";

		public const int MaxValue = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class MultiFaceDirectionBits : BlockStateInt
	{
		public override string Name => "multi_face_direction_bits";

		public const int MaxValue = 63;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Natural : BlockStateByte
	{
		public override string Name => "natural";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OccupiedBit : BlockStateByte
	{
		public override string Name => "occupied_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Ominous : BlockStateByte
	{
		public override string Name => "ominous";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OpenBit : BlockStateByte
	{
		public override string Name => "open_bit";

		public const byte MaxValue = 1;

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

		protected const string DownEastValue = "down_east";
		protected const string DownNorthValue = "down_north";
		protected const string DownSouthValue = "down_south";
		protected const string DownWestValue = "down_west";
		protected const string UpEastValue = "up_east";
		protected const string UpNorthValue = "up_north";
		protected const string UpSouthValue = "up_south";
		protected const string UpWestValue = "up_west";
		protected const string WestUpValue = "west_up";
		protected const string EastUpValue = "east_up";
		protected const string NorthUpValue = "north_up";
		protected const string SouthUpValue = "south_up";

		public static readonly Orientation DownEast = new Orientation(DownEastValue);
		public static readonly Orientation DownNorth = new Orientation(DownNorthValue);
		public static readonly Orientation DownSouth = new Orientation(DownSouthValue);
		public static readonly Orientation DownWest = new Orientation(DownWestValue);
		public static readonly Orientation UpEast = new Orientation(UpEastValue);
		public static readonly Orientation UpNorth = new Orientation(UpNorthValue);
		public static readonly Orientation UpSouth = new Orientation(UpSouthValue);
		public static readonly Orientation UpWest = new Orientation(UpWestValue);
		public static readonly Orientation WestUp = new Orientation(WestUpValue);
		public static readonly Orientation EastUp = new Orientation(EastUpValue);
		public static readonly Orientation NorthUp = new Orientation(NorthUpValue);
		public static readonly Orientation SouthUp = new Orientation(SouthUpValue);

		public static Orientation[] Values()
		{
			return [DownEast, DownNorth, DownSouth, DownWest, UpEast, UpNorth, UpSouth, UpWest, WestUp, EastUp, NorthUp, SouthUp];
		}

	} // class

	public partial class OutputLitBit : BlockStateByte
	{
		public override string Name => "output_lit_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class OutputSubtractBit : BlockStateByte
	{
		public override string Name => "output_subtract_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class PaleMossCarpetSideEast : BlockStateString
	{
		public override string Name => "pale_moss_carpet_side_east";

		protected PaleMossCarpetSideEast(string value)
		{
			Value = value;
		}

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly PaleMossCarpetSideEast None = new PaleMossCarpetSideEast(NoneValue);
		public static readonly PaleMossCarpetSideEast Short = new PaleMossCarpetSideEast(ShortValue);
		public static readonly PaleMossCarpetSideEast Tall = new PaleMossCarpetSideEast(TallValue);

		public static PaleMossCarpetSideEast[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class PaleMossCarpetSideNorth : BlockStateString
	{
		public override string Name => "pale_moss_carpet_side_north";

		protected PaleMossCarpetSideNorth(string value)
		{
			Value = value;
		}

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly PaleMossCarpetSideNorth None = new PaleMossCarpetSideNorth(NoneValue);
		public static readonly PaleMossCarpetSideNorth Short = new PaleMossCarpetSideNorth(ShortValue);
		public static readonly PaleMossCarpetSideNorth Tall = new PaleMossCarpetSideNorth(TallValue);

		public static PaleMossCarpetSideNorth[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class PaleMossCarpetSideSouth : BlockStateString
	{
		public override string Name => "pale_moss_carpet_side_south";

		protected PaleMossCarpetSideSouth(string value)
		{
			Value = value;
		}

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly PaleMossCarpetSideSouth None = new PaleMossCarpetSideSouth(NoneValue);
		public static readonly PaleMossCarpetSideSouth Short = new PaleMossCarpetSideSouth(ShortValue);
		public static readonly PaleMossCarpetSideSouth Tall = new PaleMossCarpetSideSouth(TallValue);

		public static PaleMossCarpetSideSouth[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class PaleMossCarpetSideWest : BlockStateString
	{
		public override string Name => "pale_moss_carpet_side_west";

		protected PaleMossCarpetSideWest(string value)
		{
			Value = value;
		}

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly PaleMossCarpetSideWest None = new PaleMossCarpetSideWest(NoneValue);
		public static readonly PaleMossCarpetSideWest Short = new PaleMossCarpetSideWest(ShortValue);
		public static readonly PaleMossCarpetSideWest Tall = new PaleMossCarpetSideWest(TallValue);

		public static PaleMossCarpetSideWest[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class PersistentBit : BlockStateByte
	{
		public override string Name => "persistent_bit";

		public const byte MaxValue = 1;

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

		protected const string YValue = "y";
		protected const string XValue = "x";
		protected const string ZValue = "z";

		public static readonly PillarAxis Y = new PillarAxis(YValue);
		public static readonly PillarAxis X = new PillarAxis(XValue);
		public static readonly PillarAxis Z = new PillarAxis(ZValue);

		public static PillarAxis[] Values()
		{
			return [Y, X, Z];
		}

	} // class

	public partial class PortalAxis : BlockStateString
	{
		public override string Name => "portal_axis";

		protected PortalAxis(string value)
		{
			Value = value;
		}

		protected const string UnknownValue = "unknown";
		protected const string XValue = "x";
		protected const string ZValue = "z";

		public static readonly PortalAxis Unknown = new PortalAxis(UnknownValue);
		public static readonly PortalAxis X = new PortalAxis(XValue);
		public static readonly PortalAxis Z = new PortalAxis(ZValue);

		public static PortalAxis[] Values()
		{
			return [Unknown, X, Z];
		}

	} // class

	public partial class PoweredBit : BlockStateByte
	{
		public override string Name => "powered_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class PropaguleStage : BlockStateInt
	{
		public override string Name => "propagule_stage";

		public const int MaxValue = 4;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class RailDataBit : BlockStateByte
	{
		public override string Name => "rail_data_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class RailDirection : BlockStateInt
	{
		public override string Name => "rail_direction";

		public const int MaxValue = 9;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class RedstoneSignal : BlockStateInt
	{
		public override string Name => "redstone_signal";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class RepeaterDelay : BlockStateInt
	{
		public override string Name => "repeater_delay";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class RespawnAnchorCharge : BlockStateInt
	{
		public override string Name => "respawn_anchor_charge";

		public const int MaxValue = 4;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class Rotation : BlockStateInt
	{
		public override string Name => "rotation";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class SculkSensorPhase : BlockStateInt
	{
		public override string Name => "sculk_sensor_phase";

		public const int MaxValue = 2;

		public static int[] Values()
		{
			return [0, 1, 2];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class SeaGrassType : BlockStateString
	{
		public override string Name => "sea_grass_type";

		protected SeaGrassType(string value)
		{
			Value = value;
		}

		protected const string DefaultValue = "default";
		protected const string DoubleTopValue = "double_top";
		protected const string DoubleBotValue = "double_bot";

		public static readonly SeaGrassType Default = new SeaGrassType(DefaultValue);
		public static readonly SeaGrassType DoubleTop = new SeaGrassType(DoubleTopValue);
		public static readonly SeaGrassType DoubleBot = new SeaGrassType(DoubleBotValue);

		public static SeaGrassType[] Values()
		{
			return [Default, DoubleTop, DoubleBot];
		}

	} // class

	public partial class Stability : BlockStateInt
	{
		public override string Name => "stability";

		public const int MaxValue = 7;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class StabilityCheck : BlockStateByte
	{
		public override string Name => "stability_check";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class StructureBlockType : BlockStateString
	{
		public override string Name => "structure_block_type";

		protected StructureBlockType(string value)
		{
			Value = value;
		}

		protected const string DataValue = "data";
		protected const string SaveValue = "save";
		protected const string LoadValue = "load";
		protected const string CornerValue = "corner";
		protected const string InvalidValue = "invalid";
		protected const string ExportValue = "export";

		public static readonly StructureBlockType Data = new StructureBlockType(DataValue);
		public static readonly StructureBlockType Save = new StructureBlockType(SaveValue);
		public static readonly StructureBlockType Load = new StructureBlockType(LoadValue);
		public static readonly StructureBlockType Corner = new StructureBlockType(CornerValue);
		public static readonly StructureBlockType Invalid = new StructureBlockType(InvalidValue);
		public static readonly StructureBlockType Export = new StructureBlockType(ExportValue);

		public static StructureBlockType[] Values()
		{
			return [Data, Save, Load, Corner, Invalid, Export];
		}

	} // class

	public partial class SuspendedBit : BlockStateByte
	{
		public override string Name => "suspended_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class Tip : BlockStateByte
	{
		public override string Name => "tip";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class ToggleBit : BlockStateByte
	{
		public override string Name => "toggle_bit";

		public const byte MaxValue = 1;

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

		protected const string UnknownValue = "unknown";
		protected const string WestValue = "west";
		protected const string EastValue = "east";
		protected const string NorthValue = "north";
		protected const string SouthValue = "south";
		protected const string TopValue = "top";

		public static readonly TorchFacingDirection Unknown = new TorchFacingDirection(UnknownValue);
		public static readonly TorchFacingDirection West = new TorchFacingDirection(WestValue);
		public static readonly TorchFacingDirection East = new TorchFacingDirection(EastValue);
		public static readonly TorchFacingDirection North = new TorchFacingDirection(NorthValue);
		public static readonly TorchFacingDirection South = new TorchFacingDirection(SouthValue);
		public static readonly TorchFacingDirection Top = new TorchFacingDirection(TopValue);

		public static TorchFacingDirection[] Values()
		{
			return [Unknown, West, East, North, South, Top];
		}

	} // class

	public partial class TrialSpawnerState : BlockStateInt
	{
		public override string Name => "trial_spawner_state";

		public const int MaxValue = 5;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class TriggeredBit : BlockStateByte
	{
		public override string Name => "triggered_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class TurtleEggCount : BlockStateString
	{
		public override string Name => "turtle_egg_count";

		protected TurtleEggCount(string value)
		{
			Value = value;
		}

		protected const string OneEggValue = "one_egg";
		protected const string TwoEggValue = "two_egg";
		protected const string ThreeEggValue = "three_egg";
		protected const string FourEggValue = "four_egg";

		public static readonly TurtleEggCount OneEgg = new TurtleEggCount(OneEggValue);
		public static readonly TurtleEggCount TwoEgg = new TurtleEggCount(TwoEggValue);
		public static readonly TurtleEggCount ThreeEgg = new TurtleEggCount(ThreeEggValue);
		public static readonly TurtleEggCount FourEgg = new TurtleEggCount(FourEggValue);

		public static TurtleEggCount[] Values()
		{
			return [OneEgg, TwoEgg, ThreeEgg, FourEgg];
		}

	} // class

	public partial class TwistingVinesAge : BlockStateInt
	{
		public override string Name => "twisting_vines_age";

		public const int MaxValue = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class UpdateBit : BlockStateByte
	{
		public override string Name => "update_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class UpperBlockBit : BlockStateByte
	{
		public override string Name => "upper_block_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class UpsideDownBit : BlockStateByte
	{
		public override string Name => "upside_down_bit";

		public const byte MaxValue = 1;

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

		protected const string InactiveValue = "inactive";
		protected const string ActiveValue = "active";
		protected const string UnlockingValue = "unlocking";
		protected const string EjectingValue = "ejecting";

		public static readonly VaultState Inactive = new VaultState(InactiveValue);
		public static readonly VaultState Active = new VaultState(ActiveValue);
		public static readonly VaultState Unlocking = new VaultState(UnlockingValue);
		public static readonly VaultState Ejecting = new VaultState(EjectingValue);

		public static VaultState[] Values()
		{
			return [Inactive, Active, Unlocking, Ejecting];
		}

	} // class

	public partial class VineDirectionBits : BlockStateInt
	{
		public override string Name => "vine_direction_bits";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class WallConnectionTypeEast : BlockStateString
	{
		public override string Name => "wall_connection_type_east";

		protected WallConnectionTypeEast(string value)
		{
			Value = value;
		}

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly WallConnectionTypeEast None = new WallConnectionTypeEast(NoneValue);
		public static readonly WallConnectionTypeEast Short = new WallConnectionTypeEast(ShortValue);
		public static readonly WallConnectionTypeEast Tall = new WallConnectionTypeEast(TallValue);

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

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly WallConnectionTypeNorth None = new WallConnectionTypeNorth(NoneValue);
		public static readonly WallConnectionTypeNorth Short = new WallConnectionTypeNorth(ShortValue);
		public static readonly WallConnectionTypeNorth Tall = new WallConnectionTypeNorth(TallValue);

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

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly WallConnectionTypeSouth None = new WallConnectionTypeSouth(NoneValue);
		public static readonly WallConnectionTypeSouth Short = new WallConnectionTypeSouth(ShortValue);
		public static readonly WallConnectionTypeSouth Tall = new WallConnectionTypeSouth(TallValue);

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

		protected const string NoneValue = "none";
		protected const string ShortValue = "short";
		protected const string TallValue = "tall";

		public static readonly WallConnectionTypeWest None = new WallConnectionTypeWest(NoneValue);
		public static readonly WallConnectionTypeWest Short = new WallConnectionTypeWest(ShortValue);
		public static readonly WallConnectionTypeWest Tall = new WallConnectionTypeWest(TallValue);

		public static WallConnectionTypeWest[] Values()
		{
			return [None, Short, Tall];
		}

	} // class

	public partial class WallPostBit : BlockStateByte
	{
		public override string Name => "wall_post_bit";

		public const byte MaxValue = 1;

		public static byte[] Values()
		{
			return [0, 1];
		}

	} // class

	public partial class WeepingVinesAge : BlockStateInt
	{
		public override string Name => "weeping_vines_age";

		public const int MaxValue = 25;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class

	public partial class WeirdoDirection : BlockStateInt
	{
		public override string Name => "weirdo_direction";

		public const int MaxValue = 3;

		public static int[] Values()
		{
			return [0, 1, 2, 3];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}

	} // class
}
