using System.Collections.Generic;
using System;
using fNbt;
using fNbt.Serialization;
using MiNET.BlockEntities;
using MiNET.Blocks;
using MiNET.Utils.Vectors;
using System.Linq;

namespace MiNET.Worlds.Anvil.Data
{
	[NbtObject]
	public class RegionChunkSection
	{
		private static readonly int WaterBlockRuntimeId = new Water().RuntimeId;
		private static readonly int SnowLayerBlockRuntimeId = new SnowLayer().RuntimeId;


		public byte Y { get; set; }

		[NbtProperty("biomes")]
		public AnvilPalette<string> Biomes { get; set; }

		[NbtProperty("block_states")]
		public AnvilPalette<NbtCompound> BlockStates { get; set; }

		public byte[] BlockLight { get; set; }

		public byte[] SkyLight { get; set; }

		public void PopulateChunk(ChunkColumn chunkColumn, int dataVersion)
		{
			// Y can be up to -4, but the array index starts from 0
			var subChunkId = 4 + (sbyte) Y;

			if (subChunkId < 0 || subChunkId >= ChunkColumn.WorldHeight << 4)
			{
				return;
			}

			var subChunk = (AnvilSubChunk) chunkColumn[subChunkId];

			PopulateChunkBlockStates(chunkColumn, subChunk, dataVersion);
			PopulateChunkBiomes(subChunk);
			PopulateChunkBlockLigths(subChunk);
			PopulateChunkSkyLigths(subChunk);
		}

		private void PopulateChunkBlockStates(ChunkColumn chunkColumn, AnvilSubChunk subChunk, int dataVersion)
		{
			var layer0 = subChunk.Layers[0];
			var layer1 = subChunk.Layers[1];

			var runtimeIds = new List<int>(BlockStates.Palette.Count);
			var blockEntities = new List<BlockEntity>(BlockStates.Palette.Count);
			var waterloggedIds = new List<int>(BlockStates.Palette.Count);
			var snowyIds = new List<int>(BlockStates.Palette.Count);

			foreach (NbtCompound p in BlockStates.Palette)
			{
				AnvilToAnvilPaletteConverter.MapStates(p, dataVersion);
				var id = AnvilToBedrockPaletteConverter.GetRuntimeIdByPalette(p, out var blockEntity);

				waterloggedIds.Add(
					p["Properties"]?["waterlogged"]?.StringValue == "true"
					|| AnvilToBedrockPaletteConverter.IsSeaBlock(BlockFactory.GetIdByRuntimeId(id))
						? id : Block.UnknownRuntimeId);

				//snowyIds.Add(
				//	p["Properties"]?["snowy"]?.StringValue == "true" 
				//		? id : Block.UnknownRuntimeId);

				blockEntities.Add(blockEntity);
				runtimeIds.Add(id);
			}

			var waterRuntimeId = runtimeIds.IndexOf(WaterBlockRuntimeId);
			//var snowRuntimeId = runtimeIds.IndexOf(SnowLayerBlockRuntimeId);
			ushort waterChunkId = 0;
			//ushort snowChunkId = 0;

			layer0.Clear();
			layer0.AppendPaletteRange(runtimeIds);

			var namedRuntimeIds = runtimeIds.Select(id => BlockFactory.GetIdByRuntimeId((short) id)).ToList();

			if (waterloggedIds.Any(id => id != Block.UnknownRuntimeId))
			{
				waterChunkId = layer1.AppedPalette(WaterBlockRuntimeId);
			}
			//if (snowyIds.Any(id => id != Block.UnknownRuntimeId))
			//{
			//	snowChunkId = layer1.AppedPalette(SnowLayerBlockRuntimeId);
			//}

			if (runtimeIds.Count == 1)
			{
				var block = BlockFactory.GetBlockByRuntimeId(runtimeIds.Single());
				chunkColumn.IsAllAir &= block is Air;

				return;
			}

			var blockSize = (byte) Math.Max(4, Math.Ceiling(Math.Log(runtimeIds.Count, 2)));

			var layer0Data = layer0.Data;
			var layer1Data = layer1.Data;
			AnvilDataUtils.ReadAnvilPalettedContainerData(BlockStates.Data, layer0Data, blockSize);

			if (blockEntities.Any() || waterloggedIds.Any())
			{
				for (var i = 0; i != layer0Data.BlocksCount; i++)
				{
					var block = layer0Data[i];
					if (waterloggedIds[block] != Block.UnknownRuntimeId)
					{
						layer1Data[i] = waterChunkId;
					}

					if (blockEntities[block] != null)
					{
						var x = i >> 8;
						var z = i >> 4 & 0xF;
						var y = i & 0xF;

						var template = blockEntities[block];
						template.Coordinates = new BlockCoordinates(
							(subChunk.X << 4) | x,
							((subChunk.Index << 4) + ChunkColumn.WorldMinY) | y,
							(subChunk.Z << 4) | z);

						chunkColumn.SetBlockEntity((BlockEntity) template.Clone());
					}
				}
			}

			chunkColumn.IsAllAir = false;
		}

		private void PopulateChunkBiomes(AnvilSubChunk subChunk)
		{
			var usingBiomes = Biomes.Palette.Select(AnvilToBedrockPaletteConverter.GetBiomeByName).ToArray();

			subChunk.Biomes.Clear();
			subChunk.Biomes.AppendPaletteRange(usingBiomes.Select(biome => biome.Id));

			if (usingBiomes.Length == 1)
			{
				return;
			}

			var bitsPerBlock = (byte) Math.Ceiling(Math.Log(usingBiomes.Length, 2));

			var biomesNoise = new byte[64];
			AnvilDataUtils.ReadAnyBitLengthShortFromLongs(Biomes.Data, biomesNoise, bitsPerBlock);

			subChunk.SetBiomesNoise(biomesNoise);
		}

		private void PopulateChunkBlockLigths(AnvilSubChunk subChunk)
		{
			//if (!ReadBlockLight) return;

			//if (BlockLight == null) return;

			//Array.Copy(SkyLight, subChunk.BlockLight.Data, 0);
		}

		private void PopulateChunkSkyLigths(AnvilSubChunk subChunk)
		{
			//if (!ReadSkyLight) return;

			//if (SkyLight == null) return;

			//Array.Copy(SkyLight, subChunk.SkyLight.Data, 0);
		}
	}
}
