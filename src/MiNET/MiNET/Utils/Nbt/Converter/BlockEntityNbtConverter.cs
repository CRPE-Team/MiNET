using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.BlockEntities;
using MiNET.Utils.Vectors;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockEntityNbtConverter : ObjectNbtConverter
	{
		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();

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
			var tag = (NbtTag) TagNbtConverter.Read(stream, typeof(NbtCompound), value, name, settings);

			return FromNbt(tag, type, value, settings);
		}

		public override void Write(NbtBinaryWriter stream, object value, string name, NbtSerializerSettings settings)
		{
			var tag = ToNbt(value, name, settings);
			TagNbtConverter.Write(stream, tag, name, settings);
		}

		public override void WriteData(NbtBinaryWriter stream, object value, NbtSerializerSettings settings)
		{
			var tag = ToNbt(value, string.Empty, settings);
			TagNbtConverter.WriteData(stream, tag, settings);
		}

		public override object FromNbt(NbtTag tag, Type type, object value, NbtSerializerSettings settings)
		{
			var id = tag["id"]?.StringValue;
			if (id == null) return null;

			var coordinates = new BlockCoordinates(
				tag["x"].IntValue,
				tag["y"].IntValue,
				tag["z"].IntValue);

			var blockEntity = value as BlockEntity ?? BlockEntityFactory.GetBlockEntityById(id);
			blockEntity.Coordinates = coordinates;

			return base.FromNbt(tag, type, blockEntity, settings);
		}

		public override NbtTag ToNbt(object value, string name, NbtSerializerSettings settings)
		{
			var blockEntity = (BlockEntity) value;
			var tag = (NbtCompound) base.ToNbt(value, name, settings);

			tag.Remove(nameof(BlockEntity.Coordinates));
			tag.Add(new NbtInt("x", blockEntity.Coordinates.X));
			tag.Add(new NbtInt("y", blockEntity.Coordinates.Y));
			tag.Add(new NbtInt("z", blockEntity.Coordinates.Z));

			return tag;
		}
	}
}
