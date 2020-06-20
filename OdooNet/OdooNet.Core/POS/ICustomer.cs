using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Core.POS
{
	public interface ICustomer
	{
		public int Id { get; }
		public string Name { get; }
		public DateTime Created { get; }
	}
}
