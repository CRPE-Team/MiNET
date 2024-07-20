using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using MiNET.Net;

namespace MiNET.Utils.IO
{
	public class CompressionManager
	{
		public static CompressionManager NoneCompressionManager { get; } = new CompressionManager();
		public static CompressionManager ZLibCompressionManager { get; } = new CompressionManager() { CompressionAlgorithm = CompressionAlgorithm.ZLib };

		public CompressionAlgorithm CompressionAlgorithm { get; set; } = CompressionAlgorithm.None;

		public short CompressionThreshold { get; set; } = 1000;

		public byte[] Compress(Memory<byte> input, bool writeLen = false, CompressionLevel compressionLevel = CompressionLevel.Fastest)
		{
			using (MemoryStream stream = MiNetServer.MemoryStreamManager.GetStream())
			{
				var compressor = GetCompressor(input.Length);

				if (CompressionAlgorithm != CompressionAlgorithm.None)
				{
					WriteCompressorAlgorithm(stream, compressor.CompressionAlgorithm);
				}

				compressor.Write(stream, input, writeLen, compressionLevel);

				return stream.ToArray();
			}
		}

		public byte[] CompressPacketsForWrapper(List<Packet> packets, CompressionLevel compressionLevel = CompressionLevel.Fastest)
		{
			long length = 0;
			foreach (Packet packet in packets)
			{
				length += packet.Encode().Length;
			}

			using (MemoryStream stream = MiNetServer.MemoryStreamManager.GetStream())
			{
				var compressor = GetCompressor(length);

				if (CompressionAlgorithm != CompressionAlgorithm.None)
				{
					WriteCompressorAlgorithm(stream, compressor.CompressionAlgorithm);
				}

				compressor.Write(stream, packets, compressionLevel);

				return stream.ToArray();
			}
		}

		private ICompressor GetCompressor(long length)
		{
			return length > CompressionThreshold 
				? GetCompressor(CompressionAlgorithm) 
				: GetCompressor(CompressionAlgorithm.None);
		}

		public IEnumerable<Packet> Decompress(ReadOnlyMemory<byte> payload)
		{
			if (CompressionAlgorithm == CompressionAlgorithm.None)
			{
				return GetCompressor(CompressionAlgorithm.None).ReadPackets(payload);
			}
			else
			{
				return GetCompressor((CompressionAlgorithm) payload.Span[0]).ReadPackets(payload.Slice(1));
			}
		}

		public static ICompressor GetCompressor(CompressionAlgorithm compressionAlgorithm)
		{
			return compressionAlgorithm switch
			{
				CompressionAlgorithm.ZLib => ZLibCompressor.Instance,
				CompressionAlgorithm.Snappy => throw new NotImplementedException(),

				_ => NoneCompressor.Instance
			};
		}

		private static void WriteCompressorAlgorithm(MemoryStream stream, CompressionAlgorithm compressionAlgorithm)
		{
			stream.WriteByte((byte) compressionAlgorithm);
		}
	}
}
