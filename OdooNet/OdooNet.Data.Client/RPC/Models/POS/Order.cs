using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdooNet.Core.POS;
using OdooNet.Data.Client.RPC.Helpers.POS;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Order : Base, IOrder
	{
		public static string MODEL = "pos.order";



		[JsonProperty("sequence_number")]
		public string Number { get; set; }

		[JsonProperty("date_order")]
		public DateTime Date { get; set; }

		[JsonProperty("pos_reference")]
		public string Reference { get; set; }

		[JsonProperty("state")]
		public PosOrderState State { get; set; }


		[JsonProperty("location_id")]
		public JToken LocationId { get; set; }
		public (long Id, string Name) Location 
			=> (this.LocationId.SelectToken("[0]").Value<long>(), this.LocationId.SelectToken("[1]").Value<string>());


		[JsonProperty("user_id")]
		public JToken UserId { get; set; }
		public (long Id, string Name) User 
			=> (this.UserId.SelectToken("[0]").Value<long>(), this.UserId.SelectToken("[1]").Value<string>());


		[JsonProperty("partner_id")]
		public JToken PartnerId { get; set; }
		public (long Id, string Name) Partner 
			=> (this.PartnerId.SelectToken("[0]").Value<long>(), this.PartnerId.SelectToken("[1]").Value<string>());



		[JsonProperty("currency_id")]
		public JToken CurrencyId { get; set; }

		public (long Id, string Name, decimal Rate) Currency 
			=> (this.CurrencyId.SelectToken("[0]").Value<long>(), this.CurrencyId.SelectToken("[1]").Value<string>(), this.CurrencyRate);

		
		[JsonProperty("currency_rate")]
		public decimal CurrencyRate { get; set; }


		[JsonProperty("amount_tax")]
		public decimal TaxValue { get; set; }

		[JsonProperty("amount_total")]
		public decimal TotalValue { get; set; }

		[JsonProperty("amount_paid")]
		public decimal PaidValue { get; set; }

		[JsonProperty("amount_return")]
		public decimal ReturnedValue { get; set; }

		public IOrderLine[] GetLines() => this.OdooRpcClient.GetPosOrderLines(this.Id);
	}

	public class OrderLine : Base, IOrderLine
	{
		public static string MODEL = "pos.order.line";
		public static string[] FIELDS =
		{
			"id",
			"name",
			"create_date",
			"product_id",
			"product_uom_id",
			"qty",
			"price_unit",
			"discount",
			"price_subtotal",
			"price_subtotal_incl"
		};


		[JsonProperty("product_id")]
		public JToken ProductId { get; set; }

		[JsonProperty("x_product_code")]
		public string ProductCode { get; set; }

		public (long Id, string Name, string Code) Product 
			=> (this.ProductId.SelectToken("[0]").Value<long>(), this.ProductId.SelectToken("[1]").Value<string>(), this.ProductCode);


		[JsonProperty("product_uom_id")]
		public JToken UnitId { get; set; }

		public (long Id, string Name) Unit => (this.UnitId.SelectToken("[0]").Value<long>(), this.UnitId.SelectToken("[1]").Value<string>());

		[JsonProperty("qty")]
		public decimal Quantity { get; set; }

		[JsonProperty("price_unit")]
		public decimal Price { get; set; }

		[JsonProperty("discount")]
		public decimal DiscountRate { get; set; }

		[JsonProperty("price_subtotal")]
		public decimal Value { get; set; }

		public decimal TaxValue => this.TotalValue - this.Value;

		[JsonProperty("price_subtotal_incl")]
		public decimal TotalValue { get; set; }

	}

	
}
