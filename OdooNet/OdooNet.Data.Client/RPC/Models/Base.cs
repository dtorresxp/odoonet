using Newtonsoft.Json;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models
{
	public abstract class Base
	{
		public static string[] FIELDS = {"id", "name", "create_date"};

		[JsonIgnore]
		public OdooRpcClient OdooRpcClient { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("create_date")]
		public DateTime Created { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
