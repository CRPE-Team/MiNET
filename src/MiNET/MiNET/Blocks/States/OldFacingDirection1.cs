namespace MiNET.Blocks.States
{
	public class OldFacingDirection1 : OldFacingDirection
	{
		/// <summary>0</summary>
		private const int DownValue = 0;
		/// <summary>1</summary>
		private const int UpValue = 1;
		/// <summary>2</summary>
		private const int NorthValue = 2;
		/// <summary>3</summary>
		private const int SouthValue = 3;
		/// <summary>4</summary>
		private const int EastValue = 4;
		/// <summary>5</summary>
		private const int WestValue = 5;


		internal OldFacingDirection1() { }

		private OldFacingDirection1(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = <inheritdoc cref="DownValue"/>
		/// </summary>
		public static readonly OldFacingDirection1 Down = new OldFacingDirection1(DownValue);
		
		/// <summary>
		/// Value = <inheritdoc cref="UpValue"/>
		/// </summary>
		public static readonly OldFacingDirection1 Up = new OldFacingDirection1(UpValue);
		
		/// <summary>
		/// Value = <inheritdoc cref="NorthValue"/>
		/// </summary>
		public static readonly OldFacingDirection1 North = new OldFacingDirection1(NorthValue);
		
		/// <summary>
		/// Value = <inheritdoc cref="SouthValue"/>
		/// </summary>
		public static readonly OldFacingDirection1 South = new OldFacingDirection1(SouthValue);
		
		/// <summary>
		/// Value = <inheritdoc cref="EastValue"/>
		/// </summary>
		public static readonly OldFacingDirection1 East = new OldFacingDirection1(EastValue);
		
		/// <summary>
		/// Value = <inheritdoc cref="WestValue"/>
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

		public static implicit operator MiNET.Utils.Direction(OldFacingDirection1 direction)
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

		public static implicit operator MiNET.BlockFace(OldFacingDirection1 direction)
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
