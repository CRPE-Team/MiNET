using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Blocks;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockStateStringNbtConverter : StringNbtConverter
	{
		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(BlockStateString));
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateString) Activator.CreateInstance(type);
			state.Name = name;
			state.Value = (string) base.Read(stream, type, value, name, settings);

			return state;
		}

		public override void Write(NbtBinaryWriter stream, object value, string name, NbtSerializerSettings settings)
		{
			base.Write(stream, value, ((BlockStateString) value).Name, settings);
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			base.WriteData(stream, ((BlockStateString) value).Value, settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var state = (BlockStateString) Activator.CreateInstance(type);
			state.Name = tag.Name;
			state.Value = (string) base.FromNbt(tag, type, value, settings);

			return state;
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateInt) value;
			return base.ToNbt(state.Value, state.Name, settings);
		}
	}
}
