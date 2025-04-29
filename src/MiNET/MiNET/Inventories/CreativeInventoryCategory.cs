using System.Collections.Generic;

namespace MiNET.Inventories
{
	public class CreativeInventoryCategory
	{
		public CreativeInventoryCategoryType Type { get; set; }

		public List<CreativeInventoryGroup> Groups { get; set; } = new List<CreativeInventoryGroup>();
	}
}
