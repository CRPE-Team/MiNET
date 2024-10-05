using System;

namespace MiNET.Blocks.States
{
	public partial class BlockFace
	{
		public static implicit operator BlockFace(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.Down => Down,
				MiNET.BlockFace.Up => Up,
				MiNET.BlockFace.North => North,
				MiNET.BlockFace.South => South,
				MiNET.BlockFace.West => West,
				MiNET.BlockFace.East => East,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}

		public static implicit operator MiNET.BlockFace(BlockFace face)
		{
			return face.Value switch
			{
				DownValue => MiNET.BlockFace.Down,
				UpValue => MiNET.BlockFace.Up,
				NorthValue => MiNET.BlockFace.North,
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				EastValue => MiNET.BlockFace.East,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}
	}
}
