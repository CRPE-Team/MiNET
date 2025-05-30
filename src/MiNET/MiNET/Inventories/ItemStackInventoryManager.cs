﻿#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MiNET.Crafting;
using MiNET.Items;
using MiNET.Net;
using MiNET.Worlds;

namespace MiNET.Inventories
{
	public class ItemStackInventoryManager
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ItemStackInventoryManager));

		private readonly Player _player;

		public ItemStackInventoryManager(Player player)
		{
			_player = player;
		}

		public virtual StackResponseStatus HandleItemStackActions(int requestId, ItemStackActionList actions, out List<StackResponseContainerInfo> stackResponses)
		{
			stackResponses = new List<StackResponseContainerInfo>();
			Recipe recipe = null;
			var craftRepetitions = 0;
			List<Item> createCache = null;

			foreach (ItemStackAction stackAction in actions)
				switch (stackAction)
				{
					case CraftAction craftAction:
						{
							if (!ProcessCraftAction(craftAction, out recipe, out craftRepetitions))
							{
								return StackResponseStatus.Error;
							}

							break;
						}
					case CraftCreativeAction craftCreativeAction:
						{
							if (!ProcessCraftCreativeAction(craftCreativeAction, out recipe, out craftRepetitions))
							{
								return StackResponseStatus.Error;
							}

							break;
						}
					case CraftNotImplementedDeprecatedAction craftNotImplementedDeprecatedAction:
						{
							// Do nothing democrafts
							ProcessCraftNotImplementedDeprecatedAction(craftNotImplementedDeprecatedAction);
							break;
						}
					case CraftRecipeOptionalAction craftRecipeOptionalAction:
						{
							ProcessCraftRecipeOptionalAction(craftRecipeOptionalAction);
							break;
						}
					case CraftResultDeprecatedAction craftResultDeprecatedAction:
						{
							if (!ProcessCraftResultDeprecatedAction(craftResultDeprecatedAction, recipe, craftRepetitions, stackResponses, out createCache))
							{
								return StackResponseStatus.Error;
							}

							break;
						}
					case TakeAction takeAction:
						{
							ProcessTakeAction(takeAction, stackResponses);

							break;
						}
					case PlaceAction placeAction:
						{
							ProcessPlaceAction(placeAction, stackResponses);
							break;
						}
					case SwapAction swapAction:
						{
							ProcessSwapAction(swapAction, stackResponses);
							break;
						}
					case DestroyAction destroyAction:
						{
							ProcessDestroyAction(destroyAction, stackResponses);
							break;
						}
					case DropAction dropAction:
						{
							ProcessDropAction(dropAction, stackResponses);

							break;
						}
					case ConsumeAction consumeAction:
						{
							if (recipe == null)
							{
								ProcessConsumeAction(consumeAction, stackResponses);
							}
							break;
						}
					case CreateAction createAction:
						{
							ProcessCreateAction(createAction, createCache);
							break;
						}
					default:
						throw new ArgumentOutOfRangeException(nameof(stackAction));
				}

			foreach (IGrouping<ContainerId, StackResponseContainerInfo> stackResponseGroup in stackResponses.GroupBy(r => r.ContainerName.ContainerId))
				if (stackResponseGroup.Count() > 1)
				{
					var containerId = stackResponseGroup.Key;
					StackResponseSlotInfo slotToKeep = null;
					foreach (IGrouping<byte, StackResponseSlotInfo> slotGroup in stackResponseGroup.SelectMany(d => d.Slots).GroupBy(s => s.Slot))
					{
						var slot = slotGroup.Key;
						if (slotGroup.Count() > 1)
							slotToKeep = slotGroup.ToList().Last();
					}
					if (slotToKeep != null)
						foreach (StackResponseContainerInfo containerInfo in stackResponseGroup)
							if (!containerInfo.Slots.Contains(slotToKeep))
								stackResponses.Remove(containerInfo);
				}

			return StackResponseStatus.Ok;
		}

		protected virtual void ProcessConsumeAction(ConsumeAction action, List<StackResponseContainerInfo> stackResponses)
		{
			byte count = action.Count;
			StackRequestSlotInfo source = action.Source;

			Item sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);
			sourceItem.Count -= count;
			if (sourceItem.Count <= 0)
			{
				sourceItem = new ItemAir();
				SetContainerItem(source.ContainerName.ContainerId, source.Slot, sourceItem);
			}

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessDropAction(DropAction action, List<StackResponseContainerInfo> stackResponses)
		{
			byte count = action.Count;
			Item dropItem;
			StackRequestSlotInfo source = action.Source;

			Item sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);

			if (sourceItem.Count == count || sourceItem.Count - count <= 0)
			{
				dropItem = sourceItem;
				sourceItem = new ItemAir();
				sourceItem.UniqueId = 0;
				SetContainerItem(source.ContainerName.ContainerId, source.Slot, sourceItem);
			}
			else
			{
				dropItem = (Item) sourceItem.Clone();
				sourceItem.Count -= count;
				dropItem.Count = count;
				dropItem.UniqueId = Item.GetUniqueId();
			}

			_player.DropItem(dropItem);

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessDestroyAction(DestroyAction action, List<StackResponseContainerInfo> stackResponses)
		{
			byte count = action.Count;
			StackRequestSlotInfo source = action.Source;

			Item sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);
			sourceItem.Count -= count;
			if (sourceItem.Count <= 0)
			{
				sourceItem = new ItemAir();
				SetContainerItem(source.ContainerName.ContainerId, source.Slot, sourceItem);
			}

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{ 
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessSwapAction(SwapAction action, List<StackResponseContainerInfo> stackResponses)
		{
			StackRequestSlotInfo source = action.Source;
			StackRequestSlotInfo destination = action.Destination;

			Item sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);
			Item destItem = GetContainerItem(destination.ContainerName.ContainerId, destination.Slot);

			SetContainerItem(source.ContainerName.ContainerId, source.Slot, destItem);
			SetContainerItem(destination.ContainerName.ContainerId, destination.Slot, sourceItem);

			if (source.ContainerName.ContainerId == ContainerId.EnchantingInput
				|| source.ContainerName.ContainerId == ContainerId.EnchantingMaterial
				|| destination.ContainerName.ContainerId == ContainerId.EnchantingInput
				|| destination.ContainerName.ContainerId == ContainerId.EnchantingMaterial)
			{
				if (!(GetContainerItem(ContainerId.EnchantingInput) is ItemAir)
					&& !(GetContainerItem(ContainerId.EnchantingMaterial) is ItemAir))
				{
					Enchantment.SendEnchantments(_player, GetContainerItem(ContainerId.EnchantingInput));
				}
				else
				{
					Enchantment.SendEmptyEnchantments(_player);
				}
			}

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = destItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = destItem.UniqueId
					}
				}
			});
			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = destination.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = destination.Slot,
						HotbarSlot = destination.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessPlaceAction(PlaceAction action, List<StackResponseContainerInfo> stackResponses)
		{
			byte count = action.Count;
			Item sourceItem;
			Item destItem;
			StackRequestSlotInfo source = action.Source;
			StackRequestSlotInfo destination = action.Destination;

			sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);

			if (sourceItem.Count == count || sourceItem.Count - count <= 0)
			{
				destItem = sourceItem;
				sourceItem = new ItemAir();
				sourceItem.UniqueId = 0;
				SetContainerItem(source.ContainerName.ContainerId, source.Slot, sourceItem);
			}
			else
			{
				destItem = (Item) sourceItem.Clone();
				sourceItem.Count -= count;
				destItem.Count = count;
				destItem.UniqueId = Item.GetUniqueId();
			}

			Item existingItem = GetContainerItem(destination.ContainerName.ContainerId, destination.Slot);
			if (existingItem.Equals(destItem))
			{
				existingItem.Count += count;
				destItem = existingItem;
			}
			else
			{
				SetContainerItem(destination.ContainerName.ContainerId, destination.Slot, destItem);
			}

			if (destination.ContainerName.ContainerId == ContainerId.EnchantingInput || destination.ContainerName.ContainerId == ContainerId.EnchantingMaterial)
			{
				if (!(GetContainerItem(ContainerId.EnchantingInput) is ItemAir)
					&& !(GetContainerItem(ContainerId.EnchantingMaterial) is ItemAir))
				{
					Enchantment.SendEnchantments(_player, GetContainerItem(ContainerId.EnchantingInput));
				}
				else
				{
					Enchantment.SendEmptyEnchantments(_player);
				}
			}

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{ 
					ContainerId = destination.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = destItem.Count,
						Slot = destination.Slot,
						HotbarSlot = destination.Slot,
						StackNetworkId = destItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessTakeAction(TakeAction action, List<StackResponseContainerInfo> stackResponses)
		{
			byte count = action.Count;
			Item sourceItem;
			Item destItem;
			StackRequestSlotInfo source = action.Source;
			StackRequestSlotInfo destination = action.Destination;

			sourceItem = GetContainerItem(source.ContainerName.ContainerId, source.Slot);
			Log.Debug($"Take {sourceItem}");

			if (sourceItem.Count - count <= 0)
			{
				destItem = sourceItem;
				sourceItem = new ItemAir();
				sourceItem.UniqueId = 0;
				SetContainerItem(source.ContainerName.ContainerId, source.Slot, sourceItem);
			}
			else
			{
				destItem = (Item) sourceItem.Clone();
				sourceItem.Count -= count;
				destItem.Count = count;
				destItem.UniqueId = Item.GetUniqueId();
			}

			var existingItem = GetContainerItem(destination.ContainerName.ContainerId, destination.Slot);
			if (existingItem.Equals(destItem))
			{
				existingItem.Count += destItem.Count;
				destItem = existingItem;
			}
			else
			{
				SetContainerItem(destination.ContainerName.ContainerId, destination.Slot, destItem);
			}

			if (source.ContainerName.ContainerId == ContainerId.EnchantingInput || source.ContainerName.ContainerId == ContainerId.EnchantingMaterial)
			{
				if (!(GetContainerItem(ContainerId.EnchantingInput) is ItemAir)
					&& !(GetContainerItem(ContainerId.EnchantingMaterial) is ItemAir))
				{
					Enchantment.SendEnchantments(_player, GetContainerItem(ContainerId.EnchantingInput));
				}
				else
				{
					Enchantment.SendEmptyEnchantments(_player);
				}
			}

			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = source.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = sourceItem.Count,
						Slot = source.Slot,
						HotbarSlot = source.Slot,
						StackNetworkId = sourceItem.UniqueId
					}
				}
			});
			stackResponses.Add(new StackResponseContainerInfo
			{
				ContainerName = new FullContainerName()
				{
					ContainerId = destination.ContainerName.ContainerId
				},
				Slots = new List<StackResponseSlotInfo>
				{
					new StackResponseSlotInfo()
					{
						Count = destItem.Count,
						Slot = destination.Slot,
						HotbarSlot = destination.Slot,
						StackNetworkId = destItem.UniqueId
					}
				}
			});
		}

		protected virtual void ProcessCreateAction(CreateAction action, List<Item> createCache)
		{
			SetContainerItem(ContainerId.CreatedOutput, createCache[action.ResultSlot]);
		}

		protected virtual bool ProcessCraftResultDeprecatedAction(CraftResultDeprecatedAction action, Recipe recipe, int repetitions, List<StackResponseContainerInfo> stackResponses, out List<Item> createCache)
		{
			createCache = null;
			if (recipe == null) return false;

			for (var i = 0; i < action.ResultItems.Length; i++)
			{
				if (action.ResultItems[i] == null) return false;
			}

			if (GetContainerItem(ContainerId.CreatedOutput).UniqueId != 0) return false;

			if (!RecipeManager.ValidateRecipe(
				recipe,
				_player.Inventory.UiInventory.Slots.Skip((byte) UIInventorySlots.CraftingInput[0]).Take(UIInventorySlots.CraftingInput.Length).ToList(),
				action.TimesCrafted * repetitions,
				out var resultItems,
				out var consumeItems))
			{
				return false;
			}

			for (var i = 0; i < consumeItems.Length; i++)
			{
				var consumeItem = consumeItems[i];
				var slot = (byte) UIInventorySlots.CraftingInput[i];

				if (consumeItem == null) continue;

				var existingItem = GetContainerItem(ContainerId.CraftingInput, slot);
				existingItem.Count -= consumeItem.Count;

				if (existingItem.Count <= 0)
				{
					SetContainerItem(ContainerId.CraftingInput, slot, existingItem = new ItemAir());
				}

				stackResponses.Add(new StackResponseContainerInfo
				{
					ContainerName = new FullContainerName()
					{ 
						ContainerId = ContainerId.CraftingInput
					},
					Slots = new List<StackResponseSlotInfo>
					{
						new StackResponseSlotInfo()
						{
							Count = existingItem.Count,
							Slot = slot,
							HotbarSlot = slot,
							StackNetworkId = existingItem.UniqueId
						}
					}
				});
			}

			for (var i = 0; i < resultItems.Count; i++)
			{
				var item = resultItems[i];
				item.UniqueId = Item.GetUniqueId();
			}

			if (resultItems.Count > 1)
			{
				createCache = resultItems;
				return true;
			}

			SetContainerItem(ContainerId.CreatedOutput, resultItems.Single());
			return true;
		}

		protected virtual void ProcessCraftNotImplementedDeprecatedAction(CraftNotImplementedDeprecatedAction action)
		{
		}

		protected virtual bool ProcessCraftAction(CraftAction action, out Recipe recipe, out int repetitions)
		{
			repetitions = 0;

			if (RecipeManager.NetworkIdRecipeMap.TryGetValue((int) action.RecipeNetworkId, out recipe))
			{
				repetitions = action.Repetitions;
				return true;
			}

			return false;
		}

		protected virtual bool ProcessCraftCreativeAction(CraftCreativeAction action, out Recipe recipe, out int repetitions)
		{
			repetitions = 0;
			recipe = null;
			if (_player.GameMode != GameMode.Creative) return false;

			var creativeItem = InventoryUtils.Content.GetItemById(action.CreativeItemNetworkId);
			if (creativeItem == null)
			{
				throw new Exception($"Failed to find inventory item with unique id: {action.CreativeItemNetworkId}");
			}

			creativeItem = creativeItem.Clone() as Item;
			creativeItem.Count = (byte) creativeItem.MaxStackSize;
			//Log.Debug($"Creating {creativeItem}");
			//_player.Inventory.UiInventory.Slots[50] = creativeItem;

			recipe = new ShapelessRecipe(creativeItem, new());
			repetitions = action.Repetitions;
			return true;
		}

		protected virtual void ProcessCraftRecipeOptionalAction(CraftRecipeOptionalAction action)
		{
		}

		private Item GetContainerItem(ContainerId containerId)
		{
			var slot = UIInventorySlots.GetSlotByContainerId(containerId);
			if (slot == null) return null;

			return GetContainerItem(containerId, slot.Value);
		}

		private Item GetContainerItem(ContainerId containerId, int slot)
		{
			Item item = null;
			switch (containerId.ToWindowId())
			{
				case WindowId.UI:
					item = _player.Inventory.UiInventory.Slots[slot];
					break;
				case WindowId.Inventory:
					item = _player.Inventory.Slots[slot];
					break;
				case WindowId.Offhand:
					item = _player.Inventory.OffHand;
					break;
				case WindowId.Armor:
					item = _player.Inventory.GetArmorSlot((ArmorType) slot);
					break;
				default:
					if (_player.GetOpenInventory() is ContainerInventory inventory)
					{
						item = inventory.GetSlot((byte) slot);
					}
					break;
			}

			return item;
		}

		private bool SetContainerItem(ContainerId containerId, Item item)
		{
			var slot = UIInventorySlots.GetSlotByContainerId(containerId);
			if (slot == null) return false;

			SetContainerItem(containerId, slot.Value, item);
			return true;
		}

		private void SetContainerItem(ContainerId containerId, int slot, Item item)
		{
			switch (containerId.ToWindowId())
			{
				case WindowId.UI:
					_player.Inventory.UiInventory.Slots[slot] = item;
					break;
				case WindowId.Inventory:
					_player.Inventory.Slots[slot] = item;
					break;
				case WindowId.Offhand:
					_player.Inventory.OffHand = item;
					break;
				case WindowId.Armor:
					_player.Inventory.UpdateArmorSlot((ArmorType) slot, item, true);
					break;
				default:
					if (_player.GetOpenInventory() is ContainerInventory inventory)
					{
						inventory.SetSlot(_player, (byte) slot, item);
					}
					break;
			}
		}
	}
}