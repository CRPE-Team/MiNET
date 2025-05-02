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

using MiNET.Inventories;

namespace MiNET.BlockEntities
{
	public interface ICustomBlockEntityFactory
	{
		BlockEntity GetBlockEntityById(string blockEntityId);
	}

	public static class BlockEntityFactory
	{
		public static ICustomBlockEntityFactory CustomBlockEntityFactory { get; set; }

		public static BlockEntity GetBlockEntityById(string blockEntityId)
		{
			BlockEntity blockEntity = CustomBlockEntityFactory?.GetBlockEntityById(blockEntityId);

			if (blockEntity != null)
			{
				return blockEntity;
			}

			return blockEntityId switch
			{
				BlockEntityIds.Sign => new SignBlockEntity(),
				BlockEntityIds.HangingSign => new HangingSignBlockEntity(),
				BlockEntityIds.Chest => new ChestBlockEntity(),
				BlockEntityIds.EnderChest => new ChestBlockEntity(),
				BlockEntityIds.EnchantTable => new EnchantingTableBlockEntity(),
				BlockEntityIds.Furnace => new FurnaceBlockEntity(),
				BlockEntityIds.BlastFurnace => new BlastFurnaceBlockEntity(),
				BlockEntityIds.Skull => new SkullBlockEntity(),
				BlockEntityIds.ItemFrame => new ItemFrameBlockEntity(),
				BlockEntityIds.Bed => new BedBlockEntity(),
				BlockEntityIds.Banner => new BannerBlockEntity(),
				BlockEntityIds.FlowerPot => new FlowerPotBlockEntity(),
				BlockEntityIds.Beacon => new BeaconBlockEntity(),
				BlockEntityIds.MobSpawner => new MobSpawnerBlockEntity(),
				BlockEntityIds.ChalkboardBlock => new ChalkboardBlockEntity(),
				BlockEntityIds.ShulkerBox => new ShulkerBoxBlockEntity(),
				BlockEntityIds.StructureBlock => new StructureBlockBlockEntity(),
				BlockEntityIds.Cauldron => new CauldronBlockEntity(),
				_ => blockEntity
			};
		}

		public static bool TryGetWindowTypeById(string blockEntityId, out WindowType windowType)
		{
			windowType = blockEntityId switch
			{
				BlockEntityIds.Barrel => WindowType.Container,
				BlockEntityIds.Beacon => WindowType.Beacon,
				BlockEntityIds.BlastFurnace => WindowType.BlastFurnace,
				BlockEntityIds.BrewingStand => WindowType.BrewingStand,
				BlockEntityIds.Chest => WindowType.Container,
				BlockEntityIds.EnchantTable => WindowType.Enchantment,
				BlockEntityIds.EnderChest => WindowType.Container,
				BlockEntityIds.Furnace => WindowType.Furnace,
				BlockEntityIds.Hopper => WindowType.Hopper,
				BlockEntityIds.Lectern => WindowType.Lectern,
				BlockEntityIds.ShulkerBox => WindowType.Container,
				BlockEntityIds.Smoker => WindowType.Smoker,
				BlockEntityIds.StructureBlock => WindowType.StructureEditor,

				_ => WindowType.None
			};

			return windowType != WindowType.None;
		}
	}
}