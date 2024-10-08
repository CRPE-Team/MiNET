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
using System.Numerics;
using MiNET.BlockEntities;
using MiNET.Blocks;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Items
{
	public partial class ItemSkull
	{
		public ItemSkull() : base()
		{

		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			var coor = GetNewCoordinatesFromFace(blockCoordinates, face);
			if (face == BlockFace.Up) // On top of block
			{
				var skull = new Skull();
				skull.Coordinates = coor;
				skull.FacingDirection = BlockFace.Up; // Skull on floor, rotation in block entity
				world.SetBlock(skull);
			}
			else if (face == BlockFace.Down) // At the bottom of block
			{
				// Doesn't work, ignore if that happen. 
				return false;
			}
			else
			{
				var skull = new Skull();
				skull.Coordinates = coor;
				skull.FacingDirection = face; // Skull on floor, rotation in block entity
				world.SetBlock(skull);
			}

			// Then we create and set the sign block entity that has all the intersting data

			var skullBlockEntity = new SkullBlockEntity
			{
				Coordinates = coor,
				Rotation = (byte) player.KnownPosition.GetDirection16(),
				SkullType = (byte) Metadata
			};


			world.SetBlockEntity(skullBlockEntity);

			if (player.GameMode == GameMode.Survival)
			{
				var itemInHand = player.Inventory.GetItemInHand();
				itemInHand.Count--;
				player.Inventory.SetInventorySlot(player.Inventory.InHandSlot, itemInHand);
			}

			return true;
		}
	}
}