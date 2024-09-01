using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Items;

namespace MiNET.Utils.Nbt.Converter
{
	public class ItemsNbtConverter : ArrayNbtConverter
	{
		private const string SlotTagName = "Slot";

		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();

		public ItemsNbtConverter() : base(typeof(Item))
		{
		}

		public override bool CanConvert(Type type)
		{
			return type == typeof(Item[]);
		}

		protected override object ReadItem(NbtBinaryReader stream, ref int index, NbtSerializerSettings settings)
		{
			var tag = (NbtTag) TagNbtConverter.Read(stream, typeof(NbtCompound), null, string.Empty, settings);

			return ItemFromNbt(tag, ref index, settings);
		}

		protected override void WriteItem(NbtBinaryWriter stream, object value, int index, NbtSerializerSettings settings)
		{
			var tag = ItemToNbt(value, index, settings);

			TagNbtConverter.WriteData(stream, tag, settings);
		}

		protected override object ItemFromNbt(NbtTag tag, ref int index, NbtSerializerSettings settings)
		{
			index = tag[SlotTagName].ByteValue;
			return base.ItemFromNbt(tag, ref index, settings);
		}

		protected override NbtTag ItemToNbt(object value, int index, NbtSerializerSettings settings)
		{
			var tag = (NbtCompound) base.ItemToNbt(value, index, settings);
			tag.Add(new NbtByte(SlotTagName, (byte) index));

			return tag;
		}
	}
}
