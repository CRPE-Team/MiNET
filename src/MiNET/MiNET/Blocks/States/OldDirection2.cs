namespace MiNET.Blocks.States
{
	public class OldDirection2 : OldDirection
	{
		/// <summary>0</summary>
		private const int NorthValue = 0;
		/// <summary>1</summary>
		private const int EastValue = 1;
		/// <summary>2</summary>
		private const int SouthValue = 2;
		/// <summary>3</summary>
		private const int WestValue = 3;

		internal OldDirection2() { }

		private OldDirection2(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = <inheritdoc cref="SouthValue"/>
		/// </summary>
		public static readonly OldDirection2 South = new OldDirection2(SouthValue);

		/// <summary>
		/// Value = <inheritdoc cref="WestValue"/>
		/// </summary>
		public static readonly OldDirection2 West = new OldDirection2(WestValue);

		/// <summary>
		/// Value = <inheritdoc cref="NorthValue"/>
		/// </summary>
		public static readonly OldDirection2 North = new OldDirection2(NorthValue);

		/// <summary>
		/// Value = <inheritdoc cref="EastValue"/>
		/// </summary>
		public static readonly OldDirection2 East = new OldDirection2(EastValue);

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

		public static implicit operator OldDirection2(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.East => East,
				_ => North
			};
		}

		public static implicit operator MiNET.BlockFace(OldDirection2 direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				NorthValue => MiNET.BlockFace.North,
				EastValue => MiNET.BlockFace.East,
				_ => MiNET.BlockFace.North
			};
		}

		public static implicit operator MiNET.Utils.Direction(OldDirection2 direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.Utils.Direction.South,
				WestValue => MiNET.Utils.Direction.West,
				NorthValue => MiNET.Utils.Direction.North,
				EastValue => MiNET.Utils.Direction.East,
				_ => MiNET.Utils.Direction.North
			};
		}
	}
}
