using System.Numerics;

namespace MiNET.Net
{
	public class PlayerAuthInputVehicleInfo : IPacketDataObject
	{
		public Vector2 Rotation { get; set; }

		public long ClientPredictedVehicleEntityUniqueId { get; set; }

		public void Write(Packet packet)
		{
			packet.Write(Rotation);
			packet.WriteEntityId(ClientPredictedVehicleEntityUniqueId);
		}

		public static PlayerAuthInputVehicleInfo Read(Packet packet)
		{
			return new PlayerAuthInputVehicleInfo()
			{
				Rotation = packet.ReadVector2(),
				ClientPredictedVehicleEntityUniqueId = packet.ReadEntityId()
			};
		}
	}
}
