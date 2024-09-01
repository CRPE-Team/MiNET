using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Items;

namespace MiNET.Utils.Nbt.Converter
{
	public class ItemNbtConverter : ObjectNbtConverter
	{
		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();

		public override bool CanWrite => false;

		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(Item));
		}

		public override NbtTagType GetTagType(Type type, NbtSerializerSettings settings)
		{
			return NbtTagType.Compound;
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			var tag = (NbtCompound) TagNbtConverter.Read(stream, typeof(NbtCompound), value, name, settings);

			return FromNbt(tag, type, value, settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var id = tag["Name"].StringValue;

			var item = ItemFactory.GetItem(id);
			return base.FromNbt(tag, item.GetType(), item, settings);
		}
	}
}
