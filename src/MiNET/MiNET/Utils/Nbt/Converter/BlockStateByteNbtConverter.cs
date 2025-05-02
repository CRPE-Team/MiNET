using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Blocks;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockStateByteNbtConverter : ByteNbtConverter
	{
		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(BlockStateByte));
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateByte) Activator.CreateInstance(type);
			state.Name = name;
			state.Value = (byte) base.Read(stream, type, value, name, settings);

			return state;
		}

		public override void Write(NbtBinaryWriter stream, object value, string name, NbtSerializerSettings settings)
		{
			base.Write(stream, value, ((BlockStateByte) value).Name, settings);
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			base.WriteData(stream, ((BlockStateByte) value).Value, settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var state = (BlockStateByte) Activator.CreateInstance(type);
			state.Name = tag.Name;
			state.Value = (byte) base.FromNbt(tag, type, value, settings);

			return state;
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			var state = (BlockStateInt) value;
			return base.ToNbt(state.Value, state.Name, settings);
		}
	}
}
