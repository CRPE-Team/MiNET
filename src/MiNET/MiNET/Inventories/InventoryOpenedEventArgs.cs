namespace MiNET.Inventories
{
	public class InventoryOpenedEventArgs : InventoryEventArgs
	{
		public bool Opened { get; }

		public InventoryOpenedEventArgs(Player player, Inventory inventory, bool opened)
			: base(player, inventory)
		{
			Opened = opened;
		}
	}
}
