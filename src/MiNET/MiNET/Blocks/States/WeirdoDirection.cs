namespace MiNET.Blocks.States
{
	public partial class WeirdoDirection
	{
		internal WeirdoDirection() { }

		private WeirdoDirection(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly WeirdoDirection East = new WeirdoDirection(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly WeirdoDirection West = new WeirdoDirection(1);

		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly WeirdoDirection South = new WeirdoDirection(2);

		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly WeirdoDirection North = new WeirdoDirection(3);

		public static implicit operator WeirdoDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.East => East,
				_ => North
			};
		}
	}
}
