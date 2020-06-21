using Newtonsoft.Json;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models
{
	public abstract class Base
	{
		[JsonIgnore]
		public OdooRpcClient OdooRpcClient { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("create_date")]
		public DateTime Created { get; set; }

		[JsonProperty("write_date")]
		public DateTime Updated { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("display_name")]
		public string DisplayName { get; set; }
	}
}
