using System.Collections.Generic;
using System.Linq;
using fNbt;
using log4net;
using MiNET.Net;
using MiNET.Utils.Nbt;
using Newtonsoft.Json;

namespace MiNET.Items
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
		private static readonly NbtCompound EmptyNbt = new NbtCompound(string.Empty);

		private NbtCompound _nbt;

		[JsonProperty("runtime_id")]
		public short RuntimeId { get; set; }

		[JsonProperty("component_based")]
		public bool ComponentBased { get; set; } = false;

		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("component_nbt")]
		public byte[] NbtData { get; set; }

		[JsonIgnore]
		public NbtCompound Nbt 
		{ 
			get 
			{
				if (_nbt == null)
				{
					_nbt = NbtData == null ? EmptyNbt : NbtExtensions.ReadNbtCompound(NbtData);
				}

				return _nbt;
			}
			set
			{
				_nbt = value;
				NbtData = _nbt.Any() ? NbtExtensions.ToBytes(_nbt, NbtFlavor.Bedrock) : null;
			}
		}

		public void Write(Packet packet)
		{
			packet.Write(RuntimeId);
			packet.Write(ComponentBased);
			packet.WriteSignedVarInt(Version);
			packet.Write(Nbt);
		}

		public static ItemState Read(Packet packet)
		{
			return new ItemState()
			{
				RuntimeId = packet.ReadShort(),
				ComponentBased = packet.ReadBool(),
				Version = packet.ReadSignedVarInt(),
				Nbt = packet.ReadNbtCompound()
			};
		}
	}
}
