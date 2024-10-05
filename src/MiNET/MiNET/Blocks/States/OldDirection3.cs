namespace MiNET.Blocks.States
{
	public class OldDirection3 : OldDirection
	{
		internal OldDirection3() { }

		private OldDirection3(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = 0
		/// </summary>
		public static readonly OldDirection3 East = new OldDirection3(0);

		/// <summary>
		/// Value = 1
		/// </summary>
		public static readonly OldDirection3 South = new OldDirection3(1);

		/// <summary>
		/// Value = 2
		/// </summary>
		public static readonly OldDirection3 West = new OldDirection3(2);

		/// <summary>
		/// Value = 3
		/// </summary>
		public static readonly OldDirection3 North = new OldDirection3(3);


		public static implicit operator OldDirection3(MiNET.Utils.Direction direction)
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
