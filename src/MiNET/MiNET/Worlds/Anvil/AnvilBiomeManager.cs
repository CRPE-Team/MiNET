using System;
using MiNET.Utils.Vectors;
using System.Security.Cryptography;

namespace MiNET.Worlds.Anvil
{
	public class AnvilBiomeManager
	{
		private static readonly int MinNoiseY = FromBlock(ChunkColumn.WorldMinY);
		private static readonly int MaxNoiseY = MinNoiseY + FromBlock(ChunkColumn.WorldHeight) - 1;

		private Lazy<long> _obfuscatedSeed;

		public long ObfuscatedSeed => _obfuscatedSeed.Value;

		private readonly AnvilWorldProvider _worldProvider;

		public AnvilBiomeManager(AnvilWorldProvider worldProvider)
		{
			_worldProvider = worldProvider;

			_obfuscatedSeed = new Lazy<long>(() => ObfuscateSeed(_worldProvider.LevelInfo.Seed));
		}

		public byte GetNoiseBiome(int x, int y, int z)
		{
			var chunk = _worldProvider.GenerateChunkColumn(new ChunkCoordinates(FromBlock(x), FromBlock(z)));

			int fixedY = Math.Clamp(y, MinNoiseY, MaxNoiseY);
			int j = GetSectionIndex(ToBlock(fixedY));
			var subChunk = chunk[j];

			if (subChunk is AnvilSubChunk section)
			{
				return section.GetNoiseBiome(x, fixedY, z);
			}
			else
			{
				return subChunk.GetBiome((x << 2) & 0xf, (fixedY << 2) & 0xf, (z << 2) & 0xf);
			}
		}

		private static int FromBlock(int value)
		{
			return value >> 2;
		}

		private static int ToBlock(int value)
		{
			return value << 2;
		}

		private static int GetSectionIndex(int value)
		{
			return BlockToSectionCoord(value) + 4;
		}

		private static int BlockToSectionCoord(int value)
		{
			return value >> 4;
		}

		private static long ObfuscateSeed(long seed)
		{
			using (var sha256Hash = SHA256.Create())
			{
				var bytes = sha256Hash.ComputeHash(BitConverter.GetBytes(seed));
				return BitConverter.ToInt64(bytes);
			}
		}
	}
}
