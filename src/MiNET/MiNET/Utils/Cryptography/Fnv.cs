using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MiNET.Utils.Cryptography
{
	public class Fnv
	{
		private const uint FNV_OFFSET_BASIS = 0x811C9DC5;
		private const uint FNV_PRIME = 0x01000193;

		public static uint ComputeHash(ArraySegment<byte> data)
		{
			return ComputeHash(data.Array, data.Offset, data.Count);
		}

		public unsafe static uint ComputeHash(Span<byte> data, int length)
		{
			fixed (byte* ptr = &MemoryMarshal.GetReference(data))
			{
				return UnsafeComputeHash(ptr, length);
			}
		}

		public unsafe static uint ComputeHash(ReadOnlySpan<byte> data, int length)
		{
			fixed (byte* ptr = &MemoryMarshal.GetReference(data))
			{
				return UnsafeComputeHash(ptr, length);
			}
		}

		public unsafe static uint ComputeHash(byte[] data, int offset, int length)
		{
			fixed (byte* ptr = &data[offset])
			{
				return UnsafeComputeHash(ptr, length);
			}
		}

		public static unsafe uint ComputeHash(Stream stream, int bufferSize = 4096)
		{
			var data = ArrayPool<byte>.Shared.Rent(bufferSize);

			var hash = FNV_OFFSET_BASIS;
			try
			{
				int length;
				while ((length = stream.Read(data, 0, bufferSize)) > 0)
				{
					fixed (byte* ptr = &data[0])
					{
						CommonComputeHashProcess(ptr, length, ref hash);
					}
				}

				return hash;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(data);
			}
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint UnsafeComputeHash(byte* ptr, int length)
		{
			var hash = FNV_OFFSET_BASIS;

			CommonComputeHashProcess(ptr, length, ref hash);

			return hash;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe void CommonComputeHashProcess(byte* ptr, int length, ref uint hash)
		{
			while (length > 0)
			{
				hash ^= *ptr++;
				hash *= FNV_PRIME;

				length--;
			}
		}
	}
}
