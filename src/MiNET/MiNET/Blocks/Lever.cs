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

using System.Numerics;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;
using MiNET.Blocks.States;
using Direction = MiNET.Utils.Direction;

namespace MiNET.Blocks
{
	public partial class Lever : Block
	{
		public Lever() : base()
		{
			IsTransparent = true;
			IsSolid = false;
			BlastResistance = 2.5f;
			Hardness = 0.5f;
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			var direction = player.KnownPosition.ToDirection();

			switch (face)
			{
				case BlockFace.Down:
					if (direction == Direction.West || direction == Direction.East) LeverDirection = LeverDirection.DownNorthSouth;
					else LeverDirection = LeverDirection.DownEastWest;
					break;
				case BlockFace.North:
					LeverDirection = LeverDirection.North;
					break;
				case BlockFace.South:
					LeverDirection = LeverDirection.South;
					break;
				case BlockFace.West:
					LeverDirection = LeverDirection.West;
					break;
				case BlockFace.East:
					LeverDirection = LeverDirection.East;
					break;
				case BlockFace.Up:
					if (direction == Direction.West || direction == Direction.East) LeverDirection = LeverDirection.UpNorthSouth;
					else LeverDirection = LeverDirection.UpEastWest;
					break;
			}

			return false;
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			OpenBit = !OpenBit;
			world.SetBlock(this);

			return true;
		}
	}
}