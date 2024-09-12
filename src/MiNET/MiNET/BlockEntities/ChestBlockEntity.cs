using System;
using fNbt.Serialization;
using MiNET.Inventory;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.BlockEntities
{
	public class ChestBlockEntity : ContainerBlockEntity
	{
		[NbtProperty("forceunpair")]
		public bool? ForceUnpair { get; set; }

		[NbtProperty("pairlead")]
		public bool? IsPairLead { get; set; }

		[NbtProperty("pairx")]
		public int? PairX { get; set; }

		[NbtProperty("pairz")]
		public int? PairZ { get; set; }

		[NbtIgnore]
		public bool IsPaired => PairX.HasValue && PairZ.HasValue && IsPairLead.HasValue && !(ForceUnpair ?? false);

		[NbtIgnore]
		public BlockCoordinates PairCoordinates => new BlockCoordinates(PairX ?? 0, Coordinates.Y, PairZ ?? 0);

		public ChestBlockEntity() : base(BlockEntityIds.Chest)
		{

		}

		public override ContainerInventory GetInventory(Level level)
		{
			lock (Items)
			{
				CheckPair(level);

				return base.GetInventory(level);
			}
		}

		protected override ContainerInventory CreateInventory(Level level)
		{
			if (IsPaired)
			{
				var pair = level.GetBlockEntity(PairCoordinates) as ChestBlockEntity;

				if (IsPairLead.Value)
				{
					var inventory = CreateInventoryInternal();
					pair.UpdatePair(inventory);

					return inventory;
				}
				else
				{
					var inventory = pair.GetInventory(level);
					UpdatePair(inventory);

					return inventory;
				}
			}

			return CreateInventoryInternal();
		}

		public override void RemoveBlockEntity(Level level)
		{
			base.RemoveBlockEntity(level);

			if (level.GetBlockEntity(PairCoordinates) is ChestBlockEntity pair 
				&& pair.PairCoordinates == Coordinates)
			{
				pair.RemovePair();
			}
		}

		public bool Pair(Level level, ChestBlockEntity pair)
		{
			if (!CanPair(level, pair)) return false;

			SetPair(pair.Coordinates, true);
			pair.SetPair(Coordinates, false);

			return true;
		}
		
		public void RemovePair()
		{
			if (!IsPaired) return;

			if (Inventory != null && Inventory.Slots is ContainerItemStacks stacks)
			{
				stacks.RemoveContainer(Convert.ToInt32(IsPairLead.Value));
				Inventory.Coordinates = Coordinates;
			}

			IsPairLead = null;
			PairX = null;
			PairZ = null;
		}

		public bool CanPair(Level level, ChestBlockEntity chest)
		{
			CheckPair(level);

			if (IsPaired || chest.IsPaired) return false;
			if ((ForceUnpair ?? false) || (chest.ForceUnpair ?? false)) return false;
			if (Coordinates.Y != chest.Coordinates.Y) return false;

			var diff = (Coordinates - chest.Coordinates).Abs();

			return diff.X == 1 && diff.Z == 0
				|| diff.X == 0 && diff.Z == 1;
		}

		private void CheckPair(Level level)
		{
			if (IsPaired)
			{
				var pair = level.GetBlockEntity(PairCoordinates) as ChestBlockEntity;

				if (pair == null || pair.PairCoordinates != Coordinates)
				{
					RemovePair();
					return;
				}

				if (Inventory != null && IsPairLead.Value)
				{
					pair.UpdatePair(Inventory);
				}
			}
		}

		private void UpdatePair(ContainerInventory inventory)
		{
			if (inventory?.Slots is ContainerItemStacks itemStacks)
			{
				itemStacks.SetContainer(Items, Convert.ToInt32(!(IsPairLead ?? true)));
			}
		}

		private void SetPair(BlockCoordinates pairCoordinates, bool isLead)
		{
			IsPairLead = isLead;
			PairX = pairCoordinates.X;
			PairZ = pairCoordinates.Z;
		}

		private ContainerInventory CreateInventoryInternal()
		{
			return new ContainerInventory(new ContainerItemStacks(Items), Coordinates)
			{
				Type = WindowType
			};
		}

		protected override void OnInventoryOpened(object sender, InventoryOpenedEventArgs args)
		{
			base.OnInventoryOpened(sender, args);

			if (args.Opened && (IsPairLead ?? true))
			{
				args.Player.Level.BroadcastSound(Coordinates, LevelSoundEventType.ChestOpen);
			}
		}

		protected override void OnInventoryClosed(object sender, InventoryClosedEventArgs args)
		{
			base.OnInventoryClosed(sender, args);

			if (args.Closed && (IsPairLead ?? true))
			{
				args.Player.Level.BroadcastSound(Coordinates, LevelSoundEventType.ChestClosed);
			}
		}
	}
}