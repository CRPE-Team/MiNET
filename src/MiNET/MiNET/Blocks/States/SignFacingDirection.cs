namespace MiNET.Blocks.States
{
	public class SignFacingDirection : OldFacingDirection
	{
		internal SignFacingDirection() { }

		private SignFacingDirection(int value)
		{
			Value = value;
		}

		public static readonly SignFacingDirection Down = new SignFacingDirection(0);
		public static readonly SignFacingDirection Up = new SignFacingDirection(1);
		public static readonly SignFacingDirection North = new SignFacingDirection(2);
		public static readonly SignFacingDirection South = new SignFacingDirection(3);
		public static readonly SignFacingDirection West = new SignFacingDirection(4);
		public static readonly SignFacingDirection East = new SignFacingDirection(5);

		public static implicit operator SignFacingDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => West,
				MiNET.Utils.Direction.West => North,
				MiNET.Utils.Direction.North => East,
				MiNET.Utils.Direction.East => South,
				_ => Down
			};
		}

		public static implicit operator SignFacingDirection(MiNET.BlockFace blockFace)
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
