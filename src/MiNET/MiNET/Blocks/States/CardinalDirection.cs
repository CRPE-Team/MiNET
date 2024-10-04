using MiNET.Utils.Vectors;

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
	}
}
