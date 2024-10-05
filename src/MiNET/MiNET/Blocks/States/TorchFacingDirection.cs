namespace MiNET.Blocks.States
{
	public partial class TorchFacingDirection
	{
		public static implicit operator TorchFacingDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.North => South,
				MiNET.Utils.Direction.South => North,
				MiNET.Utils.Direction.West => East,
				MiNET.Utils.Direction.East => West,
				_ => Top
			};
		}

		public static implicit operator MiNET.Utils.Direction(TorchFacingDirection direction)
		{
			return direction.Value switch
			{
				NorthValue => MiNET.Utils.Direction.South,
				SouthValue => MiNET.Utils.Direction.North,
				WestValue => MiNET.Utils.Direction.East,
				EastValue => MiNET.Utils.Direction.West,
				_ => MiNET.Utils.Direction.North
			};
		}

		public static implicit operator TorchFacingDirection(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.North => South,
				MiNET.BlockFace.South => North,
				MiNET.BlockFace.West => East,
				MiNET.BlockFace.East => West,
				_ => Top
			};
		}

		public static implicit operator MiNET.BlockFace(TorchFacingDirection direction)
		{
			return direction.Value switch
			{
				NorthValue => MiNET.BlockFace.South,
				SouthValue => MiNET.BlockFace.North,
				WestValue => MiNET.BlockFace.East,
				EastValue => MiNET.BlockFace.West,
				_ => MiNET.BlockFace.Up
			};
		}
	}
}
