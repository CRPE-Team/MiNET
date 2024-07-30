using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using MiNET.Net;

namespace MiNET.Utils.IO
{
	public interface ICompressor
	{
		public CompressionAlgorithm CompressionAlgorithm { get; }

		public void Write(MemoryStream stream, Memory<byte> input, bool writeLen = false, CompressionLevel compressionLevel = CompressionLevel.Fastest);
		public void Write(MemoryStream stream, List<Packet> packets, CompressionLevel compressionLevel = CompressionLevel.Fastest);
		public IEnumerable<Packet> ReadPackets(ReadOnlyMemory<byte> payload);
	}
}
