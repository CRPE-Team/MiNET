#region LICENSE

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
using System.Collections.Concurrent;
using System.Linq;
using log4net;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Inventories
{
	public interface IInventory
	{
		public WindowType Type { get; }
		public ItemStacks Slots { get; }
		public byte WindowsId { get; }

		public long RuntimeEntityId { get; }
		public BlockCoordinates Coordinates { get; }

		public bool IsOpen { get; }

		public bool Open(Player player);
		public void Close();
		public bool Close(Player player, bool closedByPlayer = false);
		public void Clear();
	}

	public class Inventory : IInventory
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Inventory));

		public event EventHandler<InventoryChangeEventArgs> InventoryChanged;
		public event EventHandler<InventoryOpenEventArgs> InventoryOpen;
		public event EventHandler<InventoryOpenedEventArgs> InventoryOpened;
		public event EventHandler<InventoryClosedEventArgs> InventoryClosed;

		public WindowType Type { get; set; }
		public virtual ItemStacks Slots { get; set; }
		public byte WindowsId { get; set; } = GetNewWindowId();

		public long RuntimeEntityId { get; set; } = EntityManager.EntityIdUndefined;
		public BlockCoordinates Coordinates { get; set; }

		public bool IsOpen => Observers.Any();

		public Inventory(ItemStacks items, long runtimeEntityId)
		{
			Slots = items;
			RuntimeEntityId = runtimeEntityId;
		}

		public Inventory(ItemStacks items, BlockCoordinates coordinates)
		{
			Slots = items;
			Coordinates = coordinates;
		}

		public Inventory(BlockCoordinates coordinates, WindowType type)
		{
			Coordinates = coordinates;
			Type = type;

			RuntimeEntityId = EntityManager.EntityIdSelf;
			Slots = new ItemStacks(0);
		}

		public void SetSlot(Player player, byte slot, Item itemStack)
		{
			Slots[slot] = itemStack;

			OnInventoryChange(player, slot, itemStack);
		}

		public Item GetSlot(byte slot)
		{
			return Slots[slot];
		}

		public bool DecreaseSlot(byte slot)
		{
			var slotData = Slots[slot];
			if (slotData is ItemAir) return false;
			var count = slotData.Count;

			slotData.Count--;

			if (slotData.Count <= 0)
			{
				slotData = new ItemAir();
			}

			SetSlot(null, slot, slotData);

			if (count <= 0) return false;

			OnInventoryChange(null, slot, slotData);
			return true;
		}

		public void IncreaseSlot(byte slot, string id, short metadata)
		{
			Item slotData = Slots[slot];
			if (slotData is ItemAir)
			{
				slotData = ItemFactory.GetItem(id, metadata, 1);
			}
			else
			{
				slotData.Count++;
			}

			SetSlot(null, slot, slotData);

			OnInventoryChange(null, slot, slotData);
		}

		public virtual bool Open(Player player)
		{
			var openedInventory = player.GetOpenInventory();

			if (this == openedInventory) return true;
			if (openedInventory != null)
			{
				player.CloseOpenedInventory();
			}

			var open = !IsOpen;
			if (!OnInventoryOpen(player, open)) return false;

			player.SetOpenInventory(this);

			AddObserver(player);

			SendOpen(player);
			SendContent(player);

			OnInventoryOpened(player, open);

			return true;
		}

		public virtual void Close()
		{
			foreach (var observer in Observers.ToArray())
			{
				Close(observer);
			}
		}

		public virtual bool Close(Player player, bool closedByPlayer = false)
		{
			var openedInventory = player.GetOpenInventory();

			if (openedInventory != this)
			{
				return false;
			}

			player.SetOpenInventory(null);
			RemoveObserver(player);

			SendClose(player, closedByPlayer);

			foreach (var item in player.Inventory.UiInventory.Slots)
			{
				if (item is ItemAir) continue;

				player.Inventory.SetFirstEmptySlot(item, true);
			}

			OnInventoryClosed(player, !IsOpen);

			return true;
		}

		public void SendContent(Player player)
		{
			var containerSetContent = McpeInventoryContent.CreateObject();
			containerSetContent.inventoryId = WindowsId;
			containerSetContent.input = Slots;
			containerSetContent.containerName = FullContainerName.Unknown;
			player.SendPacket(containerSetContent);
		}

		protected void SendOpen(Player player)
		{
			var containerOpen = McpeContainerOpen.CreateObject();
			containerOpen.windowId = WindowsId;
			containerOpen.type = (sbyte) Type;
			containerOpen.coordinates = Coordinates;
			containerOpen.runtimeEntityId = RuntimeEntityId;
			player.SendPacket(containerOpen);
		}

		protected void SendClose(Player player, bool closedByPlayer)
		{
			var closePacket = McpeContainerClose.CreateObject();
			closePacket.windowId = WindowsId;
			closePacket.windowType = (sbyte) Type;
			closePacket.server = !closedByPlayer;
			player.SendPacket(closePacket);
		}

		public virtual void Clear()
		{
			Slots.Reset();

			foreach (var observer in Observers)
			{
				SendContent(observer);
			}
		}

		protected virtual void OnInventoryChange(Player player, byte slot, Item itemStack)
		{
			InventoryChanged?.Invoke(this, new InventoryChangeEventArgs(player, this, slot, itemStack));
		}

		protected virtual bool OnInventoryOpen(Player player, bool open)
		{
			var args = new InventoryOpenEventArgs(player, this, open);
			InventoryOpen?.Invoke(this, args);

			return !args.Cancel;
		}

		protected virtual void OnInventoryOpened(Player player, bool opened)
		{
			InventoryOpened?.Invoke(this, new InventoryOpenedEventArgs(player, this, opened));
		}

		protected virtual void OnInventoryClosed(Player player, bool closed)
		{
			InventoryClosed?.Invoke(this, new InventoryClosedEventArgs(player, this, closed));
		}

		// Below is a workaround making it possible to send
		// updates to only peopele that is looking at this inventory.
		// Is should be converted to some sort of event based version.

		public ConcurrentBag<Player> Observers { get; } = new ConcurrentBag<Player>();

		public virtual void AddObserver(Player player)
		{
			Observers.Add(player);
		}

		public virtual void RemoveObserver(Player player)
		{
			// Need to arrange for this to work when players get disconnected
			// from crash. It will leak players for sure.
			Observers.TryTake(out player);
		}

		private static byte _lastWindowId;

		private static byte GetNewWindowId()
		{
			return _lastWindowId = (byte) Math.Max((byte) WindowId.First, ++_lastWindowId % (byte) WindowId.Last);
		}
	}
}