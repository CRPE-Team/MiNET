using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MiNET.Items;
using MiNET.Net;

namespace MiNET.Utils
{
	public class ItemStacks : IEnumerable<Item>, IPacketDataObject
	{
		private readonly Item[] _items;

		public virtual int Length => _items.Length;

		public virtual Item this[int index] { get => GetItem(index); set => SetItem(index, value); }

		protected ItemStacks()
		{

		}

		public ItemStacks(int length)
		{
			_items = new Item[length];
		}

		public ItemStacks(Item[] collection)
		{
			_items = collection;
		}

		public ItemStacks(ItemStacks collection)
			: this(collection.Length)
		{
			collection.CopyTo(_items, 0);
		}

		public static ItemStacks CreateAir(int length)
		{
			var stacks = new ItemStacks(length);
			stacks.Reset();

			return stacks;
		}

		public virtual void Write(Packet packet)
		{
			Write(packet, true);
		}

		public virtual void Write(Packet packet, bool writeUniqueId)
		{
			packet.WriteLength(Length);

			for (int i = 0; i < Length; i++)
			{
				packet.Write(this[i], writeUniqueId);
			}
		}

		public static ItemStacks Read(Packet packet)
		{
			return Read(packet, true);
		}

		public static ItemStacks Read(Packet packet, bool readUniqueId)
		{
			var count = packet.ReadLength();
			var itemStacks = new ItemStacks(count);

			for (int i = 0; i < count; i++)
			{
				itemStacks[i] = packet.ReadItem(readUniqueId);
			}

			return itemStacks;
		}

		public virtual int IndexOf(Item item)
		{
			return Array.IndexOf(_items, item);
		}

		public virtual bool Contains(Item item)
		{
			return _items.Contains(item);
		}

		public virtual void CopyTo(Item[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public virtual Item[] GetSource()
		{
			return _items;
		}

		public virtual void Reset()
		{
			for (int i = 0; i < _items.Length; i++)
			{
				ref var slot = ref _items[i];
				if (slot is not ItemAir)
				{
					slot = new ItemAir();
				}
			}
		}

		public virtual IEnumerator<Item> GetEnumerator()
		{
			return ((ICollection<Item>) _items).GetEnumerator();
		}

		public virtual ItemStacks CreateCopy()
		{
			return new ItemStacks(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		private Item GetItem(int index)
		{
			var item = _items[index];

			if (item is not ItemAir && item.Count <= 0)
			{
				item = _items[index] = new ItemAir();
			}

			return item;
		}

		private void SetItem(int index, Item item)
		{
			if (item is not ItemAir && item.Count <= 0)
			{
				item = new ItemAir();
			}

			_items[index] = item;
		}
	}

	public class ContainerItemStacks : ItemStacks
	{
		private readonly int _oneContainerSize;
		private readonly List<Item[]> _containers;

		public override int Length => _containers.Count * _oneContainerSize;

		public override Item this[int index] 
		{ 
			get => _containers[index / _oneContainerSize][index % _oneContainerSize]; 
			set => _containers[index / _oneContainerSize][index % _oneContainerSize] = value; 
		}

		public ContainerItemStacks(int oneContainerSize)
		{
			_oneContainerSize = oneContainerSize;
			_containers = new List<Item[]>();
		}

		public ContainerItemStacks(Item[] container)
		{
			_oneContainerSize = container.Length;
			_containers = [container];
		}

		public ContainerItemStacks(ContainerItemStacks collection)
		{
			_oneContainerSize = collection._oneContainerSize;
			_containers = collection._containers.Select(c =>
			{
				var container = new Item[c.Length];
				c.CopyTo(container, 0);

				return container;
			}).ToList();
		}

		public override int IndexOf(Item item)
		{
			foreach (var container in _containers)
			{
				var i = Array.IndexOf(container, item);

				if (i > -1) return i;
			}

			return -1;
		}

		public override bool Contains(Item item)
		{
			return _containers.Any(c => c.Contains(item));
		}

		public override void CopyTo(Item[] array, int arrayIndex)
		{
			var startIndex = arrayIndex;

			foreach (var container in _containers)
			{
				container.CopyTo(array, startIndex);
				startIndex += _oneContainerSize;
			}
		}

		public void AddContainer(Item[] items)
		{
			if (items.Length != _oneContainerSize)
			{
				throw new ArgumentOutOfRangeException($"items length must equals {_oneContainerSize}");
			}

			_containers.Add(items);
		}

		public void SetContainer(Item[] items, int containerIndex)
		{
			if (items.Length != _oneContainerSize)
			{
				throw new ArgumentOutOfRangeException($"items length must equals {_oneContainerSize}");
			}

			if (containerIndex == _containers.Count)
			{
				_containers.Add(items);
				return;
			}

			// cannot set a value for the index outside the size of the containers
			_containers[containerIndex] = items;
		}

		public Item[] GetContainer(int containerIndex)
		{
			return _containers[containerIndex];
		}

		public void RemoveContainer(int containerIndex)
		{
			_containers.RemoveAt(containerIndex);
		}

		public int ContainersCount()
		{
			return _containers.Count;
		}

		public override IEnumerator<Item> GetEnumerator()
		{
			foreach (var container in _containers)
			{
				foreach (var item in container)
				{
					yield return item;
				}
			}
		}

		public override void Reset()
		{
			foreach (var container in _containers)
			{
				for (int i = 0; i < container.Length; i++)
				{
					container[i] = new ItemAir();
				}
			}
		}

		public override ItemStacks CreateCopy()
		{
			return new ContainerItemStacks(this);
		}
	}

	public class CreativeItemStacks : ItemStacks
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(CreativeItemStacks));

		public CreativeItemStacks(int length) : base(length)
		{
		}

		public CreativeItemStacks(Item[] collection) : base(collection)
		{
		}

		public override void Write(Packet packet)
		{
			packet.WriteLength(Length);

			foreach (var item in this)
			{
				packet.WriteUnsignedVarInt((uint) item.UniqueId);
				packet.Write(item, false);
			}
		}

		public static new CreativeItemStacks Read(Packet packet)
		{
			var count = packet.ReadLength();
			var metadata = new CreativeItemStacks(count);

			for (int i = 0; i < count; i++)
			{
				var networkId = packet.ReadUnsignedVarInt();
				var item = packet.ReadItem(false);

				item.UniqueId = (int) networkId;
				metadata[i] = item;

				Log.Debug(item);
			}

			return metadata;
		}
	}
}