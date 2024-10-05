using Direction = MiNET.Utils.Direction;

namespace MiNET.Blocks.States
{
	public partial class LeverDirection
	{
		public static implicit operator LeverDirection(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.Up => UpNorthSouth,
				MiNET.BlockFace.Down => DownNorthSouth,
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.East => East,
				_ => South
			};
		}

		public static LeverDirection FromFaceAndDirections(MiNET.BlockFace face, Direction direction)
		{
			return (face, direction) switch
			{
				(MiNET.BlockFace.Down, Direction.North or Direction.South) => DownNorthSouth,
				(MiNET.BlockFace.Down, Direction.East or Direction.West) => DownEastWest,

				(MiNET.BlockFace.Up, Direction.North or Direction.South) => UpNorthSouth,
				(MiNET.BlockFace.Up, Direction.East or Direction.West) => UpEastWest,

				_ => face
			};
		}
	}
}
