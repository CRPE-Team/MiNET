using System;
using System.Collections.Generic;
using System.IO;
using MiNET.Blocks;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;

namespace MiNET.Inventory
{
	public static class InventoryUtils
	{
		public static List<Item> CreativeInventoryItems { get; } = new List<Item>();

		private static McpeCreativeContent _creativeInventoryData;
		private static readonly bool _isEduEnabled;

		static InventoryUtils()
		{
			_isEduEnabled = Config.GetProperty("EnableEdu", false);

			var creativeItems = ResourceUtil.ReadResource<List<ExternalDataItem>>("creativeitems.json", typeof(InventoryUtils), "Data");

			CreativeInventoryItems.Add(new ItemAir());

			var uniqueId = 1;
			foreach (var itemData in creativeItems)
			{
				if (TryGetItemFromExternalData(itemData, out var item))
				{
					item.UniqueId = uniqueId++;
					CreativeInventoryItems.Add(item);
				}
			}
		}

		public static McpeCreativeContent GetCreativeInventoryData()
		{
			if (_creativeInventoryData == null)
			{
				var creativeContent = McpeCreativeContent.CreateObject();
				creativeContent.input = GetCreativeMetadataSlots();
				creativeContent.MarkPermanent(true);
				_creativeInventoryData = creativeContent;
			}

			return _creativeInventoryData;
		}

		public static CreativeItemStacks GetCreativeMetadataSlots()
		{
			return new CreativeItemStacks(CreativeInventoryItems.ToArray());
		}

		public static bool TryGetItemFromExternalData(ExternalDataItem itemData, out Item result)
		{
			result = null;

			if (string.IsNullOrEmpty(itemData.Id)) return false;

			var item = ItemFactory.GetItem(itemData.Id, itemData.Metadata, (byte) itemData.Count);
			if (item is ItemAir) return false;
			if (item.Edu && !_isEduEnabled) return false;

			if (itemData.BlockStates != null && item is ItemBlock itemBlock)
			{
				var bytes = Convert.FromBase64String(itemData.BlockStates);

				using MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length);
				var compound = Packet.ReadNbtCompound(memoryStream, false);

				itemBlock.Block.SetStates(BlockFactory.GetBlockStates(compound));
			}

			if (itemData.ExtraData != null)
			{
				var bytes = Convert.FromBase64String(itemData.ExtraData);

				using MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length);
				item.ExtraData = Packet.ReadNbtCompound(memoryStream, false);
			}

			item.Metadata = itemData.Metadata;

			result = item;
			return true;
		}
	}
}