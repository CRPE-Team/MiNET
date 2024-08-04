using MiNET.Blocks;

namespace MiNET.Items
{
	public abstract class ItemHangingSignBase : ItemSignBase
	{
		protected ItemHangingSignBase() : base()
		{
			Block = BlockFactory.GetBlockById(Id);
		}

		protected override bool SetupSignBlock(BlockFace face)
		{
			// do nothing
			return true;
		}
	}
}
