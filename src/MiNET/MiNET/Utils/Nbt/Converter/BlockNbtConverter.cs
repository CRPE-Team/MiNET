using System;
using fNbt;
using fNbt.Serialization;
using fNbt.Serialization.Converters;
using MiNET.Blocks;

namespace MiNET.Utils.Nbt.Converter
{
	public class BlockNbtConverter : ObjectNbtConverter
	{
		private static readonly TagNbtConverter TagNbtConverter = new TagNbtConverter();

		public override bool CanWrite => false;

		public override bool CanConvert(Type type)
		{
			return type.IsAssignableTo(typeof(Block));
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
			var states = BlockFactory.GetBlockStateContainer(tag);
			var existingBlock = value as Block;

			if (existingBlock == null || existingBlock.Id != states.Id)
			{
				if (BlockFactory.BlockStates.TryGetValue(states, out var blockState))
				{
					return BlockFactory.GetBlockByRuntimeId(blockState.RuntimeId);
				}
			}
			else
			{
				existingBlock.SetStates(states);
			}

			return existingBlock;
		}
	}
}
