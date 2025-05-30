﻿using log4net;
using MiNET.Worlds.Utils;

namespace MiNET.Worlds.Anvil
{
	public class AnvilSubChunk : SubChunk
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(AnvilSubChunk));

		private byte[] _biomesNoise = null;
		private bool _biomesResolved = true;

		private readonly object _sync = new object();

		private AnvilBiomeManager _biomeManager;

		public AnvilSubChunk()
			: base()
		{

		}

		public AnvilSubChunk(AnvilBiomeManager biomeManager, int x, int z, int index, bool clearBuffers = true)
			: base(x, z, index, clearBuffers)
		{
			_biomeManager = biomeManager;

			if (clearBuffers)
			{
				ClearBuffers();
			}
		}

		internal override PalettedContainer Biomes 
		{
			get
			{
				if (!_biomesResolved)
				{
					lock (_sync)
					{
						if (!_biomesResolved)
						{
							ResolveBiomes();
						}
					}
				}

				return base.Biomes;
			}
		}

		public override object Clone()
		{
			var cc = base.Clone() as AnvilSubChunk;

			cc._biomeManager = _biomeManager;

			if (_biomesNoise != null)
			{
				cc._biomesNoise = new byte[_biomesNoise.Length];
				_biomesNoise.CopyTo(cc._biomesNoise, 0);
			}
			cc._biomesResolved = _biomesResolved;

			return cc;
		}

		public override void Dispose()
		{
			//_biomesNoise = null;
			base.Dispose();
		}

		internal void SetBiomesNoise(byte[] biomesNoise)
		{
			_biomesNoise = biomesNoise;
			_biomesResolved = false;
		}

		internal byte GetNoiseBiome(int x, int y, int z)
		{
			var palette = base.Biomes.Palette;
			if (palette.Count == 0) return 0;

			var paletteIndex = GetNoiseBiomeIndex(x, y, z);
			if (paletteIndex >= palette.Count || paletteIndex < 0)
			{
				Log.Error($"Can't read biome index [{paletteIndex}] from [{(X << 4) | x}, {(Index << 4) + ChunkColumn.WorldMinY + y}, {(Z << 4) | z}] " +
					$"in ids [{string.Join(", ", palette)}] of chunk [{X}, {Index + (ChunkColumn.WorldMinY >> 4)}, {Z}]");
				return 0;
			}

			return (byte) palette[paletteIndex];
		}

		internal byte GetNoiseBiomeIndex(int x, int y, int z)
		{
			return _biomesNoise?[GetNoiseIndex(x & 3, y & 3, z & 3)] ?? 0;
		}

		private void ResolveBiomes()
		{
			var biomes = base.Biomes.Data;

			if (_biomesNoise == null)
			{
				_biomesResolved = true;
				return;
			}

			var seed = _biomeManager.ObfuscatedSeed;

			var cX = X << 4;
			var cZ = Z << 4;
			var cIndex = (Index << 4) + ChunkColumn.WorldMinY;
			for (var i = 0; i < 4096; i++)
			{
				var x = cX | (i >> 8);
				var z = cZ | ((i >> 4) & 0xF);
				var y = cIndex | (i & 0xF);

				biomes[i] = GetBiomeIndexFromeNoisedCoordinates(x, y, z, seed);
			}

			_biomesResolved = true;
		}

		private ushort GetBiomeIndexFromeNoisedCoordinates(int x, int y, int z, long seed)
		{
			unchecked
			{
				int leftX = x - 2;
				int leftY = y - 2;
				int leftZ = z - 2;
				int oNoiseX = leftX >> 2;
				int oNoiseY = leftY >> 2;
				int oNoiseZ = leftZ >> 2;
				double oDistX = (leftX & 3) / 4.0D;
				double oDistY = (leftY & 3) / 4.0D;
				double oDistZ = (leftZ & 3) / 4.0D;

				int minSet = 0;

				var minDist = double.PositiveInfinity;
				for (int set = 0; set < 2; ++set)
				{
					bool flagZ = (set & 1) == 0;
					int sNoiseZ = flagZ ? oNoiseZ : oNoiseZ + 1;
					double sDistZ = flagZ ? oDistZ : oDistZ - 1.0D;
					double dist1 = FiddleCalculator.GetFiddledDistance(seed, oNoiseX, oNoiseY, sNoiseZ, oDistX, oDistY, sDistZ);
					double dist2 = FiddleCalculator.GetFiddledDistance(seed, oNoiseX, oNoiseY + 1, sNoiseZ, oDistX, oDistY - 1.0D, sDistZ);
					double dist3 = FiddleCalculator.GetFiddledDistance(seed, oNoiseX + 1, oNoiseY, sNoiseZ, oDistX - 1.0D, oDistY, sDistZ);
					double dist4 = FiddleCalculator.GetFiddledDistance(seed, oNoiseX + 1, oNoiseY + 1, sNoiseZ, oDistX - 1.0D, oDistY - 1.0D, sDistZ);
					if (minDist > dist1)
					{
						minSet = set;
						minDist = dist1;
					}
					if (minDist > dist2)
					{
						minSet = set | 2;
						minDist = dist2;
					}
					if (minDist > dist3)
					{
						minSet = set | 4;
						minDist = dist3;
					}
					if (minDist > dist4)
					{
						minSet = set | 6;
						minDist = dist4;
					}
				}

				int noiseX = (minSet & 4) == 0 ? oNoiseX : oNoiseX + 1;
				int noiseY = (minSet & 2) == 0 ? oNoiseY : oNoiseY + 1;
				int noiseZ = (minSet & 1) == 0 ? oNoiseZ : oNoiseZ + 1;


				if (X == noiseX >> 2 && Z == noiseZ >> 2 && Index - 4 == noiseY >> 2)
				{
					return GetNoiseBiomeIndex(noiseX, noiseY, noiseZ);
				}

				var biome = _biomeManager.GetNoiseBiome(noiseX, noiseY, noiseZ);

				return base.Biomes.GetPalettedId(biome);
			}
		}

		private static int GetNoiseIndex(int x, int y, int z)
		{
			return (y << 2 | z) << 2 | x;
		}

		public class FiddleCalculator
		{
			private const long MULTIPLIER = 6364136223846793005L;
			private const long INCREMENT = 1442695040888963407L;
			private const double INV_SCALE = 0.9D / 1024.0D;

			public static double GetFiddledDistance(long seed, int x, int y, int z, double distX, double distY, double distZ)
			{
				//agressive optimisation
				unchecked
				{
					long __7 = seed * (seed * MULTIPLIER + INCREMENT) + x;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + y;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + z;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + x;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + y;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + z;
					double rDistX = ((__7 >> 24 & 1023) - 512) * INV_SCALE + distX;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + seed;
					double rDistY = ((__7 >> 24 & 1023) - 512) * INV_SCALE + distY;
					__7 = __7 * (__7 * MULTIPLIER + INCREMENT) + seed;
					double rDistZ = ((__7 >> 24 & 1023) - 512) * INV_SCALE + distZ;
					return rDistX * rDistX + rDistY * rDistY + rDistZ * rDistZ;
				}
			}
		}
	}
}
