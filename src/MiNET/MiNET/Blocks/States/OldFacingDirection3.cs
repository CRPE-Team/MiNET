namespace MiNET.Blocks.States
{
	public class OldFacingDirection3 : OldFacingDirection
	{
		/// <summary>0</summary>
		private const int DownValue = 0;
		/// <summary>1</summary>
		private const int UpValue = 1;
		/// <summary>2</summary>
		private const int SouthValue = 2;
		/// <summary>3</summary>
		private const int NorthValue = 3;
		/// <summary>4</summary>
		private const int EastValue = 4;
		/// <summary>5</summary>
		private const int WestValue = 5;

		internal OldFacingDirection3() { }

		private OldFacingDirection3(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = <inheritdoc cref="DownValue"/>
		/// </summary>
		public static readonly OldFacingDirection3 Down = new OldFacingDirection3(DownValue);

		/// <summary>
		/// Value = <inheritdoc cref="UpValue"/>
		/// </summary>
		public static readonly OldFacingDirection3 Up = new OldFacingDirection3(UpValue);

		/// <summary>
		/// Value = <inheritdoc cref="NorthValue"/>
		/// </summary>
		public static readonly OldFacingDirection3 North = new OldFacingDirection3(NorthValue);

		/// <summary>
		/// Value = <inheritdoc cref="SouthValue"/>
		/// </summary>
		public static readonly OldFacingDirection3 South = new OldFacingDirection3(SouthValue);

		/// <summary>
		/// Value = <inheritdoc cref="EastValue"/>
		/// </summary>
		public static readonly OldFacingDirection3 East = new OldFacingDirection3(EastValue);

		/// <summary>
		/// Value = <inheritdoc cref="WestValue"/>
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

		public static implicit operator MiNET.Utils.Direction(OldFacingDirection3 direction)
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

		public static implicit operator MiNET.BlockFace(OldFacingDirection3 direction)
		{
			return direction.Value switch
			{
				DownValue => MiNET.BlockFace.Down,
				UpValue => MiNET.BlockFace.Up,
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				NorthValue => MiNET.BlockFace.North,
				EastValue => MiNET.BlockFace.East,
				_ => MiNET.BlockFace.Down
			};
		}
	}
}
