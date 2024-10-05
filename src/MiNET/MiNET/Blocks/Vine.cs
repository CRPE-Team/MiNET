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
using log4net;
using MiNET.Blocks.States;
using MiNET.Items;
using MiNET.Particles;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public partial class Vine : Block
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Vine));

		public Vine() : base()
		{
			IsSolid = false;
			IsTransparent = true;
			BlastResistance = 1;
			Hardness = 0.2f;
			IsFlammable = true;
			IsReplaceable = true;
		}

		protected override bool CanPlace(Level world, Player player, BlockCoordinates blockCoordinates, BlockCoordinates targetCoordinates, BlockFace face)
		{
			if (!base.CanPlace(world, player, blockCoordinates, targetCoordinates, face))
			{
				return false;
			}

			var onTop = world.GetBlock(Coordinates.BlockUp()) as Vine;
			if (face == BlockFace.Up || face == BlockFace.Down)
			{
				return onTop != null;
			}

			return CanPlace(world, this, onTop, face.Opposite().ToDirection());
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			if (world.GetBlock(Coordinates) is Vine block)
			{
				VineDirectionBits = block.VineDirectionBits;
			}

			if (face == BlockFace.Up || face == BlockFace.Down)
			{
				var canPlace = false;
				var onTop = world.GetBlock(Coordinates.BlockUp()) as Vine;
				foreach (var direction in Enum.GetValues<Direction>())
				{
					if (VineDirectionBits.HasSide(direction)) continue;
					if (!CanPlace(world, this, onTop, direction)) continue;

					canPlace = true;
					face = direction.ToBlockFace().Opposite();
					break;
				}

				if (!canPlace) return true;
			}

			var vineFace = face.Opposite();

			if (VineDirectionBits.HasSide(vineFace)) return true;

			VineDirectionBits += vineFace;

			return false;
		}

		//public override void BreakBlock(Level level, BlockFace face, bool silent = false)
		//{
		//	Log.Debug($"Breaking vine face {face}, have direction: {VineDirectionBits}");
		//	int newValue = GetDirectionBits(level, this);
		//	switch (face)
		//	{
		//		case BlockFace.North:
		//			newValue &= ~North;
		//			break;
		//		case BlockFace.East:
		//			newValue &= ~East;
		//			break;
		//		case BlockFace.South:
		//			newValue &= ~South;
		//			break;
		//		case BlockFace.West:
		//			newValue &= ~West;
		//			break;
		//	}
		//	Log.Debug($"Breaking vine, new value: {newValue}, old {VineDirectionBits}");
		//	if (newValue != VineDirectionBits)
		//	{
		//		VineDirectionBits = newValue;
		//		if (VineDirectionBits != 0)
		//		{
		//			level.SetBlock(this);
		//		}
		//		else
		//		{
		//			base.BreakBlock(level, face, silent);
		//		}
		//	}
		//}

		public override void BlockUpdate(Level level, BlockCoordinates blockCoordinates)
		{
			var newValue = GetDirectionBits(level, this);
			if (newValue == VineDirectionBits) return;
			
			VineDirectionBits = newValue;

			if (VineDirectionBits == VineDirectionBits.None)
			{
				level.BreakBlock(null, this);
			}
			else
			{
				level.SetBlock(this);
				UpdateBlocks(level);
				new DestroyBlockParticle(level, this).Spawn();
			}
		}

		private static VineDirectionBits GetDirectionBits(Level level, Vine vine)
		{
			var newVineDirectionBits = VineDirectionBits.None;

			var onTop = level.GetBlock(vine.Coordinates.BlockUp()) as Vine;
			foreach (var direction in Enum.GetValues<Direction>())
			{
				if (!vine.VineDirectionBits.HasSide(direction)) continue;
				if (!CanPlace(level, vine, onTop, direction)) continue;

				newVineDirectionBits += direction;
			}

			return newVineDirectionBits;
		}

		public override Item[] GetDrops(Level world, Item tool)
		{
			if (tool.ItemType != ItemType.Sheers) return [];

			return base.GetDrops(world, tool);
		}

		private static bool CanPlace(Level level, Vine vine, Vine onTop, Direction direction)
		{
			var hasSideTop = onTop != null && onTop.VineDirectionBits.HasSide(direction);
			var hasFaceBlockSide = level.GetBlock(vine.Coordinates + direction).IsSolid;

			return hasSideTop || hasFaceBlockSide;
		}
	}
}