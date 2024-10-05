namespace MiNET.Blocks.States
{
	public class OldFacingDirection4 : OldFacingDirection
	{
		internal OldFacingDirection4() { }

		private OldFacingDirection4(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly OldFacingDirection4 Down = new OldFacingDirection4(0);
		
		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly OldFacingDirection4 Up = new OldFacingDirection4(1);
		
		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly OldFacingDirection4 North = new OldFacingDirection4(2);
		
		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly OldFacingDirection4 South = new OldFacingDirection4(3);
		
		/// <summary>
		/// Value = 4
		/// </summary>
		public static readonly OldFacingDirection4 West = new OldFacingDirection4(4);
		
		/// <summary>
		/// Value = 5
		/// </summary>
		public static readonly OldFacingDirection4 East = new OldFacingDirection4(5);

		public static implicit operator OldFacingDirection4(MiNET.Utils.Direction direction)
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

		public static implicit operator OldFacingDirection4(MiNET.BlockFace blockFace)
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
