using MiNET.Entities;
using MiNET.Utils.Vectors;

namespace MiNET.Net;

public class UpdateSubChunkBlocksPacketEntry : IPacketDataObject
{
	public BlockCoordinates Coordinates { get; set; }

	public uint BlockRuntimeId { get; set; }

	public uint Flags { get; set; }

	public long SyncedUpdatedEntityUniqueId { get; set; }

	public uint SyncedUpdateType { get; set; }

	public void Write(Packet packet)
	{
		packet.Write(Coordinates);
		packet.WriteUnsignedVarInt(BlockRuntimeId);
		packet.WriteUnsignedVarInt(Flags);
		packet.WriteUnsignedVarLong(SyncedUpdatedEntityUniqueId);
		packet.WriteUnsignedVarInt(SyncedUpdateType);
	}

	public static UpdateSubChunkBlocksPacketEntry Read(Packet packet)
	{
		return new UpdateSubChunkBlocksPacketEntry()
		{
			Coordinates = packet.ReadBlockCoordinates(),
			BlockRuntimeId = packet.ReadUnsignedVarInt(),
			Flags = packet.ReadUnsignedVarInt(),
			SyncedUpdatedEntityUniqueId = packet.ReadUnsignedVarLong(),
			SyncedUpdateType = packet.ReadUnsignedVarInt()
		};
	}
}