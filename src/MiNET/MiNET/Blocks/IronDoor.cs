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
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public partial class IronDoor : Block
	{
		public IronDoor() : base()
		{
			IsTransparent = true;
			BlastResistance = 25;
			Hardness = 5;
		}

		protected override bool CanPlace(Level world, Player player, BlockCoordinates blockCoordinates, BlockCoordinates targetCoordinates, BlockFace face)
		{
			return world.GetBlock(blockCoordinates).IsReplaceable && world.GetBlock(blockCoordinates + Level.Up).IsReplaceable;
		}

		public override void BreakBlock(Level level, BlockFace face, bool silent = false)
		{
			// Remove door
			if (UpperBlockBit) // Is Upper?
			{
				level.SetAir(Coordinates + Level.Down);
			}
			else
			{
				level.SetAir(Coordinates + Level.Up);
			}

			base.BreakBlock(level, face, silent);
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			Block block = this;
			// Remove door
			if (UpperBlockBit) block = world.GetBlock(GetNewCoordinatesFromFace(blockCoordinates, BlockFace.Down));

			OpenBit = !OpenBit;
			world.SetBlock(block);

			return true;
		}
	}
}