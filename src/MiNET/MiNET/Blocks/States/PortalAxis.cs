using System;

namespace MiNET.Blocks.States
{
	public partial class PortalAxis
	{
		public static implicit operator PortalAxis(BlockAxis axis)
		{
			return axis switch
			{
				BlockAxis.X => X,
				BlockAxis.Y => Unknown,
				BlockAxis.Z => Z,
				_ => Unknown
			};
		}

		public static implicit operator BlockAxis(PortalAxis axis)
		{
			return axis.Value switch
			{
				XValue => BlockAxis.X,
				UnknownValue => BlockAxis.Y,
				ZValue => BlockAxis.Z,
				_ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
			};
		}
	}
}
