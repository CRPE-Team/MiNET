namespace MiNET.Inventory
{
	public class InventoryOpenedEventArgs : InventoryEventArgs
	{
		public bool Opened { get; }

		public InventoryOpenedEventArgs(Player player, ContainerInventory inventory, bool opened)
			: base(player, inventory)
		{
			Opened = opened;
		}
	}
}
