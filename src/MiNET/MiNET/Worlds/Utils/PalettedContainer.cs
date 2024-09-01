using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using MiNET.Utils;

namespace MiNET.Worlds.Utils
{
	public class PalettedContainer : IDisposable, ICloneable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(PalettedContainer));

		private static readonly Dictionary<ushort, PalettedContainerData> _sharedEmptyDatas = new Dictionary<ushort, PalettedContainerData>();

		private readonly Dictionary<int, ushort> _runtimeIdToPaletted;
		private readonly List<int> _palette;
		private PalettedContainerData _data;

		public int this[int index]
		{
			get => GetBlockRuntimeId(index);
			set => SetBlock(index, value);
		}

		public PalettedContainer(int paletteSize, ushort blocksCount)
		{
			_runtimeIdToPaletted = new Dictionary<int, ushort>(paletteSize);
			_palette = new List<int>(paletteSize);
			_data = new PalettedContainerData(paletteSize, blocksCount);
		}

		internal PalettedContainer(List<int> palette, PalettedContainerData data)
		{
			_palette = palette;
			_data = data;

			_runtimeIdToPaletted = _palette
				.Zip(Enumerable.Range(0, _palette.Count - 1))
				.DistinctBy(pair => pair.First)
				.ToDictionary(block => block.First, block => (ushort) block.Second);
		}

		public IReadOnlyList<int> Palette => _palette.AsReadOnly();

		internal PalettedContainerData Data => _data;

		public static PalettedContainer CreateFilledWith(int runtimeId, ushort blocksCount)
		{
			return new PalettedContainer(new List<int> { runtimeId }, GetEmptyData(blocksCount));
		}

		public void Clear()
		{
			lock (_palette)
			{
				_palette.Clear();
				_runtimeIdToPaletted.Clear();

				_data = GetEmptyData(_data.BlocksCount);
			}
		}

		public int GetBlockRuntimeId(int index)
		{
			lock (_palette)
			{
				var palettedId = _data[index];

				if (palettedId >= _palette.Count)
				{
					Log.Error($"Can't read block index [{palettedId}] in ids [{string.Join(", ", Palette)}]");
					return 0;
				}

				return _palette[palettedId];
			}
		}

		public void SetBlock(int index, int runtimeId)
		{
			lock (_palette)
			{
				//due to the fact that the instance of the _data field may change
				//in the process, we should to perform set index on another line!
				var palettedId = GetPalettedId(runtimeId);
				_data[index] = palettedId;
			}
		}

		public void WriteToStream(MemoryStream stream, bool network = true)
		{
			lock (_palette)
			{
				if (!network)
				{
					// TODO - save palette as nbt data
					throw new NotImplementedException();
				}

				stream.WriteByte((byte) ((_data.DataProfile.BlockSize << 1) | Convert.ToByte(network))); // flags
				_data.WriteToStream(stream);

				VarInt.WriteSInt32(stream, _palette.Count); // count

				foreach (var id in _palette)
				{
					VarInt.WriteSInt32(stream, id);
				}
			}
		}

		internal void AppendPaletteRange(IEnumerable<int> runtimeIds)
		{
			lock (_palette)
			{
				bool wasEmpty = _palette.Count <= 1;

				foreach (var runtimeId in runtimeIds)
				{
					AppendToPalette(runtimeId);
				}

				if (wasEmpty)
				{
					_data = new PalettedContainerData(_palette.Count, _data.BlocksCount);
				}
				else
				{
					_data.TryResize(_palette.Count);
				}
			}
		}

		internal ushort AppedPalette(int runtimeId)
		{
			lock (_palette)
			{
				bool wasEmpty = _palette.Count <= 1;

				var palettedId = AppendToPalette(runtimeId);

				if (wasEmpty && _palette.Count > 1)
				{
					_data = new PalettedContainerData(_palette.Count, _data.BlocksCount);
				}
				else
				{
					_data.TryResize(_palette.Count);
				}

				return palettedId;
			}
		}

		public object Clone()
		{
			lock (_palette)
			{
				return new PalettedContainer(new List<int>(_palette), (PalettedContainerData) _data.Clone());
			}
		}

		public void Dispose()
		{
			_data.Dispose();
		}

		internal ushort GetPalettedId(int runtimeId)
		{
			lock (_palette)
			{
				if (!_runtimeIdToPaletted.TryGetValue(runtimeId, out var palettedId))
				{
					palettedId = AppedPalette(runtimeId);
				}

				return palettedId;
			}
		}

		private static PalettedContainerData GetEmptyData(ushort blocksCount)
		{
			if (!_sharedEmptyDatas.TryGetValue(blocksCount, out var data))
			{
				_sharedEmptyDatas.Add(blocksCount, data = new PalettedContainerData(1, blocksCount));
			}

			return data;
		}

		private ushort AppendToPalette(int runtimeId)
		{
			var palettedId = (ushort) _palette.Count;

			_palette.Add(runtimeId);
			_runtimeIdToPaletted.TryAdd(runtimeId, palettedId);

			return palettedId;
		}
	}
}
