using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OdooNet.Core.POS
{
	public interface IOrder
	{
		public DateTime Created { get; }
		public DateTime Date { get; }
		public string Reference { get; }
		public PosOrderState State { get; }
		public (long id, string name) Location { get; }
		public (long id, string name) User { get; }
		public (long id, string name) Partner { get; }
		public (long id, string name) Currency { get; }
		public decimal CurrencyRate { get; }
		public decimal TaxValue { get; }
		public decimal TotalValue { get; }
		public decimal PaidValue { get; }
		public decimal ReturnedValue { get; }

		public IOrderLine[] GetLines();


	}

	public interface IOrderLine
	{
		public (long id, string name) Product { get; }
		public (long id, string name) Unit { get; }
		public decimal Quantity { get; }
		public decimal Price { get; }
		public decimal DiscountRate { get; }
		public decimal Value { get; set; }
		public decimal TaxValue { get; }
		public decimal TotalValue { get; }
	}

	/**
	 * Order states
	 */
	public enum PosOrderState
	{
		[EnumMember(Value = "draft")]
		DRAFT,

		[EnumMember(Value = "cancel")]
		CANCELED,

		[EnumMember(Value = "paid")]
		PAID,

		[EnumMember(Value = "posted")]
		DONE,

		[EnumMember(Value = "invoiced")]
		INVOICED
	}

	
}
