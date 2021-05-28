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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;
using MiNET.Worlds;

namespace MiNET
{
	public class PlayerInventory
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(PlayerInventory));

		public const int HotbarSize = 9;
		public const int InventorySize = HotbarSize + 36;
		public Player Player { get; }

		public List<Item> Slots { get; }

		public int InHandSlot { get; set; }
		public Item OffHand { get; set; } = new ItemAir();

		public CursorInventory UiInventory { get; set; } = new CursorInventory();

		// Armour
		public Item Boots { get; set; } = new ItemAir();
		public Item Leggings { get; set; } = new ItemAir();
		public Item Chest { get; set; } = new ItemAir();
		public Item Helmet { get; set; } = new ItemAir();


		public PlayerInventory(Player player)
		{
			Player = player;

			Slots = Enumerable.Repeat((Item) new ItemAir(), InventorySize).ToList();

			InHandSlot = 0;
		}

		public virtual Item GetItemInHand()
		{
			return Slots[InHandSlot] ?? new ItemAir();
		}

		public virtual void DamageItemInHand(ItemDamageReason reason, Entity target, Block block)
		{
			Slots[InHandSlot] = DamageItem(GetItemInHand(), reason, target, block);
		}

		public virtual void DamageArmor()
		{
			if (Player.GameMode != GameMode.Survival) return;

			Helmet = DamageArmorItem(Helmet);
			Chest = DamageArmorItem(Chest);
			Leggings = DamageArmorItem(Leggings);
			Boots = DamageArmorItem(Boots);
			Player.SendArmorForPlayer();
		}

		public virtual Item DamageArmorItem(Item item)
		{
			return DamageItem(item, ItemDamageReason.EntityAttack, null, null);
		}

		public virtual Item DamageItem(Item item, ItemDamageReason reason, Entity target, Block block)
		{
			if (Player.GameMode != GameMode.Survival) return item;

			var unbreakingLevel = item.GetEnchantingLevel(EnchantingType.Unbreaking);
			if (unbreakingLevel > 0)
			{
				if (new Random().Next(1 + unbreakingLevel) != 0) return item;
			}

			if (item.DamageItem(Player, reason, target, block))
			{
				item = new ItemAir();

				var sound = McpeLevelSoundEventOld.CreateObject();
				sound.soundId = 5;
				sound.blockId = -1;
				sound.entityType = 1;
				sound.position = Player.KnownPosition;
				Player.Level.RelayBroadcast(sound);
			}

			return item;
		}


		[Wired]
		public virtual void SetInventorySlot(int slot, Item item)
		{
			if (item == null || item.Count <= 0) item = new ItemAir();

			UpdateInventorySlot(slot, item);

			SendSetSlot(slot);
		}

		[Wired]
		public virtual void SetArmorSlot(ArmorType type, Item item, bool sendToPlayer = true)
		{
			if (item == null || item.Count <= 0) item = new ItemAir();

			UpdateArmorSlot(type, item);

			Player.SendArmorForPlayer();
			if (sendToPlayer) SendSetSlot((int) type, item, 0x78);
		}

		[Wired]
		public virtual void SetOffHandSlot(Item item, bool sendToPlayer = true)
		{
			if (item == null || item.Count <= 0) item = new ItemAir();

			UpdateOffHandSlot(item);

			Player.SendEquipmentForPlayer();
			if (sendToPlayer) SendSetSlot(0, item, 0x77);
		}

		[Wired]
		public virtual void SetUiSlot(int slot, Item item)
		{
			if (item == null || item.Count <= 0) item = new ItemAir();

			UpdateUiSlot(slot, item);

			SendSetSlot(slot, item, 0x7c);
		}

		public virtual void UpdateInventorySlot(int slot, Item item)
		{
			var existing = Slots[slot];

			UpdateSlot(() => existing, newItem => Slots[slot] = newItem, item);
		}

		public virtual void UpdateOffHandSlot(Item item)
		{
			UpdateSlot(() => OffHand, newItem => OffHand = newItem, item);
		}

		public virtual void UpdateUiSlot(int slot, Item item)
		{
			var slots = UiInventory.Slots;
			var existing = slots[slot];
			
			UpdateSlot(() => existing, newItem => slots[slot] = newItem, item);
		}

		public virtual void UpdateArmorSlot(ArmorType type, Item item)
		{
			var existing = type switch
			{
				ArmorType.Helmet => Helmet,
				ArmorType.Chestplate => Chest,
				ArmorType.Leggings => Leggings,
				ArmorType.Boots => Boots,
				_ => null
			};

			if (existing == null) return;

			Action<Item> setItemDelegate = newItem =>
			{
				switch (type)
				{
					case ArmorType.Helmet:
						Helmet = newItem;
						break;
					case ArmorType.Chestplate:
						Chest = newItem;
						break;
					case ArmorType.Leggings:
						Leggings = newItem;
						break;
					case ArmorType.Boots:
						Boots = newItem;
						break;
				}
			};

			UpdateSlot(() => existing, setItemDelegate, item);
		}

		private void UpdateSlot(Func<Item> getItem, Action<Item> setItem, Item item)
		{
			var existing = getItem();
			if (existing.Id != item.Id)
			{
				setItem(item);
				existing = item;
			}
			existing.Count = item.Count;
			existing.Metadata = item.Metadata;
			existing.ExtraData = item.ExtraData;
		}

		public ItemStacks GetSlots()
		{
			ItemStacks slotData = new ItemStacks();
			for (int i = 0; i < Slots.Count; i++)
			{
				if (Slots[i].Count == 0) Slots[i] = new ItemAir();
				slotData.Add(Slots[i]);
			}

			return slotData;
		}

		public ItemStacks GetUiSlots()
		{
			ItemStacks slotData = new ItemStacks();
			for (int i = 0; i < UiInventory.Slots.Count; i++)
			{
				if (UiInventory.Slots[i].Count == 0) UiInventory.Slots[i] = new ItemAir();
				slotData.Add(UiInventory.Slots[i]);
			}

			return slotData;
		}

		public ItemStacks GetOffHand()
		{
			return new ItemStacks
			{
				OffHand ?? new ItemAir(),
			};
		}

		public ItemStacks GetArmor()
		{
			return new ItemStacks
			{
				Helmet ?? new ItemAir(),
				Chest ?? new ItemAir(),
				Leggings ?? new ItemAir(),
				Boots ?? new ItemAir(),
			};
		}

		public virtual bool SetFirstEmptySlot(Item item, bool update)
		{
			for (int si = 0; si < Slots.Count; si++)
			{
				Item existingItem = Slots[si];

				// This needs to also take extradata into account when comparing.
				if (existingItem.Equals(item) && existingItem.Count < existingItem.MaxStackSize)
				{
					int take = Math.Min(item.Count, existingItem.MaxStackSize - existingItem.Count);
					existingItem.Count += (byte) take;
					item.Count -= (byte) take;
					if (update) SendSetSlot(si);

					if (item.Count <= 0)
					{
						return true;
					}
				}
			}

			for (int si = 0; si < Slots.Count; si++)
			{
				if (FirstEmptySlot(item, update, si)) return true;
			}

			return false;
		}

		private bool FirstEmptySlot(Item item, bool update, int si)
		{
			Item existingItem = Slots[si];

			if (existingItem is ItemAir || existingItem.Id == 0 || existingItem.Id == -1)
			{
				Slots[si] = (Item) item.Clone();
				item.Count = 0;
				if (update) SendSetSlot(si);
				return true;
			}

			return false;
		}

		public bool AddItem(Item item, bool update)
		{
			for (int si = 0; si < Slots.Count; si++)
			{
				Item existingItem = Slots[si];

				if (existingItem is ItemAir || existingItem.Id == 0 || existingItem.Id == -1)
				{
					Slots[si] = item;
					if (update) SendSetSlot(si);
					return true;
				}
			}

			return false;
		}


		public virtual void SetHeldItemSlot(int selectedHotbarSlot, bool sendToPlayer = true)
		{
			InHandSlot = selectedHotbarSlot;

			if (sendToPlayer)
			{
				var order = McpeMobEquipment.CreateObject();
				order.runtimeEntityId = EntityManager.EntityIdSelf;
				order.item = GetItemInHand();
				order.selectedSlot = (byte) InHandSlot;
				order.slot = (byte) (InHandSlot + HotbarSize);
				Player.SendPacket(order);
			}

			var broadcast = McpeMobEquipment.CreateObject();
			broadcast.runtimeEntityId = Player.EntityId;
			broadcast.item = GetItemInHand();
			broadcast.selectedSlot = (byte) InHandSlot;
			broadcast.slot = (byte) (InHandSlot + HotbarSize);
			Player.Level?.RelayBroadcast(Player, broadcast);
		}

		/// <summary>
		///     Empty the specified slot
		/// </summary>
		/// <param name="slot">The slot to empty.</param>
		public void ClearInventorySlot(byte slot)
		{
			SetInventorySlot(slot, new ItemAir());
		}

		public bool HasItem(Item item)
		{
			for (byte i = 0; i < Slots.Count; i++)
			{
				if (Slots[i].Id == item.Id && Slots[i].Metadata == item.Metadata)
				{
					return true;
				}
			}

			return false;
		}

		public void RemoveItems(short id, byte count)
		{
			if (count <= 0) return;

			for (byte i = 0; i < Slots.Count; i++)
			{
				if (count <= 0) break;

				var slot = Slots[i];
				if (slot.Id == id)
				{
					if (Slots[i].Count >= count)
					{
						Slots[i].Count -= count;
						count = 0;
					}
					else
					{
						count -= Slots[i].Count;
						Slots[i].Count = 0;
					}

					if (slot.Count == 0)
					{
						Slots[i] = new ItemAir();
					}

					SendSetSlot(i);
				}
			}
		}

		public virtual void SendSetSlot(int slot)
		{
			SendSetSlot(slot, Slots[slot], 0);
		}

		public virtual void SendSetSlot(int slot, Item item, uint inventoryId)
		{
			var sendSlot = McpeInventorySlot.CreateObject();
			sendSlot.inventoryId = inventoryId;
			sendSlot.slot = (uint) slot;
			sendSlot.uniqueid = item.UniqueId;
			sendSlot.item = item;
			Player.SendPacket(sendSlot);
		}

		public void Clear()
		{
			for (int i = 0; i < Slots.Count; ++i)
			{
				if (Slots[i] == null || Slots[i].Id != 0) Slots[i] = new ItemAir();
			}
			
			UiInventory.Clear();

			if (OffHand.Id != 0) OffHand = new ItemAir();

			if (Helmet.Id != 0) Helmet = new ItemAir();
			if (Chest.Id != 0) Chest = new ItemAir();
			if (Leggings.Id != 0) Leggings = new ItemAir();
			if (Boots.Id != 0) Boots = new ItemAir();

			Player.SendPlayerInventory();
		}
	}
}
