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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using fNbt.Serialization;
using MiNET.Items;

namespace MiNET.BlockEntities
{
	public class ItemFrameBlockEntity : BlockEntity
	{
		public Item Item { get; set; }

		[NbtProperty("ItemRotation")]
		public float Rotation { get; set; }

		[NbtProperty("ItemDropChance")]
		public float DropChance { get; set; } = 1f;

		public ItemFrameBlockEntity() : base(BlockEntityIds.ItemFrame)
		{
			Item = new ItemAir();
		}

		/// <summary>
		/// Set the rotation value from 0 to 7
		/// </summary>
		/// <param name="rotation"></param>
		public void SetLagacyRotation(int rotation)
		{
			if (rotation < 0 || rotation > 7)
			{
				rotation = 0;
			}

			Rotation = rotation * 45;
		}

		/// <summary>
		/// Get the rotation value from 0 to 7
		/// </summary>
		/// <param name="rotation"></param>
		public int GetLagacyRotation()
		{
			return (int)Math.Clamp(Math.Floor(Rotation / 45), 0, 7);
		}

		public override List<Item> GetDrops()
		{
			return new List<Item> { Item };
		}
	}
}