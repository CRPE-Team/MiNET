using System.Collections.Generic;
using log4net;
using MiNET.Net;
using Newtonsoft.Json;

namespace MiNET.Utils
{
	public class ItemStates : Dictionary<string, ItemState>, IPacketDataObject
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ItemStates));

		public static ItemStates FromJson(string json)
		{
			return JsonConvert.DeserializeObject<ItemStates>(json);
		}

		public void Write(Packet packet)
		{
			packet.WriteLength(Count);
			foreach (var itemstate in this)
			{
				packet.Write(itemstate.Key);
				itemstate.Value.Write(packet);
			}
		}

		public static ItemStates Read(Packet packet)
		{
			var result = new ItemStates();

			var count = packet.ReadLength();
			for (int runtimeId = 0; runtimeId < count; runtimeId++)
			{
				var name = packet.ReadString();
				var itemstate = ItemState.Read(packet);

				if (name == "minecraft:shield")
				{
					Log.Warn($"Got shield with runtime id {runtimeId}, legacy {itemstate.RuntimeId}");
				}

				result.Add(name, itemstate);
			}

			return result;
		}
	}

	public class ItemState : IPacketDataObject
	{
		[JsonProperty("runtime_id")]
		public short RuntimeId { get; set; }

		[JsonProperty("component_based")]
		public bool ComponentBased { get; set; } = false;

		public void Write(Packet packet)
		{
			packet.Write(RuntimeId);
			packet.Write(ComponentBased);
		}

		public static ItemState Read(Packet packet)
		{
			var legacyId = packet.ReadShort();
			var component = packet.ReadBool();

			return new ItemState()
			{
				RuntimeId = legacyId,
				ComponentBased = component
			};
		}
	}
}
