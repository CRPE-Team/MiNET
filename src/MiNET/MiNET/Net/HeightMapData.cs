using System;
using System.Linq;

namespace MiNET.Net;

public class HeightMapData : IPacketDataObject
{
	public short[] Heights { get; }

	public bool IsAllTooLow => Heights.Any(x => x > 0);

	public bool IsAllTooHigh => Heights.Any(x => x <= 15);

	public HeightMapData(short[] heights)
	{
		if (heights.Length != 256)
			throw new ArgumentException("Expected 256 data entries");

		Heights = heights;
	}

	public int GetHeight(int x, int z)
	{
		return Heights[((z & 0xf) << 4) | (x & 0xf)];
	}

	public void Write(Packet packet)
	{
		if (IsAllTooHigh)
		{
			packet.Write((byte) SubChunkPacketHeightMapType.AllTooHigh);
			return;
		}

		if (IsAllTooLow)
		{
			packet.Write((byte) SubChunkPacketHeightMapType.AllTooLow);
			return;
		}

		packet.Write((byte) SubChunkPacketHeightMapType.Data);

		for (int i = 0; i < Heights.Length; i++)
		{
			packet.Write((byte) Heights[i]);
		}
	}

	public static HeightMapData Read(Packet packet)
	{
		var type = (SubChunkPacketHeightMapType) packet.ReadByte();

		if (type != SubChunkPacketHeightMapType.Data) return null;

		var heights = new short[256];

		for (int i = 0; i < heights.Length; i++)
		{
			heights[i] = (short) packet.ReadByte();
		}

		return new HeightMapData(heights);
	}
}

public enum SubChunkPacketHeightMapType : byte
{
	NoData = 0,
	Data = 1,
	AllTooHigh = 2,
	AllTooLow = 3
}

public enum SubChunkRequestResult : byte
{
	Success = 1,
	NoSuchChunk = 2,
	WrongDimension = 3,
	NullPlayer = 4, 
	YIndexOutOfBounds = 5,
	SuccessAllAir = 6
}