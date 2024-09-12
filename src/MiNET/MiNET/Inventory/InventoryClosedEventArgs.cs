namespace MiNET.Inventory
{
	public class InventoryClosedEventArgs : InventoryEventArgs
	{
		public bool Closed { get; }

		public InventoryClosedEventArgs(Player player, ContainerInventory inventory, bool closed)
			: base(player, inventory)
		{
			Closed = closed;
		}
	}
}
