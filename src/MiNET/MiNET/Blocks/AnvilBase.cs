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
using MiNET.Blocks.States;
using MiNET.Inventories;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class AnvilBase : Block
	{
		public abstract CardinalDirection Direction { get; set; }

		public AnvilBase() : base()
		{
			IsTransparent = true;
			BlastResistance = 6000;
			Hardness = 5;
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			Direction = player.KnownPosition.GetDirection().Shift();

			return false;
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			new Inventory(Coordinates, WindowType.Anvil).Open(player);

			//var sendSlot = McpeInventorySlot.CreateObject();
			//sendSlot.inventoryId = 14;
			//sendSlot.slot = (uint) 1;
			//sendSlot.uniqueid = 1;
			//sendSlot.item = new ItemIronShovel();
			//player.SendPacket(sendSlot);

			return true;
		}
	}
}