using MiNET.Inventories;
using MiNET.Net;
using MiNET.Utils;

namespace MiNET.BlockEntities
{
	public abstract class ContainerBlockEntity : ContainerBlockEntityBase
	{
		protected ContainerBlockEntity(string id) 
			: base(id, 27, WindowType.Container)
		{
			
		}

		protected override void OnInventoryOpened(object sender, InventoryOpenedEventArgs args)
		{
			base.OnInventoryOpened(sender, args);

			if (args.Opened)
			{
				var tileEvent = McpeBlockEvent.CreateObject();
				tileEvent.coordinates = Coordinates;
				tileEvent.case1 = 1;
				tileEvent.case2 = 2;
				args.Player.Level.RelayBroadcast(tileEvent);
			}
		}

		protected override void OnInventoryClosed(object sender, InventoryClosedEventArgs args)
		{
			base.OnInventoryClosed(sender, args);

			if (args.Closed)
			{
				var tileEvent = McpeBlockEvent.CreateObject();
				tileEvent.coordinates = Coordinates;
				tileEvent.case1 = 1;
				tileEvent.case2 = 0;
				args.Player.Level.RelayBroadcast(tileEvent);
			}
		}
	}
}