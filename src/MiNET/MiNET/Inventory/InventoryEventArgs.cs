namespace MiNET.Inventory
{
	public class InventoryEventArgs : PlayerEventArgs
	{
		public ContainerInventory Inventory { get; }

		public InventoryEventArgs(Player player, ContainerInventory inventory)
			: base(player)
		{
			Inventory = inventory;
		}
	}
}
