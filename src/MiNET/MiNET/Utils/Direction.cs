using System;

namespace MiNET.Utils
{
	public enum Direction
	{
		North = 0,
		East = 1,
		South = 2,
		West = 3,
	}

	public static class DirectionExtensions
	{
		public static Direction Opposite(this Direction direction)
		{
			return direction switch
			{
				Direction.South => Direction.North,
				Direction.West => Direction.East,
				Direction.North => Direction.South,
				Direction.East => Direction.West,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static Direction Shift(this Direction direction)
		{
			return (Direction) (((int) direction + 1) & 0x03);
		}

		public static BlockFace ToBlockFace(this Direction direction)
		{
			return direction switch
			{
				Direction.South => BlockFace.South,
				Direction.West => BlockFace.West,
				Direction.North => BlockFace.North,
				Direction.East => BlockFace.East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}