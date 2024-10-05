namespace MiNET.Blocks.States
{
	public class OldDirection4 : OldDirection
	{
		/// <summary>0</summary>
		private const int WestValue = 0;
		/// <summary>1</summary>
		private const int EastValue = 1;
		/// <summary>2</summary>
		private const int NorthValue = 2;
		/// <summary>3</summary>
		private const int SouthValue = 3;

		internal OldDirection4() { }

		private OldDirection4(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = <inheritdoc cref="SouthValue"/>
		/// </summary>
		public static readonly OldDirection4 South = new OldDirection4(SouthValue);

		/// <summary>
		/// Value = <inheritdoc cref="WestValue"/>
		/// </summary>
		public static readonly OldDirection4 West = new OldDirection4(WestValue);

		/// <summary>
		/// Value = <inheritdoc cref="NorthValue"/>
		/// </summary>
		public static readonly OldDirection4 North = new OldDirection4(NorthValue);

		/// <summary>
		/// Value = <inheritdoc cref="EastValue"/>
		/// </summary>
		public static readonly OldDirection4 East = new OldDirection4(EastValue);

		public static implicit operator OldDirection4(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.East => East,
				_ => West
			};
		}

		public static implicit operator OldDirection4(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.East => East,
				_ => West
			};
		}

		public static implicit operator MiNET.BlockFace(OldDirection4 direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				NorthValue => MiNET.BlockFace.North,
				EastValue => MiNET.BlockFace.East,
				_ => MiNET.BlockFace.West
			};
		}

		public static implicit operator MiNET.Utils.Direction(OldDirection4 direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.Utils.Direction.South,
				WestValue => MiNET.Utils.Direction.West,
				NorthValue => MiNET.Utils.Direction.North,
				EastValue => MiNET.Utils.Direction.East,
				_ => MiNET.Utils.Direction.West
			};
		}
	}
}
