using System;

namespace MiNET.Blocks.States
{
	public partial class CoralFanDirection
	{
		internal CoralFanDirection() { }

		private CoralFanDirection(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly CoralFanDirection EastWest = new CoralFanDirection(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly CoralFanDirection NorthSouth = new CoralFanDirection(1);

		public static implicit operator CoralFanDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => NorthSouth,
				MiNET.Utils.Direction.West => EastWest,
				MiNET.Utils.Direction.North => NorthSouth,
				MiNET.Utils.Direction.East => EastWest,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static implicit operator CoralFanDirection(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.South => NorthSouth,
				MiNET.BlockFace.West => EastWest,
				MiNET.BlockFace.North => NorthSouth,
				MiNET.BlockFace.East => EastWest,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}
	}
}
