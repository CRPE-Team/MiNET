using System;

namespace MiNET.Blocks.States
{
	public partial class PillarAxis
	{
		public static implicit operator PillarAxis(BlockAxis axis)
		{
			return axis switch
			{
				BlockAxis.X => X,
				BlockAxis.Y => Y,
				BlockAxis.Z => Z,
				_ => X
			};
		}

		public static implicit operator BlockAxis(PillarAxis axis)
		{
			return axis.Value switch
			{
				XValue => BlockAxis.X,
				YValue => BlockAxis.Y,
				ZValue => BlockAxis.Z,
				_ => default
			};
		}

		public static explicit operator PillarAxis(MiNET.BlockFace face)
		{
			return face switch
			{
				MiNET.BlockFace.Down => Y,
				MiNET.BlockFace.Up => Y,
				MiNET.BlockFace.North => Z,
				MiNET.BlockFace.South => Z,
				MiNET.BlockFace.West => X,
				MiNET.BlockFace.East => X,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}

		public static explicit operator PillarAxis(BlockFace face)
		{
			return (PillarAxis) (MiNET.BlockFace) face;
		}
	}
}
