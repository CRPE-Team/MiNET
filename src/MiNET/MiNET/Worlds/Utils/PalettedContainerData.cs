using System;
using System.IO;

namespace MiNET.Worlds.Utils
{
	public class PalettedContainerData : IDisposable, ICloneable
	{
		private Profile _profile;

		private ushort _blocksCount;
		private int[] _data;

		public ushort this[int index]
		{
			get => Get(index);
			set => Set(index, value);
		}

		public PalettedContainerData(int[] data, int paletteSize, ushort blocksCount)
			: this(data, Profile.ByPaletteSize(paletteSize), blocksCount)
		{

		}

		public PalettedContainerData(int initPaletteSize, ushort blocksCount)
		{
			_profile = Profile.ByPaletteSize(initPaletteSize);
			_blocksCount = blocksCount;
			_data = InitData(GetDataSize(_profile));
		}

		internal PalettedContainerData(int[] data, Profile profile, ushort blocksCount)
		{
			_profile = profile;
			_data = data;
			_blocksCount = blocksCount;
		}

		public Profile DataProfile => _profile;
		public ushort BlocksCount => _blocksCount;

		public int[] Data => _data;

		public void TryResize(int paletteSize)
		{
			if (paletteSize <= _profile.MaxPaletteSize)
				return;

			var nextProfile = Profile.ByPaletteSize(paletteSize);
			var newData = InitData(GetDataSize(nextProfile));
			CopyTo(newData, nextProfile);

			//var oldData = _data;
			_profile = nextProfile;
			_data = newData;

			//ArrayPool<int>.Shared.Return(oldData);
		}

		public unsafe void WriteToStream(MemoryStream stream)
		{
			fixed (int* buffer = _data)
			{
				stream.Write(new ReadOnlySpan<byte>(buffer, _data.Length * sizeof(int)));
			}
		}

		public object Clone()
		{
			var length = _data.Length;

			var data = new int[length];
			Array.Copy(_data, data, length);

			return new PalettedContainerData(data, _profile, _blocksCount);
		}

		public void Dispose()
		{
			//_data = null;
			//if (_data != null) ArrayPool<int>.Shared.Return(_data);
		}

		private void CopyTo(int[] data, Profile profile)
		{
			var lenght = _data.Length;
			var blockSize = _profile.BlockSize;
			var blocksPerWord = _profile.BlocksPerWord;
			var wordBlocksSize = blocksPerWord * blockSize;
			var wordBlockMask = _profile.WordBlockMask;
			var blocksCount = _blocksCount;

			var newBlockSize = profile.BlockSize;
			var newWordBlocksSize = profile.BlocksPerWord * profile.BlockSize;
			var newDataIndex = 0;
			var newWordShift = 0;

			var hasSubWord = blockSize == 3 || blockSize == 5 || blockSize == 6;
			if (hasSubWord)
			{
				lenght -= 1;
			}

			ref var newWord = ref data[newDataIndex++];
			for (var i = 0; i != lenght; i++)
			{
				var word = _data[i];

				for (var wordShift = 0; wordShift != wordBlocksSize; wordShift += blockSize)
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

				for (var wordShift = 0; wordShift != subWordBlocksSize; wordShift += blockSize)
				{
					newWord |= (word >> wordShift & wordBlockMask) << newWordShift;

					newWordShift += newBlockSize;
				}
			}
		}

		private ushort Get(int index)
		{
			var blocksPerWord = _profile.BlocksPerWord;
			var shift = _profile.BlockSize * (index % blocksPerWord);
			return (ushort) (_data[index / blocksPerWord] >> shift & _profile.WordBlockMask);
		}

		private void Set(int index, ushort value)
		{
			var blocksPerWord = _profile.BlocksPerWord;

			ref var word = ref _data[index / blocksPerWord];

			var shift = index % blocksPerWord * _profile.BlockSize;
			var mask = _profile.WordBlockMask << shift;
			word &= ~mask;
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

			public static Profile ByPaletteSize(int paletteSize)
			{
				return ByBlockSize((byte) Math.Ceiling(Math.Log(paletteSize, 2)));
			}

			public static Profile ByBlockSize(byte blockSize)
			{
				return blockSize switch
				{
					0 or 1 => P1,
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
