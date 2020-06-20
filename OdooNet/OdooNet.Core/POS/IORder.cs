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
		public (long Id, string Name) Location { get; }
		public (long Id, string Name) User { get; }
		public (long Id, string Name) Partner { get; }
		public (long Id, string Name, decimal Rate) Currency { get; }
		public decimal TaxValue { get; }
		public decimal TotalValue { get; }
		public decimal PaidValue { get; }
		public decimal ReturnedValue { get; }

		public IOrderLine[] GetLines();


	}

	public interface IOrderLine
	{
		public (long Id, string Name, string Code) Product { get; }
		public (long Id, string Name) Unit { get; }
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
