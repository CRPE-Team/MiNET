using System;

namespace MiNET.Blocks.States
{
	public partial class TorchFacingDirection
	{
		public static implicit operator TorchFacingDirection(MiNET.Utils.Direction direction)
		{
			return direction switch
			{
				MiNET.Utils.Direction.North => North,
				MiNET.Utils.Direction.South => South,
				MiNET.Utils.Direction.West => West,
				MiNET.Utils.Direction.East => East,
				_ => Top
			};
		}

		public static implicit operator MiNET.Utils.Direction(TorchFacingDirection direction)
		{
			return direction.Value switch
			{
				NorthValue => MiNET.Utils.Direction.North,
				SouthValue => MiNET.Utils.Direction.South,
				WestValue => MiNET.Utils.Direction.West,
				EastValue => MiNET.Utils.Direction.East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static implicit operator TorchFacingDirection(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.East => East,
				_ => Top
			};
		}

		public static implicit operator MiNET.BlockFace(TorchFacingDirection direction)
		{
			return direction.Value switch
			{
				TopValue => MiNET.BlockFace.Up,
				NorthValue => MiNET.BlockFace.North,
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				EastValue => MiNET.BlockFace.East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}
