﻿using System.Collections.Generic;
using System.Linq;
using fNbt.Serialization;
using MiNET.Inventory;
using MiNET.Items;
using MiNET.Utils;
using MiNET.Worlds;

namespace MiNET.BlockEntities
{
	public abstract class ContainerBlockEntityBase : BlockEntity
	{
		[NbtIgnore]
		protected WindowType WindowType { get; }

		[NbtIgnore]
		protected ContainerInventory Inventory { get; set; }

		public Item[] Items { get; }

		public string Lock { get; set; }

		protected ContainerBlockEntityBase(string id, int size, WindowType windowType) : base(id)
		{
			WindowType = windowType;

			Items = Enumerable.Range(0, size).Select<int, Item>(_ => new ItemAir()).ToArray();
		}

		public virtual ContainerInventory GetInventory(Level level)
		{
			if (Inventory != null) return Inventory;

			lock (Items)
			{
				Inventory = CreateInventory(level);
				Inventory.InventoryOpen += OnInventoryOpen;
				Inventory.InventoryOpened += OnInventoryOpened;
				Inventory.InventoryClosed += OnInventoryClosed;
				Inventory.InventoryChanged += OnInventoryChanged;

				return Inventory;
			}
		}

		protected virtual ContainerInventory CreateInventory(Level level)
		{
			return new ContainerInventory(new ItemStacks(Items), Coordinates)
			{
				Type = WindowType
			};
		}

		public bool Open(Player player)
		{
			return GetInventory(player.Level).Open(player);
		}

		protected virtual void OnInventoryOpen(object sender, InventoryOpenEventArgs args)
		{
			SendData(args.Player);
		}

		protected virtual void OnInventoryOpened(object sender, InventoryOpenedEventArgs args)
		{

		}

		protected virtual void OnInventoryClosed(object sender, InventoryClosedEventArgs args)
		{

		}

		protected virtual void OnInventoryChanged(object sender, InventoryChangeEventArgs args)
		{

		}

		public override void RemoveBlockEntity(Level level)
		{
			if (Inventory != null)
			{
				Inventory.InventoryOpen -= OnInventoryOpen;
				Inventory.InventoryOpened -= OnInventoryOpened;
				Inventory.InventoryClosed -= OnInventoryClosed;
				Inventory.InventoryChanged -= OnInventoryChanged;

				Inventory.Close();
				Inventory = null;
			}
		}

		public override List<Item> GetDrops()
		{
			return Items.ToList();
		}
	}
}
