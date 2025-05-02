using System.ComponentModel;
using Newtonsoft.Json;

namespace MiNET.Inventories
{
	public class ExternalDataItem
	{
		[JsonProperty("name")]
		public string Id { get; set; }

		[JsonProperty("meta")]
		public short Metadata { get; set; }

		[JsonProperty("block_states")]
		public byte[] BlockStates { get; set; }

		[JsonProperty("nbt")]
		public byte[] ExtraData { get; set; }

		[JsonProperty("tag")]
		public string Tag { get; set; }

		[DefaultValue(1)]
		[JsonProperty("count", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public int Count { get; set; }
	}
}
