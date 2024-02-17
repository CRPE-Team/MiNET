using MiNET.Items;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class HangingSignBase : SignBase
	{
		public override Item GetItem(Level world, bool blockItem = false)
		{
			return ItemFactory.GetItem(Id);
		}
	}
}
