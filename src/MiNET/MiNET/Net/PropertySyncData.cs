using System.Collections.Generic;

namespace MiNET.Net
{
	public class PropertySyncData : IPacketDataObject
	{
		public Dictionary<uint, int> IntProperties = new Dictionary<uint, int>();
		public Dictionary<uint, float> FloatProperties = new Dictionary<uint, float>();

		public void Write(Packet packet)
		{
			packet.WriteLength(IntProperties.Count);

			foreach (var intP in IntProperties)
			{
				packet.WriteUnsignedVarInt(intP.Key);
				packet.WriteSignedVarInt(intP.Value);
			}

			packet.WriteLength(FloatProperties.Count);

			foreach (var intF in FloatProperties)
			{
				packet.WriteUnsignedVarInt(intF.Key);
				packet.Write(intF.Value);
			}
		}

		public static PropertySyncData Read(Packet packet)
		{
			var syncData = new PropertySyncData();
			var countInt = packet.ReadLength();
			for (int i = 0; i < countInt; i++)
			{
				syncData.IntProperties.Add(packet.ReadUnsignedVarInt(), packet.ReadVarInt());
			}

			var countFloat = packet.ReadLength();
			for (int i = 0; i < countFloat; i++)
			{
				syncData.FloatProperties.Add(packet.ReadUnsignedVarInt(), packet.ReadFloat());
			}

			return syncData;
		}
	}
}
