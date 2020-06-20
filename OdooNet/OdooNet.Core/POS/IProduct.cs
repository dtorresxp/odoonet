using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Core.POS
{
	public interface IProduct
	{
		public int Id { get; }
		public string Name { get; }
		public DateTime Created { get; }
		public string Code { get; }
		public string Description { get; }
		public string Unit { get; }
		public string Barcode { get; set; }
		public decimal CostPrice { get; set; }
		public decimal BuyPrice { get; set; }
		public decimal SellPrice { get; set; }
		public decimal Quantity { get; set; }

	}
}
