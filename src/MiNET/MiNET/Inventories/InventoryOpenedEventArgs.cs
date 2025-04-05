namespace MiNET.Inventories
{
	public class InventoryOpenedEventArgs : InventoryEventArgs
	{
		public bool Opened { get; }

		public InventoryOpenedEventArgs(Player player, IInventory inventory, bool opened)
			: base(player, inventory)
		{
			Opened = opened;
		}
	}
}
