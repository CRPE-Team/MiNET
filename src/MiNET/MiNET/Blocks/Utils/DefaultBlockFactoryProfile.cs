using System.Collections.Generic;
using System.Reflection;
using fNbt;
using log4net;
using MiNET.Blocks.Utils;
using MiNET.Utils.Nbt;

namespace MiNET.Blocks
{
	public class DefaultBlockFactoryProfile : BlockFactoryProfileBase
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(DefaultBlockFactoryProfile));

		public override bool BlockRuntimeIdsAreHashes => false;


		public byte[] TransparentBlocks { get; private set; }
		public byte[] LuminousBlocks { get; private set; }

		public List<string> RuntimeIdToId { get; private set; }
		
		public override IBlockPalette BlockPalette { get; } = new ListBlockPalette();

		public DefaultBlockFactoryProfile()
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

			int runtimeId = 0;

			using (var stream = assembly.GetManifestResourceStream(typeof(BlockFactory).Namespace + ".Data.canonical_block_states.nbt"))
			{
				do
				{
					var compound = NbtExtensions.ReadNbtCompound(stream);
					var container = BlockUtils.GetBlockStateContainer(compound);

					container.RuntimeId = runtimeId++;
					container.StatesNbt = compound["states"] as NbtCompound;
					container.StatesCacheNbt = container.StatesNbt.ToBytes(NbtFlavor.BedrockNoVarInt);
					BlockPalette.Add(container);

					Ids.Add(container.Id);
				} while (stream.Position < stream.Length);
			}
		}

		public override string GetIdByRuntimeId(int id)
		{
			return id > 0 && id < RuntimeIdToId.Count ? RuntimeIdToId[id] : null;
		}

		public override bool IsTransparent(int runtimeId)
		{
			if (runtimeId < 0 || runtimeId >= TransparentBlocks.Length) return false;

			return TransparentBlocks[runtimeId] == 1;
		}

		private (byte[], byte[]) BuildTransperentAndLuminousMapPair()
		{
			var transparentBlocks = new byte[BlockPalette.Count];
			var luminousBlocks = new byte[BlockPalette.Count];

			for (var i = 0; i < BlockPalette.Count; i++)
			{
				var block = GetBlockByRuntimeId(i);
				if (block != null)
				{
					if (block.IsTransparent)
					{
						transparentBlocks[i] = 1;
					}
					if (block.LightLevel > 0)
					{
						luminousBlocks[i] = (byte) block.LightLevel;
					}
				}
			}

			return (transparentBlocks, luminousBlocks);
		}

		private List<string> BuildRuntimeIdToId()
		{
			var runtimeIdToId = new List<string>();

			for (var i = 0; i < BlockPalette.Count; i++)
			{
				runtimeIdToId.Add(BlockPalette[i].Id);
			}

			return runtimeIdToId;
		}
	}
}
