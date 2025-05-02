using fNbt;
using MiNET.Blocks;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;
using MiNET.Utils.Nbt;

namespace MiNET.Inventories
{
	public static class InventoryUtils
	{
		private static readonly CreativeInventoryCategoryType[] DefaultCategories =
		[
			CreativeInventoryCategoryType.Construction,
			CreativeInventoryCategoryType.Nature,
			CreativeInventoryCategoryType.Equipment,
			CreativeInventoryCategoryType.Items,
		];

		public static CreativeInventoryContent Content { get; } = new CreativeInventoryContent();

		private static McpeCreativeContent _creativeContentData;
		private static McpeItemRegistry _itemRegistryData;
		private static readonly bool _isEduEnabled;

		static InventoryUtils()
		{
			_isEduEnabled = Config.GetProperty("EnableEdu", false);

			foreach (var category in DefaultCategories)
			{
				var data = ResourceUtil.ReadResource<ExternalDataCategory>($"{category.ToString().ToLower()}.json", typeof(InventoryUtils), "Data");
				Content.AppendCategory(category, data);
			}
		}

		public static McpeCreativeContent GetCreativeInventoryData()
		{
			if (_creativeContentData == null)
			{
				var creativeContent = McpeCreativeContent.CreateObject();
				creativeContent.content = Content;
				creativeContent.MarkPermanent(true);
				_creativeContentData = creativeContent;
			}

			return _creativeContentData;
		}

		public static McpeItemRegistry GetItemRegistryData()
		{
			if (_itemRegistryData == null)
			{
				var creativeContent = McpeItemRegistry.CreateObject();
				creativeContent.itemStates = ItemFactory.ItemStates;
				creativeContent.MarkPermanent(true);
				_itemRegistryData = creativeContent;
			}

			return _itemRegistryData;
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
				var compound = NbtExtensions.ReadNbtCompound(itemData.BlockStates, NbtFlavor.BedrockNoVarInt);

				itemBlock.Block.SetStates(BlockFactory.GetBlockStates(compound));
			}

			if (itemData.ExtraData != null)
			{
				item.ExtraData = NbtExtensions.ReadNbtCompound(itemData.ExtraData, NbtFlavor.BedrockNoVarInt);
			}

			item.Metadata = itemData.Metadata;

			result = item;
			return true;
		}
	}
}