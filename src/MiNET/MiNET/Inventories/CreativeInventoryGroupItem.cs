using MiNET.Items;
using MiNET.Net;

namespace MiNET.Inventories
{
	public class CreativeInventoryGroupItem : IPacketDataObject
	{
		public uint GroupIndex { get; set; }

		public Item Item { get; set; }

		public void Write(Packet packet)
		{
			packet.WriteUnsignedVarInt((uint) Item.UniqueId);
			packet.Write(Item, false);
			packet.WriteUnsignedVarInt(GroupIndex);
		}

		public static CreativeInventoryGroupItem Read(Packet packet)
		{
			var creativeId = packet.ReadUnsignedVarInt();
			var item = packet.ReadItem(false);
			item.UniqueId = (int) creativeId;

			return new CreativeInventoryGroupItem()
			{
				Item = item,
				GroupIndex = packet.ReadUnsignedVarInt()
			};
		}
	}
}
