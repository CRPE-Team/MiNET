using MiNET.Blocks;

namespace MiNET.Items
{
	public abstract class ItemHangingSignBase : ItemSignBase
	{
		protected ItemHangingSignBase() : base()
		{
			Block = BlockFactory.GetBlockById(Id);
		}

		protected override void SetupSignBlock(BlockFace face)
		{
			// do nothing
		}
	}
}
