#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using MiNET.Blocks;
using MiNET.Utils;
using MiNET.Worlds.IO;

namespace MiNET.Worlds
{
	public class SubChunk : IDisposable, ICloneable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(SubChunk));

		public const ushort Size = 16 * 16 * 16;

		private bool _isAllAir = true;

		private List<PalettedContainer> _layers;
		private PalettedContainer _biomes;

		// Consider disabling these if we don't calculate lights
		private NibbleArray _blockLight;
		private NibbleArray _skyLight;

		private byte[] _cache;

		public int X { get; set; }
		public int Z { get; set; }
		public int Index { get; set; }

		internal List<PalettedContainer> Layers => _layers;
		internal virtual PalettedContainer Biomes => _biomes;

		public NibbleArray BlockLight => _blockLight;
		public NibbleArray SkyLight => _skyLight;

		public bool IsDirty { get; private set; }

		public ulong Hash { get; set; }
		public bool DisableCache { get; set; } = true;

		public SubChunk()
		{
			_layers = new List<PalettedContainer>
			{
				PalettedContainer.CreateFilledWith(new Air().RuntimeId, Size),
				PalettedContainer.CreateFilledWith(new Air().RuntimeId, Size) // think it is not required if there is no layer of water or snow in the chunk
			};

			_biomes = PalettedContainer.CreateFilledWith(1, Size); // plants biome

			//_blockLight = new NibbleArray(ArrayPool<byte>.Shared.Rent(2048));
			//_skyLight = new NibbleArray(ArrayPool<byte>.Shared.Rent(2048));
		}

		public SubChunk(int x, int z, int index, bool clearBuffers = true)
			: this()
		{
			X = x;
			Z = z;
			Index = index;

			if (clearBuffers) ClearBuffers();
		}

		public virtual void ClearBuffers()
		{
			//Array.Clear(_blockLight.Data, 0, 2048);
			//ChunkColumn.Fill<byte>(_skyLight.Data, 0xff);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAllAir()
		{
			var airRuntimeId = new Air().RuntimeId;

			return _layers.All(layer => layer.Palette.Count <= 1 && layer.Palette.SingleOrDefault(airRuntimeId) == airRuntimeId)
				&& _biomes.Palette.Count <= 1 && _biomes.Palette.SingleOrDefault(airRuntimeId) == 1; // plants biome
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static int GetIndex(int bx, int by, int bz)
		{
			return (bx << 8) | (bz << 4) | by;
		}

		public int GetBlockRuntimeId(int bx, int by, int bz, int layer = 0)
		{
			return _layers[layer][GetIndex(bx, by, bz)];
		}

		public Block GetBlockObject(int bx, int by, int bz, int layer = 0)
		{
			return BlockFactory.GetBlockByRuntimeId(GetBlockRuntimeId(bx, by, bz, layer));
		}

		public void SetBlock(int bx, int by, int bz, Block block, int layer = 0)
		{
			int runtimeId = block.RuntimeId;
			if (runtimeId < 0) return;

			SetBlockByRuntimeId(bx, by, bz, runtimeId, layer);
		}

		public void SetBlockByRuntimeId(int bx, int by, int bz, int runtimeId, int layer = 0)
		{
			_layers[layer][GetIndex(bx, by, bz)] = runtimeId;

			_cache = null;
			IsDirty = true;
		}

		public void SetBlockIndex(int bx, int by, int bz, ushort paletteIndex, int layer = 0)
		{
			_layers[layer].Data[GetIndex(bx, by, bz)] = paletteIndex;

			_cache = null;
			IsDirty = true;
		}

		public byte GetBiome(int bx, int by, int bz)
		{
			return (byte) _biomes[GetIndex(bx, by, bz)];
		}

		public void SetBiome(int bx, int by, int bz, byte biome)
		{
			_biomes[GetIndex(bx, by, bz)] = biome;
		}

		[Obsolete]
		public void SetLoggedBlock(int bx, int by, int bz, Block block)
		{
			SetBlock(bx, by, bz, block, 1);
		}

		[Obsolete]
		public void SetLoggedBlockByRuntimeId(int bx, int by, int bz, int runtimeId)
		{
			SetBlockByRuntimeId(bx, by, bz, runtimeId, 1);
		}

		public byte GetBlocklight(int bx, int by, int bz)
		{
			return 0;//_blockLight[GetIndex(bx, by, bz)];
		}

		public void SetBlocklight(int bx, int by, int bz, byte data)
		{
			//_blockLight[GetIndex(bx, by, bz)] = data;
		}

		public byte GetSkylight(int bx, int by, int bz)
		{
			return 0xff;//_skyLight[GetIndex(bx, by, bz)];
		}

		public void SetSkylight(int bx, int by, int bz, byte data)
		{
			//_skyLight[GetIndex(bx, by, bz)] = data;
		}

		public void Write(MemoryStream stream)
		{
			if (!DisableCache && !IsDirty && _cache != null)
			{
				stream.Write(_cache);
				return;
			}

			var startPos = stream.Position;

			WriteToStream(stream);

			int length = (int) (stream.Position - startPos);

			//if (DisableCache)
			{
				var bytes = new byte[length];
				stream.Position = startPos;
				int read = stream.Read(bytes, 0, length);
				if (read != length)
					throw new InvalidDataException($"Read wrong amount of data. Expected {length} but read {read}");
				if (startPos + length != stream.Position)
					throw new InvalidDataException($"Expected {startPos + length} but was {stream.Position}");

				_cache = bytes;
			}

			IsDirty = false;
		}

		public void WriteToStream(MemoryStream stream, bool network = true)
		{
			stream.WriteByte(8); // version

			stream.WriteByte((byte) _layers.Count);
			foreach (var layer in _layers)
			{
				layer.WriteToStream(stream, network);
			}
		}

		public virtual object Clone()
		{
			var cc = (SubChunk) Activator.CreateInstance(GetType());
			cc.X = X;
			cc.Z = Z;
			cc.Index = Index;

			cc._isAllAir = _isAllAir;
			cc.IsDirty = IsDirty;

			cc._layers = _layers.Select(layer => layer.Clone()).Cast<PalettedContainer>().ToList();
			cc._biomes = (PalettedContainer) _biomes.Clone();
			//_blockLight.Data.CopyTo(cc._blockLight.Data, 0);
			//_skyLight.Data.CopyTo(cc._skyLight.Data, 0);

			if (_cache != null)
			{
				cc._cache = (byte[]) _cache.Clone();
			}

			return cc;
		}

		public virtual void Dispose()
		{
			_layers.ForEach(layer => layer.Dispose());
			_biomes.Dispose();
			//if (_blockLight != null) ArrayPool<byte>.Shared.Return(_blockLight.Data);
			//if (_skyLight != null) ArrayPool<byte>.Shared.Return(_skyLight.Data);

			GC.SuppressFinalize(this);
		}
	}
}