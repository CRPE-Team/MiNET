using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using fNbt;
using log4net;
using MiNET.Blocks.Utils;
using MiNET.Utils.Nbt;

namespace MiNET.Blocks
{
	public class HashBlockFactoryProfile : BlockFactoryProfileBase
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(HashBlockFactoryProfile));

		private bool _loaded = false;

		public override bool BlockRuntimeIdsAreHashes => true;


		public Dictionary<int, byte> TransparentBlocks { get; private set; }
		public Dictionary<int, byte> LuminousBlocks { get; private set; }

		public Dictionary<int, string> RuntimeIdToId { get; private set; }

		public override IBlockPalette BlockPalette { get; } = new HashBlockPalette();

		public HashBlockFactoryProfile()
		{
			
		}

		public override bool Load()
		{
			if (!base.Load()) return false;

			RuntimeIdToId = BuildRuntimeIdToId();
			(TransparentBlocks, LuminousBlocks) = BuildTransperentAndLuminousMapPair();

			return true;
		}

		protected override void OnLoad()
		{
			var assembly = Assembly.GetAssembly(typeof(Block));

			using (var stream = assembly.GetManifestResourceStream(typeof(BlockFactory).Namespace + ".Data.canonical_block_states.nbt"))
			{
				do
				{
					var compound = NbtExtensions.ReadNbtCompound(stream);
					var container = BlockUtils.GetBlockStateContainer(compound);

					compound.Remove("version");

					container.RuntimeId = (int) compound.GetFnvHash(NbtFlavor.BedrockNoVarInt);
					container.StatesNbt = compound["states"] as NbtCompound;
					container.StatesCacheNbt = container.StatesNbt.ToBytes(NbtFlavor.BedrockNoVarInt);
					BlockPalette.Add(container);

					Ids.Add(container.Id);
				} while (stream.Position < stream.Length);
			}
		}

		public override string GetIdByRuntimeId(int id)
		{
			return RuntimeIdToId.GetValueOrDefault(id);
		}

		public override bool IsTransparent(int runtimeId)
		{
			return TransparentBlocks.TryGetValue(runtimeId, out var transparency) && transparency == 1;
		}

		private (Dictionary<int, byte>, Dictionary<int, byte>) BuildTransperentAndLuminousMapPair()
		{
			var transparentBlocks = new Dictionary<int, byte>(BlockPalette.Count);
			var luminousBlocks = new Dictionary<int, byte>(BlockPalette.Count);

			foreach (var state in BlockPalette)
			{
				var block = GetBlockByRuntimeId(state.RuntimeId);
				if (block != null)
				{
					if (block.IsTransparent)
					{
						transparentBlocks.Add(block.RuntimeId, 1);
					}

					if (block.LightLevel > 0)
					{
						luminousBlocks.Add(block.RuntimeId, (byte) block.LightLevel);
					}
				}
			}

			return (transparentBlocks, luminousBlocks);
		}

		private Dictionary<int, string> BuildRuntimeIdToId()
		{
			return BlockPalette.ToDictionary(c => c.RuntimeId, c => c.Id);
		}
	}
}
