namespace MiNET.Inventories
{
	public class InventoryOpenEventArgs : InventoryEventArgs
	{
		public bool Open { get; }

		public bool Cancel { get; set; }

		public InventoryOpenEventArgs(Player player, Inventory inventory, bool open)
			: base(player, inventory)
		{
			Open = open;
		}
	}
}
