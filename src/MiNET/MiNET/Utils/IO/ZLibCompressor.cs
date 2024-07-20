using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using log4net;
using MiNET.Net;

namespace MiNET.Utils.IO
{
	public class ZLibCompressor : ICompressor
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ZLibCompressor));

		public static ICompressor Instance { get; } = new ZLibCompressor();

		public CompressionAlgorithm CompressionAlgorithm => CompressionAlgorithm.ZLib;

		public void Write(MemoryStream stream, Memory<byte> input, bool writeLen = false, CompressionLevel compressionLevel = CompressionLevel.Fastest)
		{
			using (var compressStream = new DeflateStream(stream, compressionLevel, true))
			{
				if (writeLen)
				{
					WriteLength(compressStream, input.Length);
				}

				compressStream.Write(input.Span);
			}
		}

		public void Write(MemoryStream stream, List<Packet> packets, CompressionLevel compressionLevel = CompressionLevel.Fastest)
		{
			using (var compressStream = new DeflateStream(stream, compressionLevel, true))
			{
				foreach (Packet packet in packets)
				{
					byte[] bs = packet.Encode();
					if (bs != null && bs.Length > 0)
					{
						BatchUtils.WriteLength(compressStream, bs.Length);
						compressStream.Write(bs, 0, bs.Length);
					}
					packet.PutPool();
				}
			}
		}

		public IEnumerable<Packet> ReadPackets(ReadOnlyMemory<byte> payload)
		{
			var packets = new List<Packet>();

			using (var stream = new MemoryStreamReader(payload))
			{
				using (var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, false))
				{
					using var s = new MemoryStream();
					deflateStream.CopyTo(s);
					s.Position = 0;

					int count = 0;
					// Get actual packet out of bytes
					while (s.Position < s.Length)
					{
						count++;

						uint len = VarInt.ReadUInt32(s);
						long pos = s.Position;
						ReadOnlyMemory<byte> internalBuffer = s.GetBuffer().AsMemory((int) s.Position, (int) len);
						int id = VarInt.ReadInt32(s);
						try
						{
							//if (Log.IsDebugEnabled)
							//	Log.Debug($"0x{internalBuffer[0]:x2}\n{Packet.HexDump(internalBuffer)}");

							packets.Add(PacketFactory.Create(id, internalBuffer, "mcpe") ??
										new UnknownPacket(id, internalBuffer));
						}
						catch (Exception e)
						{
							if (Log.IsDebugEnabled) Log.Warn($"Error parsing bedrock message #{count} id={id}\n{Packet.HexDump(internalBuffer)}", e);
							//throw;
							return packets; // Exit, but don't crash.
						}

						s.Position = pos + len;
					}

					if (s.Length > s.Position)
					{
						throw new Exception("Have more data");
					}
				}
			}

			return packets;
		}

		private static void WriteLength(Stream stream, int lenght)
		{
			VarInt.WriteUInt32(stream, (uint) lenght);
		}
	}
}
