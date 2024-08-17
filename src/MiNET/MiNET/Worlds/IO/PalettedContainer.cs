using System;
using System.Collections.Generic;
using MiNET.Blocks;

namespace MiNET.Worlds.IO
{
	public class PalettedContainer : IDisposable
	{
		private static readonly uint AirRuntimeId = (uint) new Air().RuntimeId;

		private readonly Dictionary<uint, ushort> _runtimeIdToPaletted;
		private readonly List<uint> _palette;
		private readonly PalettedContainerData _data;

		public PalettedContainer()
			: this(1)
		{

		}

		public PalettedContainer(int paletteSize)
		{
			_runtimeIdToPaletted = new Dictionary<uint, ushort>(paletteSize);
			_palette = new List<uint>(paletteSize);
			_data = new PalettedContainerData((uint) paletteSize);
		}

		public uint GetBlockRuntimeId(ushort index)
		{
			return _palette[_data[index]];
		}

		public void SetBlock(ushort index, uint runtimeId)
		{
			_data[index] = GetPalettedId(runtimeId);
		}

		//private void AppendPalette

		public void Dispose()
		{
			_data.Dispose();
		}

		private ushort GetPalettedId(uint runtimeId)
		{
			if (!_runtimeIdToPaletted.TryGetValue(runtimeId, out var palettedId))
			{
				palettedId = (ushort) _palette.Count;

				_palette.Add(runtimeId);
				_runtimeIdToPaletted.Add(runtimeId, palettedId);
				_data.TryResize((uint) _palette.Count);
			}

			return palettedId;
		}
	}
}
