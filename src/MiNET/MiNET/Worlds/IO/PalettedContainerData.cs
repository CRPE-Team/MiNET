using System;
using System.Buffers;

namespace MiNET.Worlds.IO
{
	public class PalettedContainerData : IDisposable
	{
		private const ushort BlocksCount = 16 * 16 * 16; // chunk section size

		private Profile _profile;

		private int[] _data;

		public ushort this[ushort index]
		{
			get => Get(index);
			set => Set(index, value);
		}

		public PalettedContainerData(uint initPaletteSize)
		{
			_profile = Profile.ByPaletteSize(initPaletteSize);
			_data = InitData(GetDataSize(_profile));
		}

		public byte BlocksPerWord => _profile.BlocksPerWord;
		public byte BlockSize => _profile.BlockSize;

		public int[] Data => _data;

		public void TryResize(uint paletteSize)
		{
			if (paletteSize <= _profile.MaxPaletteSize) return;

			var nextProfile = Profile.ByPaletteSize(paletteSize);
			var newData = InitData(GetDataSize(nextProfile));
			CopyTo(newData, nextProfile);

			//var oldData = _data;
			_profile = nextProfile;
			_data = newData;

			//ArrayPool<int>.Shared.Return(oldData);
		}

		public void Dispose()
		{
			//if (_data != null) ArrayPool<int>.Shared.Return(_data);
		}

		private void CopyTo(int[] data, Profile profile)
		{
			var lenght = _data.Length;
			var blockSize = _profile.BlockSize;
			var blocksPerWord = _profile.BlocksPerWord;
			var wordBlocksSize = blocksPerWord * blockSize;
			var wordBlockMask = _profile.WordBlockMask;
			var blocksCount = BlocksCount;

			var hasSubWord = blockSize == 3 || blockSize == 5 || blockSize == 6;
			if (hasSubWord)
			{
				lenght -= 1;
			}

			var newBlockSize = profile.BlockSize;
			var newWordBlocksSize = profile.BlocksPerWord * profile.BlockSize;
			var newDataIndex = 0;
			var newWordShift = 0;
			ref var newWord = ref data[newDataIndex++];
			for (var i = 0; i < lenght; i++)
			{
				var word = _data[i];

				for (var wordShift = 0; wordShift < wordBlocksSize; wordShift += blockSize)
				{
					if (newWordShift == newWordBlocksSize)
					{
						newWordShift = 0;
						newWord = ref data[newDataIndex++];
					}

					newWord |= (word >> wordShift & wordBlockMask) << newWordShift;

					newWordShift += newBlockSize;
				}
			}

			if (hasSubWord)
			{
				var subWordBlocksSize = (blocksCount - blocksPerWord * lenght) * blockSize;
				var word = _data[lenght];

				for (var wordShift = 0; wordShift < subWordBlocksSize; wordShift += blockSize)
				{
					newWord |= (word >> wordShift & wordBlockMask) << newWordShift;

					newWordShift += newBlockSize;
				}
			}
		}

		private ushort Get(ushort index)
		{
			var blocksPerWord = _profile.BlocksPerWord;
			var shift = _profile.BlockSize * (index % blocksPerWord);
			return (ushort) (_data[index / blocksPerWord] >> shift & _profile.WordBlockMask);
		}

		private void Set(ushort index, ushort value)
		{
			var blocksPerWord = _profile.BlocksPerWord;

			ref var word = ref _data[index / blocksPerWord];

			var shift = (index % blocksPerWord) * _profile.BlockSize;
			var mask = _profile.WordBlockMask << shift;
			word &= mask ^ -1;
			word |= (value << shift) & mask;
		}

		private int GetDataSize(Profile profile)
		{
			return (int) Math.Ceiling((float) BlocksCount / profile.BlocksPerWord);
		}

		private static int[] InitData(int dataSize)
		{
			//return ArrayPool<int>.Shared.Rent(dataSize);
			return new int[dataSize];
		}

		public class Profile
		{
			private const byte WordSize = sizeof(int) * 8;

			public static readonly Profile P16 = new Profile(16);
			public static readonly Profile P8 = new Profile(8);
			public static readonly Profile P6 = new Profile(6);
			public static readonly Profile P5 = new Profile(5);
			public static readonly Profile P4 = new Profile(4);
			public static readonly Profile P3 = new Profile(3);
			public static readonly Profile P2 = new Profile(2);
			public static readonly Profile P1 = new Profile(1);

			public byte BlocksPerWord { get; }
			public byte BlockSize { get; }
			public int MaxPaletteSize { get; }

			public int WordBlockMask { get; }

			public Profile Next { get; }

			private Profile(byte blockSize)
			{
				BlockSize = blockSize;
				BlocksPerWord = (byte) (WordSize / blockSize);
				MaxPaletteSize = (int) Math.Pow(2, blockSize);
				WordBlockMask = MaxPaletteSize - 1;
			}

			public static Profile ByPaletteSize(uint paletteSize)
			{
				var profileId = (byte) Math.Ceiling(Math.Log(paletteSize, 2));

				return profileId switch
				{
					1 => P1,
					2 => P2,
					3 => P3,
					4 => P4,
					5 => P5,
					6 => P6,
					7 or 8 => P8,
					_ => P16,
				};
			}
		}
	}
}
