namespace MiNET.Inventories
{
	public class InventoryEventArgs : PlayerEventArgs
	{
		public IInventory Inventory { get; }

		public InventoryEventArgs(Player player, IInventory inventory)
			: base(player)
		{
			Inventory = inventory;
		}
	}
}
