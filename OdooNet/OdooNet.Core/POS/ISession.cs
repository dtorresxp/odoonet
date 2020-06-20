using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OdooNet.Core.POS
{
	public interface ISession
	{
		public (long Id, string Name) User { get; }
		public int OrderCount { get; }
		public decimal TotalPayments { get; }
		public SessionState State { get; }
		public DateTime Opened { get; }
		public DateTime? Closed { get; }

		public IOrder[] GetPosOrders();
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
