﻿using OdooNet.Core.POS;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Core.RES
{
	public interface ICompany
	{
		public int Id { get; }
		public string Name { get; }
		public string Address { get; }
		public string Phone { get; }
		public string Email { get; }
		public string Website { get; }

		public IOrder[] GetOrders(DateTime createdAfter);
		public IOrder[] GetOrders(DateTime createdAfter, DateTime createdBefore);
	}
}