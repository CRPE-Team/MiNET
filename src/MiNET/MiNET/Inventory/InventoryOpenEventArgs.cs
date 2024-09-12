namespace MiNET.Inventory
{
	public class InventoryOpenEventArgs : InventoryEventArgs
	{
		public bool Open { get; }

		public bool Cancel { get; set; }

		public InventoryOpenEventArgs(Player player, ContainerInventory inventory, bool open)
			: base(player, inventory)
		{
			Open = open;
		}
	}
}
