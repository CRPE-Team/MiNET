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

		private static readonly ByteNbtConverter ByteNbtConverter = new ByteNbtConverter();
		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();

		public bool WriteSlots { get; set; } = true;

		public ItemsNbtConverter() : base(typeof(Item))
		{
		}

		public override bool CanConvert(Type type)
		{
			return type == typeof(Item[]);
		}

		protected override object ReadItem(NbtBinaryReader stream, ref int index, NbtSerializerSettings settings)
		{
			if (WriteSlots)
			{
				var tag = (NbtTag) TagNbtConverter.Read(stream, typeof(NbtCompound), null, string.Empty, settings);

				return ItemFromNbt(tag, ref index, settings);
			}
			else
			{
				return base.ReadItem(stream, ref index, settings);
			}
		}

		protected override void WriteItem(NbtBinaryWriter stream, object value, int index, NbtSerializerSettings settings)
		{
			if (WriteSlots)
			{
				ByteNbtConverter.Write(stream, (byte) index, SlotTagName, settings);
			}

			base.WriteItem(stream, value, index, settings);
		}

		protected override object ItemFromNbt(NbtTag tag, ref int index, NbtSerializerSettings settings)
		{
			if (WriteSlots)
			{
				index = tag[SlotTagName].ByteValue;
			}

			return base.ItemFromNbt(tag, ref index, settings);
		}

		protected override NbtTag ItemToNbt(object value, int index, NbtSerializerSettings settings)
		{
			var tag = (NbtCompound) base.ItemToNbt(value, index, settings);

			if (WriteSlots)
			{
				tag.Add(new NbtByte(SlotTagName, (byte) index));
			}

			return tag;
		}
	}
}
