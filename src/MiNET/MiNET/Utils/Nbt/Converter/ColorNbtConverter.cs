using System;
using System.Drawing;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;

namespace MiNET.Utils.Nbt.Converter
{
	public class ColorNbtConverter : IntNbtConverter
	{
		public override bool CanConvert(Type type)
		{
			return type == typeof(Color);
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			return Color.FromArgb((int) base.Read(stream, type, value, name, settings));
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			base.WriteData(stream, ((Color) value).ToArgb(), settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			return Color.FromArgb((int) base.FromNbt(tag, type, value, settings));
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			return base.ToNbt(((Color) value).ToArgb(), name, settings);
		}
	}
}
