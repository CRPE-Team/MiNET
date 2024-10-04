namespace MiNET.Blocks.States
{
	public partial class TorchFacingDirection
	{
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
	}
}
