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
using MiNET.Blocks;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Items
{
	public abstract class ItemSignBase : ItemBlock
	{
		protected ItemSignBase() : base()
		{
			MaxStackSize = 16;
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			if (face == BlockFace.Down) return false;

			var id = Id;

			if (!id.Contains("hanging")) id = id.Replace("dark_oak", "darkoak");
			if (id == "minecraft:oak_sign") id = id.Replace("oak_", "");

			if (face == BlockFace.Up)
			{
				if (!id.Contains("hanging")) id = id.Replace("sign", "standing_sign");

				Block = BlockFactory.GetBlockById(id);
			}
			else
			{
				if (!id.Contains("hanging")) id = id.Replace("sign", "wall_sign");

				Block = BlockFactory.GetBlockById(id);
			}

			return base.PlaceBlock(world, player, blockCoordinates, face, faceCoords);
		}
	}
}