using System.Collections.Generic;
using fNbt;

namespace MiNET.Blocks
{
	public static class BlockUtils
	{
		internal static PaletteBlockStateContainer GetBlockStateContainer(NbtTag tag)
		{
			string name = tag["name"].StringValue;

			return new PaletteBlockStateContainer(name, GetBlockStates(tag));
		}

		public static List<IBlockState> GetBlockStates(NbtTag tag)
		{
			var result = new List<IBlockState>();

			var states = tag["states"] ?? tag;
			if (states != null && states is NbtCompound compound)
			{
				foreach (var stateEntry in compound)
				{
					switch (stateEntry)
					{
						case NbtInt nbtInt:
							result.Add(new BlockStateInt()
							{
								Name = nbtInt.Name,
								Value = nbtInt.Value
							});
							break;
						case NbtByte nbtByte:
							result.Add(new BlockStateByte()
							{
								Name = nbtByte.Name,
								Value = nbtByte.Value
							});
							break;
						case NbtString nbtString:
							result.Add(new BlockStateString()
							{
								Name = nbtString.Name,
								Value = nbtString.Value
							});
							break;
					}
				}
			}

			return result;
		}

		public static string GetMetaBlockName(string name, short meta)
		{
			return $"{name}:{meta}";
		}
	}
}