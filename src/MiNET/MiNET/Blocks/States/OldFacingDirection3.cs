namespace MiNET.Blocks.States
{
	public class OldFacingDirection3 : OldFacingDirection
	{
		internal OldFacingDirection3() { }

		private OldFacingDirection3(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly OldFacingDirection3 Down = new OldFacingDirection3(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly OldFacingDirection3 Up = new OldFacingDirection3(1);

		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly OldFacingDirection3 South = new OldFacingDirection3(2);

		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly OldFacingDirection3 North = new OldFacingDirection3(3);

		/// <summary>
		/// Value = 4
		/// </summary>
		public static readonly OldFacingDirection3 East = new OldFacingDirection3(4);

		/// <summary>
		/// Value = 5
		/// </summary>
		public static readonly OldFacingDirection3 West = new OldFacingDirection3(5);

		public static implicit operator OldFacingDirection3(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.East => East,
				_ => Down
			};
		}

		public static implicit operator OldFacingDirection3(MiNET.BlockFace blockFace)
		{
			return blockFace switch
			{
				MiNET.BlockFace.Down => Down,
				MiNET.BlockFace.Up => Up,
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.East => East,
				_ => Down
			};
		}
	}
}
