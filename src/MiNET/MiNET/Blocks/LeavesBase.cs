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
using System.Collections.Generic;
using log4net;
using MiNET.Items;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class LeavesBase : Block
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(LeavesBase));

		public abstract bool PersistentBit { get; set; }

		public abstract bool UpdateBit { get; set; }

		public LeavesBase() : base()
		{
			IsTransparent = true;
			BlastResistance = 1;
			Hardness = 0.2f;
			IsFlammable = true;
		}

		public override void BlockUpdate(Level level, BlockCoordinates blockCoordinates)
		{
			// No decay
			if (PersistentBit) return;
			if (UpdateBit) return;

			UpdateBit = true;

			level.SetBlock(this, false, false, false);
		}

		public override void OnTick(Level level, bool isRandom)
		{
			if (!isRandom) return;
			if (PersistentBit) return;
			if (!UpdateBit) return;

			if (FindLog(level, Coordinates, new List<BlockCoordinates>(), 0))
			{
				UpdateBit = false;
				level.SetBlock(this, false, false, false);
				return;
			}

			var drops = GetDrops(level, null);
			BreakBlock(level, BlockFace.None, drops.Length == 0);
			foreach (var drop in drops)
			{
				level.DropItem(Coordinates, drop);
			}
		}

		public override Item GetItem(Level world, bool blockItem = false)
		{
			return ItemFactory.GetItem(this);
		}

		public override Item[] GetDrops(Level world, Item tool)
		{
			var rnd = new Random();
			if (rnd.Next(20) == 0)
			{
				return this switch
				{
					OakLeaves => [ItemFactory.GetItem<OakSapling>()],
					SpruceLeaves => [ItemFactory.GetItem<SpruceSapling>()],
					BirchLeaves => [ItemFactory.GetItem<BirchSapling>()],
					JungleLeaves => [ItemFactory.GetItem<JungleSapling>()],
					AcaciaLeaves => [ItemFactory.GetItem<AcaciaSapling>()],
					DarkOakLeaves => [ItemFactory.GetItem<DarkOakSapling>()]
				};
			}

			return [];
		}

		private bool FindLog(Level level, BlockCoordinates coord, List<BlockCoordinates> visited, int distance)
		{
			if (visited.Contains(coord)) return false;

			var block = level.GetBlock(coord);
			if (block is LogBase) return true;

			visited.Add(coord);

			if (distance >= 4) return false;

			if (block.GetType() != GetType()) return false;

			// check down
			if (FindLog(level, coord.BlockDown(), visited, distance + 1)) return true;
			// check west
			if (FindLog(level, coord.BlockWest(), visited, distance + 1)) return true;
			// check east
			if (FindLog(level, coord.BlockEast(), visited, distance + 1)) return true;
			// check south
			if (FindLog(level, coord.BlockSouth(), visited, distance + 1)) return true;
			// check north
			if (FindLog(level, coord.BlockNorth(), visited, distance + 1)) return true;
			// check up
			if (FindLog(level, coord.BlockUp(), visited, distance + 1)) return true;

			return false;
		}
	}
}