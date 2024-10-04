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
				UnknownValue => default,
				ZValue => BlockAxis.Z,
				_ => default
			};
		}
	}
}
