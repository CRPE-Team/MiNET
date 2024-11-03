using MiNET.Items;

namespace MiNET.Inventories
{
	public class InventoryChangeEventArgs : InventoryEventArgs
	{
		public byte Slot { get; }

		public Item Item { get; }

		public InventoryChangeEventArgs(Player player, Inventory inventory, byte slot, Item item)
			: base(player, inventory)
		{
			Slot = slot;
			Item = item;
		}
	}
}
