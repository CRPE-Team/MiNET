using System;
using System.Collections.Generic;
using System.IO.Compression;
using MiNET.Net;

namespace MiNET.Utils.IO
{
	public interface ICompressor
	{
		public CompressionAlgorithm CompressionAlgorithm { get; }
		public short CompressionThreshold { get; }

		public byte[] Compress(Memory<byte> input, bool writeLen = false, CompressionLevel compressionLevel = CompressionLevel.Fastest);
		public byte[] CompressPacketsForWrapper(List<Packet> packets, CompressionLevel compressionLevel = CompressionLevel.Fastest);
		public IEnumerable<Packet> Decompress(ReadOnlyMemory<byte> payload);
	}
}
