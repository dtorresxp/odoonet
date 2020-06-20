using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OdooNet.Core.POS;
using OdooNet.Data.Client.RPC.Helpers.POS;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Session : Base, ISession
	{
		public static string MODEL = "pos.session";

		[JsonProperty("user_id")]
		public JToken UserId { get; set; }

		public (long Id, string Name) User => (this.UserId.SelectToken("0").Value<long>(), this.UserId.SelectToken("1").Value<string>());

		[JsonProperty("order_count")]
		public int OrderCount { get; set; }

		[JsonProperty("total_payments_amount")]
		public decimal TotalPayments { get; set; }

		[JsonProperty("state")]
		public SessionState State { get; set; }

		[JsonProperty("start_at")]
		public DateTime Opened { get; set; }

		[JsonProperty("stop_at")]
		public JToken StopAt { get; set; }

		public DateTime? Closed => this.State == SessionState.CLOSED ? this.StopAt.Value<DateTime?>() : null;

		public IOrder[] GetPosOrders() => this.OdooRpcClient.GetPosOrders(sessionId: this.Id);
	}
}
