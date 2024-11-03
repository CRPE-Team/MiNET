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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using fNbt.Serialization;
using log4net;
using MiNET.Blocks;
using MiNET.Inventories;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils;
using MiNET.Worlds;

namespace MiNET.BlockEntities
{
	public class FurnaceBlockEntityBase<TFurnace, TLitFurnace> : ContainerBlockEntityBase 
		where TFurnace : FurnaceBase, new() 
		where TLitFurnace : FurnaceBase, new()
	{

		private static readonly ILog Log = LogManager.GetLogger(typeof(FurnaceBlockEntityBase<,>));

		private static readonly string BlockId = BlockFactory.GetIdByType<TFurnace>(false);

		public short CookTime { get; set; }
		public short BurnTime { get; set; }

		[NbtProperty("BurnDuration")]
		public short FuelEfficiency { get; set; }

		[NbtIgnore]
		public short SmeltingTime { get; }

		public FurnaceBlockEntityBase(string id, WindowType windowType, short smeltingTime) : base(id, 3, windowType)
		{
			SmeltingTime = smeltingTime;
			UpdatesOnTick = true;
		}

		public override void OnTick(Level level)
		{
			if (Inventory == null) return;

			var fuel = GetFuel();
			var ingredient = GetIngredient();
			var smelt = ingredient.GetSmelt(BlockId);

			var isLit = BurnTime > 0 || CanBurn(fuel, smelt) && BurnFuel(fuel);

			if (isLit)
			{
				BurnTime--;
				if (smelt != null)
				{
					if (++CookTime >= SmeltingTime)
					{
						if (Inventory.DecreaseSlot(0))
						{
							Inventory.IncreaseSlot(2, smelt.Id, smelt.Metadata);
						}

						CookTime = 0;
					}
				}
				else
				{
					CookTime = 0;
				}
			}

			UpdateStates(level, isLit);
		}

		protected override void OnInventoryOpened(object sender, InventoryOpenedEventArgs args)
		{
			base.OnInventoryOpened(sender, args);

			SendContainerData(args.Player);
		}

		private bool BurnFuel(Item fuel)
		{
			if (!Inventory.DecreaseSlot(1)) return false;

			FuelEfficiency = (short) (fuel.FuelEfficiency * SmeltingTime / 10);
			BurnTime = FuelEfficiency;

			return true;
		}

		private bool CanBurn(Item fuel, Item smelt)
		{
			// To light a furnace you need both fule and proper ingredient.
			if (fuel is ItemAir || fuel.FuelEfficiency <= 0 || smelt == null) return false;

			var result = GetResult();
			return result is ItemAir || result.Equals(smelt);
		}

		private void UpdateStates(Level level, bool lit)
		{
			var needReset = CookTime > 0 || BurnTime > 0 || FuelEfficiency > 0;

			if (!lit)
			{
				FuelEfficiency = 0;
				BurnTime = 0;
				CookTime = 0;

				if (!needReset)
				{
					return;
				}
			}

			BroadcastContainerData();

			var oldFurnace = level.GetBlock(Coordinates) as FurnaceBase;
			if (oldFurnace == null)
			{
				Log.Warn($"Attempt to update [{Id}] at [{Coordinates}] without a block");

				UpdatesOnTick = false;
				return;
			}

			if (lit != oldFurnace is TLitFurnace)
			{
				FurnaceBase newFurnace = lit ? new TLitFurnace() : new TFurnace();

				newFurnace.Coordinates = oldFurnace.Coordinates;
				newFurnace.CardinalDirection = oldFurnace.CardinalDirection;

				level.SetBlock(newFurnace);
			}
		}

		private void BroadcastContainerData()
		{
			foreach (var observer in Inventory.Observers)
			{
				SendContainerData(observer);
			}
		}

		private void SendContainerData(Player player)
		{
			var cookTimeSetData = McpeContainerSetData.CreateObject();
			cookTimeSetData.windowId = Inventory.WindowsId;
			cookTimeSetData.property = 0;
			cookTimeSetData.value = CookTime;
			player.SendPacket(cookTimeSetData);

			var burnTimeSetData = McpeContainerSetData.CreateObject();
			burnTimeSetData.windowId = Inventory.WindowsId;
			burnTimeSetData.property = 1;
			burnTimeSetData.value = BurnTime;
			player.SendPacket(burnTimeSetData);

			var fuelEfficientySetData = McpeContainerSetData.CreateObject();
			fuelEfficientySetData.windowId = Inventory.WindowsId;
			fuelEfficientySetData.property = 2;
			fuelEfficientySetData.value = FuelEfficiency;
			player.SendPacket(fuelEfficientySetData);
		}

		private Item GetResult()
		{
			return Items[2];
		}

		private Item GetFuel()
		{
			return Items[1];
		}

		private Item GetIngredient()
		{
			return Items[0];
		}
	}
}