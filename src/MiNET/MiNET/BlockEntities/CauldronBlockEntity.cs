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