namespace MiNET.Inventories
{
	public class InventoryOpenEventArgs : InventoryEventArgs
	{
		public bool Open { get; }

		public bool Cancel { get; set; }

		public InventoryOpenEventArgs(Player player, IInventory inventory, bool open)
			: base(player, inventory)
		{
			Open = open;
		}
	}
}
