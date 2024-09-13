using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.BlockEntities;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockEntityNbtConverter : ObjectNbtConverter
	{
		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();
		private static readonly IntNbtConverter IntNbtConverter = new IntNbtConverter();

		public override bool CanWrite => false;

		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(BlockEntity));
		}

		public override NbtTagType GetTagType(Type type, NbtSerializerSettings settings)
		{
			return NbtTagType.Compound;
		}

		public override object Read(NbtBinaryReader stream, Type type, object value, string name, NbtSerializerSettings settings)
		{
			if (value != null)
			{
				return base.Read(stream, type, value, name, settings);
			}

			var tag = (NbtTag) TagNbtConverter.Read(stream, typeof(NbtCompound), value, name, settings);

			return FromNbt(tag, type, value, settings);
		}

		public override void Write(NbtBinaryWriter stream, object value, string name, NbtSerializerSettings settings)
		{
			throw new NotImplementedException();
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			throw new NotImplementedException();
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var id = tag["id"]?.StringValue;
			if (id == null) return null;

			var blockEntity = value as BlockEntity ?? BlockEntityFactory.GetBlockEntityById(id);

			return base.FromNbt(tag, type, blockEntity, settings);
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			throw new NotImplementedException();
		}
	}
}
