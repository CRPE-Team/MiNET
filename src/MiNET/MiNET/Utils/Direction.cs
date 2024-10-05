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
				_ => Direction.South
			};
		}

		public static Direction Shift(this Direction direction)
		{
			return (Direction) (((int) direction + 1) & 0x03);
		}
	}
}