namespace MiNET.Blocks.States
{
	public class OldFacingDirection1 : OldFacingDirection
	{
		internal OldFacingDirection1() { }

		private OldFacingDirection1(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly OldFacingDirection1 Down = new OldFacingDirection1(0);
		
		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly OldFacingDirection1 Up = new OldFacingDirection1(1);
		
		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly OldFacingDirection1 North = new OldFacingDirection1(2);
		
		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly OldFacingDirection1 South = new OldFacingDirection1(3);
		
		/// <summary>
		/// Value = 4
		/// </summary>
		public static readonly OldFacingDirection1 East = new OldFacingDirection1(4);
		
		/// <summary>
		/// Value = 5
		/// </summary>
		public static readonly OldFacingDirection1 West = new OldFacingDirection1(5);

		public static implicit operator OldFacingDirection1(MiNET.Utils.Direction direction)
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

		public static implicit operator OldFacingDirection1(MiNET.BlockFace blockFace)
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
