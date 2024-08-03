using System.Linq;
using MiNET.Items;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class SignBase : Block
	{
		protected SignBase() : base()
		{
			IsTransparent = true;
			IsSolid = false;
			BlastResistance = 5;
			Hardness = 1;

			IsFlammable = true; // Only in PE!!
		}

		public override Item GetItem(Level world, bool blockItem = false)
		{
			switch (this)
			{
				case StandingSign:
				case WallSign:
					return new ItemOakSign();
			}

			var idSplit = Id.Split('_');
			var itemId = $"{string.Join('_', idSplit.Take(idSplit.Length - 2))}_{idSplit.Last()}";
			itemId = itemId.Replace("darkoak", "dark_oak");

			return ItemFactory.GetItem(itemId);
		}
	}
}
