using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using MiNET.Utils;

namespace MiNET.Worlds.IO
{
	public class PalettedContainer : IDisposable, ICloneable
	{

		private static readonly ILog Log = LogManager.GetLogger(typeof(PalettedContainer));

		private const ushort BlocksCount = 16 * 16 * 16; // chunk section size

		private Dictionary<int, ushort> _runtimeIdToPaletted;
		private List<int> _palette;
		private PalettedContainerData _data;

		public int this[int index]
		{
			get => GetBlockRuntimeId(index);
			set => SetBlock(index, value);
		}

		public PalettedContainer(int paletteSize)
		{
			_runtimeIdToPaletted = new Dictionary<int, ushort>(paletteSize);
			_palette = new List<int>(paletteSize);
			_data = new PalettedContainerData(paletteSize, BlocksCount);
		}

		internal PalettedContainer(List<int> palette, PalettedContainerData data)
		{
			_palette = palette;
			_data = data;

			UpdatePaletteMap();
		}

		public IReadOnlyList<int> Palette => _palette.AsReadOnly();

		internal PalettedContainerData Data => _data;

		public static PalettedContainer CreateFilledWith(int runtimeId)
		{
			var container = new PalettedContainer(1);
			container.AppendToPalette(runtimeId);

			return container;
		}

		public int GetBlockRuntimeId(int index)
		{
			var palettedId = _data[index];

			if (palettedId >= Palette.Count)
			{
				Log.Error($"Can't read biome index [{palettedId}] in ids [{string.Join(", ", Palette)}]");
				return 0;
			}

			return _palette[palettedId];
		}

		public void SetBlock(int index, int runtimeId)
		{
			_data[index] = GetPalettedId(runtimeId);
		}

		public void WriteToStream(MemoryStream stream, bool network = true)
		{
			if (!network)
			{
				// TODO - save palette as nbt data
				throw new NotImplementedException();
			}

			stream.WriteByte((byte) ((_data.BlockSize << 1) | Convert.ToByte(network))); // flags
			_data.WriteToStream(stream);

			VarInt.WriteSInt32(stream, _palette.Count); // count

			foreach (var id in _palette)
			{
				VarInt.WriteSInt32(stream, id);
			}
		}

		[Obsolete("Unwanted to use because of the possible significant load on the CPU")]
		internal void AppedPalette(int runtimeId)
		{
			AppendToPalette(runtimeId);
			_data.TryResize(_palette.Count);
		}

		public object Clone()
		{
			return new PalettedContainer(new List<int>(_palette), (PalettedContainerData) _data.Clone());
		}

		public void Dispose()
		{
			_data.Dispose();
		}

		internal ushort GetPalettedId(int runtimeId)
		{
			if (!_runtimeIdToPaletted.TryGetValue(runtimeId, out var palettedId))
			{
				palettedId = AppendToPalette(runtimeId);

				_data.TryResize(_palette.Count);
			}

			return palettedId;
		}

		private ushort AppendToPalette(int runtimeId)
		{
			var palettedId = (ushort) _palette.Count;

			_palette.Add(runtimeId);
			_runtimeIdToPaletted.Add(runtimeId, palettedId);

			return palettedId;
		}

		private void UpdatePaletteMap()
		{
			_runtimeIdToPaletted = _palette
				.Zip(Enumerable.Range(0, _palette.Count - 1))
				.ToDictionary(block => block.First, block => (ushort) block.Second);
		}
	}
}
