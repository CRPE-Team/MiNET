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
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class DoorBase : Block
	{
		public abstract CardinalDirection Direction { get; set; }
		public abstract bool DoorHingeBit { get; set; }
		public abstract bool OpenBit { get; set; }
		public abstract bool UpperBlockBit { get; set; }

		private BlockCoordinates SecondPartCoordinates => UpperBlockBit ? Coordinates.BlockDown() : Coordinates.BlockUp();

		protected DoorBase() : base()
		{
			IsTransparent = true;
			BlastResistance = 15;
			Hardness = 3;
		}

		protected override bool CanPlace(Level world, Player player, BlockCoordinates blockCoordinates, BlockCoordinates targetCoordinates, BlockFace face)
		{
			return world.GetBlock(blockCoordinates).IsReplaceable && world.GetBlock(blockCoordinates.BlockUp()).IsReplaceable;
		}

		public override void BreakBlock(Level level, BlockFace face, bool silent = false)
		{
			var secondPart = level.GetBlock(SecondPartCoordinates);

			BreakBlockInternal(level, face, silent);
			if (secondPart is DoorBase secondPartDoor)
			{
				secondPartDoor.BreakBlockInternal(level, face, silent);
			}
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			DoorBase block = this;
			if (UpperBlockBit)
			{
				block = (DoorBase) world.GetBlock(SecondPartCoordinates);
			}

			block.OpenBit = !block.OpenBit;
			world.SetBlock(block);

			return true;
		}

		private void BreakBlockInternal(Level level, BlockFace face, bool silent = false)
		{
			base.BreakBlock(level, face, silent);
		}
	}
}