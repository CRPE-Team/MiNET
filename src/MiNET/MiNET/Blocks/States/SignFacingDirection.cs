namespace MiNET.Blocks.States
{
	public class SignFacingDirection : OldFacingDirection
	{
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
	}
}
