using OdooNet.Core.RES;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Core
{
	public interface IOdoo
	{
		public string Version { get; }

		public ICompany GetCompany(long id);
		public ICompany GetCompany(string name);
		public ICompany[] GetCompanies();
	}
}
