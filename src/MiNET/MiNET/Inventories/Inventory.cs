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
		public WindowId WindowId { get; }

		public long RuntimeEntityId { get; }
		public BlockCoordinates Coordinates { get; }

		public bool IsOpen { get; }

		public bool Open(Player player);
		public bool Close(Player player, bool closedByPlayer = false);
		public void Clear();
	}

	public class ContainerInventory : CommonInventory
	{
		public event EventHandler<InventoryChangeEventArgs> InventoryChanged;

		public override bool IsOpen => Observers.Any();

		public ContainerInventory(ItemStacks items, long runtimeEntityId) 
			: base(items, default, runtimeEntityId)
		{

		}

		public ContainerInventory(ItemStacks items, BlockCoordinates coordinates) 
			: base(items, coordinates, EntityManager.EntityIdUndefined)
		{

		}

		public virtual void SetSlot(Player player, byte slot, Item itemStack)
		{
			Slots[slot] = itemStack;

			OnInventoryChange(player, slot, itemStack);
			BroadcastSetSlot(player, slot);
		}

		public virtual Item GetSlot(byte slot)
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
			BroadcastSetSlot(slot);
			return true;
		}

		public void IncreaseSlot(byte slot, string id, short metadata)
		{
			var slotData = Slots[slot];
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

		public virtual void Close()
		{
			foreach (var observer in Observers.ToArray())
			{
				Close(observer);
			}
		}

		public override void Clear()
		{
			base.Clear();

			foreach (var observer in Observers)
			{
				SendContent(observer);
			}
		}

		protected virtual void BroadcastSetSlot(int slot)
		{
			BroadcastSetSlot(null, slot);
		}

		protected virtual void BroadcastSetSlot(Player sender, int slot)
		{
			var item = Slots[slot];

			foreach (var observer in Observers)
			{
				if (observer == sender) continue;

				SendSetSlot(observer, slot, item, WindowId);
			}
		}

		protected override bool OnInventoryOpen(Player player, bool open)
		{
			var opened = base.OnInventoryOpen(player, open);

			if (opened)
			{
				AddObserver(player);
			}

			return opened;
		}

		protected override void OnInventoryClose(Player player)
		{
			base.OnInventoryClose(player);

			RemoveObserver(player);
		}

		// Below is a workaround making it possible to send
		// updates to only peopele that is looking at this inventory.
		// Is should be converted to some sort of event based version.

		public ConcurrentBag<Player> Observers { get; } = new ConcurrentBag<Player>();

		protected virtual void AddObserver(Player player)
		{
			Observers.Add(player);
		}

		protected virtual void RemoveObserver(Player player)
		{
			// Need to arrange for this to work when players get disconnected
			// from crash. It will leak players for sure.
			Observers.TryTake(out player);
		}

		protected virtual void OnInventoryChange(Player player, byte slot, Item itemStack)
		{
			InventoryChanged?.Invoke(this, new InventoryChangeEventArgs(player, this, slot, itemStack));
		}
	}

	public class Inventory : CommonInventory
	{
		private bool _isOpen;

		public override bool IsOpen => _isOpen;

		public Inventory(BlockCoordinates coordinates, WindowType type)
			: this(new ItemStacks(0), coordinates, type)
		{

		}

		public Inventory(ItemStacks items, BlockCoordinates coordinates, WindowType type)
			: base(items, coordinates, EntityManager.EntityIdSelf)
		{
			Type = type;
		}

		public Inventory(int size, long runtimeEntityId, WindowType type)
			: base(ItemStacks.CreateAir(size), default, runtimeEntityId)
		{
			Type = type;
		}

		protected override bool OnInventoryOpen(Player player, bool open)
		{
			if (_isOpen) return false;

			var opened = base.OnInventoryOpen(player, open);

			if (opened)
			{
				_isOpen = true;
			}

			return opened;
		}

		protected override void OnInventoryClose(Player player)
		{
			base.OnInventoryClose(player);

			_isOpen = false;
		}
	}

	public abstract class CommonInventory : IInventory
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Inventory));

		public event EventHandler<InventoryOpenEventArgs> InventoryOpen;
		public event EventHandler<InventoryOpenedEventArgs> InventoryOpened;
		public event EventHandler<InventoryEventArgs> InventoryClose;
		public event EventHandler<InventoryClosedEventArgs> InventoryClosed;

		public WindowType Type { get; set; }
		public virtual ItemStacks Slots { get; set; }
		public WindowId WindowId { get; set; } = GetNewWindowId();

		public long RuntimeEntityId { get; set; }
		public BlockCoordinates Coordinates { get; set; }

		public abstract bool IsOpen { get; }

		protected CommonInventory(ItemStacks items, BlockCoordinates coordinates, long runtimeEntityId)
		{
			Slots = items;
			Coordinates = coordinates;
			RuntimeEntityId = runtimeEntityId;
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

			SendOpen(player);
			SendContent(player);

			OnInventoryOpened(player, open);

			return true;
		}

		public virtual bool Close(Player player, bool closedByPlayer = false)
		{
			var openedInventory = player.GetOpenInventory();

			if (openedInventory != this)
			{
				return false;
			}

			OnInventoryClose(player);

			player.SetOpenInventory(null);

			SendClose(player, closedByPlayer);

			player.Inventory.CloseUiInventory();

			OnInventoryClosed(player, !IsOpen);

			return true;
		}

		public void SendContent(Player player)
		{
			var containerSetContent = McpeInventoryContent.CreateObject();
			containerSetContent.inventoryId = (byte) WindowId;
			containerSetContent.input = Slots;
			containerSetContent.containerName = FullContainerName.Unknown;
			player.SendPacket(containerSetContent);
		}

		protected virtual void SendOpen(Player player)
		{
			var containerOpen = McpeContainerOpen.CreateObject();
			containerOpen.windowId = (byte) WindowId;
			containerOpen.type = (sbyte) Type;
			containerOpen.coordinates = Coordinates;
			containerOpen.runtimeEntityId = RuntimeEntityId;
			player.SendPacket(containerOpen);
		}

		protected virtual void SendClose(Player player, bool closedByPlayer)
		{
			var closePacket = McpeContainerClose.CreateObject();
			closePacket.windowId = (byte) WindowId;
			closePacket.windowType = (sbyte) Type;
			closePacket.server = !closedByPlayer;
			player.SendPacket(closePacket);
		}

		protected virtual void SendSetSlot(Player player, int slot)
		{
			SendSetSlot(player, slot, Slots[slot], WindowId);
		}

		protected virtual void SendSetSlot(Player player, int slot, Item item, WindowId windowId)
		{
			var sendSlot = McpeInventorySlot.CreateObject();
			sendSlot.inventoryId = (uint) windowId;
			sendSlot.slot = (uint) slot;
			sendSlot.item = item;
			sendSlot.containerName = FullContainerName.Unknown;
			player.SendPacket(sendSlot);
		}

		public virtual void Clear()
		{
			Slots.Reset();
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

		protected virtual void OnInventoryClose(Player player)
		{
			InventoryClose?.Invoke(this, new InventoryEventArgs(player, this));
		}

		protected virtual void OnInventoryClosed(Player player, bool closed)
		{
			InventoryClosed?.Invoke(this, new InventoryClosedEventArgs(player, this, closed));
		}

		private static byte _lastWindowId;

		private static WindowId GetNewWindowId()
		{
			return (WindowId) (_lastWindowId = (byte) Math.Max((byte) WindowId.First, ++_lastWindowId % (byte) WindowId.Last));
		}
	}
}