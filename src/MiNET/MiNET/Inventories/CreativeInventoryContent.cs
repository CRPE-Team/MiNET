using System.Collections.Generic;
using System.Linq;
using MiNET.Items;
using MiNET.Net;

namespace MiNET.Inventories
{
	public class CreativeInventoryContent : IPacketDataObject
	{
		private int _runtimeIdCounter = 1;
		private uint _groupIdCounter = 0;
		private List<CreativeInventoryGroupItem> _items = new List<CreativeInventoryGroupItem>();
		private Dictionary<CreativeInventoryCategoryType, CreativeInventoryCategory> _creativeInventoryCategories = new();

		public CreativeInventoryContent()
		{
			_items.Add(new CreativeInventoryGroupItem() { Item = new ItemAir() });
		}

		public void AppendCategory(CreativeInventoryCategoryType type, ExternalDataCategory data)
		{
			var category = new CreativeInventoryCategory() { Type = type };

			foreach (var dataGroup in data)
			{
				var group = new CreativeInventoryGroup()
				{
					CategoryType = category.Type,
					Name = dataGroup.Name
				};

				if (dataGroup.IconItem != null)
				{
					if (!InventoryUtils.TryGetItemFromExternalData(dataGroup.IconItem, out var iconItem))
					{
						continue;
					}

					group.IconItem = iconItem;
				}

				foreach (var dataItem in dataGroup.Items)
				{
					if (InventoryUtils.TryGetItemFromExternalData(dataItem, out var item))
					{
						item.UniqueId = _runtimeIdCounter++;
						group.Items.Add(item);

						_items.Add(new CreativeInventoryGroupItem()
						{
							Item = item,
							GroupIndex = _groupIdCounter,
						});
					}
				}

				if (!group.Items.Any())
				{
					continue;
				}

				_groupIdCounter++;
				category.Groups.Add(group);
			}

			_creativeInventoryCategories.Add(category.Type, category);
		}

		public Dictionary<CreativeInventoryCategoryType, CreativeInventoryCategory> GetCategories()
		{
			return new Dictionary<CreativeInventoryCategoryType, CreativeInventoryCategory>(_creativeInventoryCategories);
		}

		public Item GetItemById(uint creativeId)
		{
			return _items.ElementAtOrDefault((int) creativeId)?.Item;
		}

		public void Write(Packet packet)
		{
			packet.WriteLength((int) _groupIdCounter);
			foreach (var category in _creativeInventoryCategories.Values.ToArray())
			{
				foreach (var group in category.Groups.ToArray())
				{
					packet.Write(group);
				}
			}

			packet.WriteLength(_items.Count - 1);
			foreach (var item in _items.Skip(1))
			{
				packet.Write(item);
			}
		}

		public static CreativeInventoryContent Read(Packet packet)
		{
			var content = new CreativeInventoryContent();

			var groups = new CreativeInventoryGroup[packet.ReadLength()];
			for (int i = 0; i < groups.Length; i++)
			{
				var group = groups[i] = CreativeInventoryGroup.Read(packet);
				
				if (!content._creativeInventoryCategories.TryGetValue(group.CategoryType, out var category))
				{
					content._creativeInventoryCategories.Add(group.CategoryType, category = new CreativeInventoryCategory()
					{
						Type = group.CategoryType
					});
				}

				category.Groups.Add(group);
			}

			var items = new CreativeInventoryGroupItem[packet.ReadLength()];
			for (var i = 0; i < items.Length; i++)
			{
				var item = CreativeInventoryGroupItem.Read(packet);
				content._items.Add(item);
				groups[item.GroupIndex].Items.Add(item.Item);
			}

			return content;
		}
	}
}
