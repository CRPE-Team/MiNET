using System;
using MiNET.Items;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public partial class DarkOakLeaves : LeavesBase
	{
		public override Item[] GetDrops(Level world, Item tool)
		{
			if (new Random().Next(200) == 0)
			{
				return [new ItemApple()];
			}

			return base.GetDrops(world, tool);
		}
	}
}
