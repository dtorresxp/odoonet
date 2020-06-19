using OdooNet.Core;
using OdooNet.Data.Client;
using System;
using System.Linq;

namespace OdooNet.Apps.Console.Demo
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			IOdoo odoo = RPCClient.Connect(
				"www.redis-stock.com", 
				8069, 
				false, 
				"bitnami_odoo", 
				"xmlrpc@redis-stock.com", 
				"xmlrpc@redis-stock.com"
			);

			System.Console.WriteLine("Odoo version: " + odoo.Version);
		}
	}
}
