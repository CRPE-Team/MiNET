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
using System.Numerics;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public partial class Torch : Block
	{
		public Torch() : base()
		{
			IsTransparent = true;
			IsSolid = false;
			LightLevel = 14;
		}

		public override void BlockUpdate(Level level, BlockCoordinates blockCoordinates)
		{;
			var face = (BlockFace) TorchFacingDirection;
			if (face == BlockFace.Up) face = BlockFace.Down;

			if (level.GetBlock(Coordinates + face).IsTransparent)
			{
				level.BreakBlock(null, this);
			}
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			return !(PlaceInternal(world, face.Opposite()) || CanPlace(world));
		}

		private bool PlaceInternal(Level world, BlockFace face)
		{
			if (face == BlockFace.Up) return false;

			if (world.GetBlock(Coordinates + face).IsTransparent)
			{
				return false;
			}

			TorchFacingDirection = face;

			return true;
		}

		private bool CanPlace(Level level)
		{
			if (PlaceInternal(level, BlockFace.Down)) return true;

			foreach (var direction in Enum.GetValues<BlockFace>())
			{
				if (PlaceInternal(level, direction))
				{
					return true;
				}
			}

			return false;
		}
	}
}