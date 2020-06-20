using Newtonsoft.Json.Linq;
using OdooNet.Core.POS;
using OdooNet.Data.Client.RPC.Models.POS;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models;
using OdooRpc.CoreCLR.Client.Models.Parameters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdooNet.Data.Client.RPC.Helpers.POS
{
	public static class RpcHelperProducts
	{
		public static IProduct[] GetProducts(this OdooRpcClient odooRpcClient, long? companyId = null)
		{
			OdooDomainFilter filter = new OdooDomainFilter();

			if (companyId.HasValue)
			{
				filter.Filter("company_id", "=", companyId.Value);
			}

			Task<JObject[]> task1 = odooRpcClient.Get<JObject[]>(new OdooSearchParameters(Product.MODEL));

			task1.Wait();

			Task<Product[]> task = odooRpcClient.Get<Product[]>(
					new OdooSearchParameters(
						Product.MODEL,
						filter
					)
				);

			task.Wait();

			List<Product> products = task.Result.ToList();
			products.ForEach(product => product.OdooRpcClient = odooRpcClient);

			return products.ToArray();
		}
	}
}
