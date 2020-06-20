using Newtonsoft.Json;
using OdooNet.Core.POS;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Product : Base, IProduct
	{
		public static string MODEL = "product.template";
		public static string[] FIELDS =
		{
			"id",
			"name",
			"create_date",
			"default_code",
			"description",
			"uom_name",
			"barcode",
			"standard_price",
			"list_price",
			"qty_available"
		};


		[JsonProperty("default_code")]
		public string Code { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("uom_name")]
		public string Unit { get; set; }

		[JsonProperty("barcode")]
		public string Barcode { get; set; }

		[JsonProperty("standard_price")]
		public decimal CostPrice { get; set; }

		//[JsonProperty("list_price")]
		public decimal BuyPrice { get; set; }

		[JsonProperty("list_price")]
		public decimal SellPrice { get; set; }

		[JsonProperty("qty_available")]
		public decimal Quantity { get; set; }
	}
}
