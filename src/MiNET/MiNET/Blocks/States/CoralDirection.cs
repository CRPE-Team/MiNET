using System;

namespace MiNET.Blocks.States
{
	public partial class CoralDirection
	{
		internal CoralDirection() { }

		private CoralDirection(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly CoralDirection West = new CoralDirection(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly CoralDirection East = new CoralDirection(1);

		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly CoralDirection North = new CoralDirection(2);
		
		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly CoralDirection South = new CoralDirection(3);

		public static implicit operator CoralDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.East => East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static implicit operator CoralDirection(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.East => East,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}
	}
}
