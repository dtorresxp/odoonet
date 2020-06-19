using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OdooNet.Data.Client.RPC.Helpers.POS;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Session : Base
	{
		public static string MODEL = "pos.session";
		public static string[] FIELDS = new string[]
		{
			"id",
			"name",
			"user_id",
			"order_count",
			"total_payments_amount",
			"state",
			"start_at",
			"stop_at",
			"order_ids"
		};

		[JsonProperty("user_id")]
		public JToken UserId { get; set; }

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

		[JsonProperty("order_ids")]
		public JToken OrderIds { get; set; }



		public Order[] GetPosOrders() => this.OdooRpcClient.GetPosOrders(sessionId: this.Id);
	}

	public enum SessionState
	{
		[EnumMember(Value = "new_session")]
		NEW,

		[EnumMember(Value = "opening_control")]
		OPENING,

		[EnumMember(Value = "opened")]
		OPENED,

		[EnumMember(Value = "closing_control")]
		CLOSING,

		[EnumMember(Value = "closed")]
		CLOSED
	}
}
