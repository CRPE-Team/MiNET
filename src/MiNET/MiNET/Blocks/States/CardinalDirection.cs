namespace MiNET.Blocks.States
{
	public partial class CardinalDirection
	{
		public static implicit operator CardinalDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.East => East,
				_ => South
			};
		}
		public static implicit operator MiNET.Utils.Direction(CardinalDirection direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.Utils.Direction.South,
				WestValue => MiNET.Utils.Direction.West,
				NorthValue => MiNET.Utils.Direction.North,
				EastValue => MiNET.Utils.Direction.East,
				_ => MiNET.Utils.Direction.South
			};
		}
	}
}
