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

using fNbt;
using MiNET.Items;

namespace MiNET.BlockEntities
{
	public class CauldronBlockEntity : BlockEntity
	{
		public int? CustomColor { get; set; }
		public Item[] Items { get; set; }
		public short PotionId { get; set; } = -1;
		public short PotionType { get; set; } = -1;

		public CauldronBlockEntity() : base(BlockEntityIds.Cauldron)
		{
			Items = [ new ItemAir() ];
		}

		public override NbtCompound GetCompound()
		{

			var items = new NbtList("Items");
			for (byte i = 0; i < Items.Length; i++)
			{
				var itemTag = Items[i].ToNbt();
				itemTag.Add(new NbtByte("Slot", i));

				items.Add(itemTag);
			}

			var compound = new NbtCompound(string.Empty)
			{
				new NbtString("id", Id),
				new NbtInt("x", Coordinates.X),
				new NbtInt("y", Coordinates.Y),
				new NbtInt("z", Coordinates.Z),
				items,
				new NbtShort(nameof(PotionId), PotionId),
				new NbtShort(nameof(PotionType), PotionType)
			};

			if (CustomColor.HasValue)
			{
				compound.Add(new NbtInt(nameof(CustomColor), CustomColor.Value));
			}

			return compound;
		}

		public override void SetCompound(NbtCompound compound)
		{
			CustomColor = compound[nameof(CustomColor)]?.IntValue;
			PotionId = compound[nameof(PotionId)].ShortValue;
			PotionType = compound[nameof(PotionType)].ShortValue;

			foreach (var item in (NbtList) compound["Items"])
			{
				Items[item["Slot"].ByteValue] = ItemFactory.FromNbt(item);
			}
		}
	}
}