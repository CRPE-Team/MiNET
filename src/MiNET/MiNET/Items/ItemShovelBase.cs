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

using System.Numerics;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Items
{
	public abstract class ItemShovelBase : Item
	{
		internal ItemShovelBase() : base()
		{
			MaxStackSize = 1;
			ItemType = ItemType.Shovel;
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			Block block = world.GetBlock(blockCoordinates);
			if (block is GrassBlock)
			{
				GrassPath grassPath = new GrassPath
				{
					Coordinates = blockCoordinates,
				};
				world.SetBlock(grassPath);
				player.Inventory.DamageItemInHand(ItemDamageReason.BlockInteract, null, block);
				return true;
			}

			return false;
		}

		public override bool DamageItem(Player player, ItemDamageReason reason, Entity target, Block block)
		{
			switch (reason)
			{
				case ItemDamageReason.BlockBreak:
				{
					Metadata++;
					return Metadata >= GetMaxUses() - 1;
				}
				case ItemDamageReason.BlockInteract:
				{
					if (block is GrassBlock)
					{
						Metadata++;
						return Metadata >= GetMaxUses() - 1;
					}
					return false;
				}
				case ItemDamageReason.EntityAttack:
				{
					Metadata += 2;
					return Metadata >= GetMaxUses() - 1;
				}
				default:
					return false;
			}
		}
	}
}