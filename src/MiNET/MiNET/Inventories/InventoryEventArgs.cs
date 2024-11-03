namespace MiNET.Inventories
{
	public class InventoryEventArgs : PlayerEventArgs
	{
		public Inventory Inventory { get; }

		public InventoryEventArgs(Player player, Inventory inventory)
			: base(player)
		{
			Inventory = inventory;
		}
	}
}
