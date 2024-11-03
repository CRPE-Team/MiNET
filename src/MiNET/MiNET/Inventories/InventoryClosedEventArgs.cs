namespace MiNET.Inventories
{
	public class InventoryClosedEventArgs : InventoryEventArgs
	{
		public bool Closed { get; }

		public InventoryClosedEventArgs(Player player, Inventory inventory, bool closed)
			: base(player, inventory)
		{
			Closed = closed;
		}
	}
}
