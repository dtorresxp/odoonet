using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Core.POS
{
	public interface ITerminal
	{
		public int Id { get; }
		public string Name { get; }
		public DateTime Created { get; }


		

		public IOrder[] GetOrders(DateTime createdAfter);
		public IOrder[] GetOrders(DateTime createdAfter, DateTime createdBefore);
	}
}
