using System.Collections.Generic;
using Newtonsoft.Json;

namespace MiNET.Inventories
{
	public class ExternalDataGroup
	{
		[JsonProperty("group_icon", NullValueHandling = NullValueHandling.Include)]
		public ExternalDataItem IconItem { get; set; }

		[JsonProperty("group_name")]
		public string Name { get; set; }

		[JsonProperty("items")]
		public List<ExternalDataItem> Items { get; set; }
	}
}
