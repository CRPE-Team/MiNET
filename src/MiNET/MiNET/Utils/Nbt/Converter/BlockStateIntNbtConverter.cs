using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Blocks;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockStateIntNbtConverter : IntNbtConverter
	{
		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(BlockStateInt));
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateInt) Activator.CreateInstance(type);
			state.Name = name;
			state.Value = (int) base.Read(stream, type, value, name, settings);

			return state;
		}

		public override void Write(NbtBinaryWriter stream, object value, string name, NbtSerializerSettings settings)
		{
			base.Write(stream, value, ((BlockStateInt) value).Name, settings);
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			base.WriteData(stream, ((BlockStateInt) value).Value, settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var state = (BlockStateInt) Activator.CreateInstance(type);
			state.Name = tag.Name;
			state.Value = (int) base.FromNbt(tag, type, value, settings);

			return state;
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateInt) value;
			return base.ToNbt(state.Value, state.Name, settings);
		}
	}
}
