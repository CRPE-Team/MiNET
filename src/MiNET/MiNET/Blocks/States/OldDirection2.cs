namespace MiNET.Blocks.States
{
	public class OldDirection2 : OldDirection
	{
		internal OldDirection2() { }

		private OldDirection2(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly OldDirection2 North = new OldDirection2(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly OldDirection2 East = new OldDirection2(1);

		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly OldDirection2 South = new OldDirection2(2);

		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly OldDirection2 West = new OldDirection2(3);

		public static implicit operator OldDirection2(MiNET.Utils.Direction direction)
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
