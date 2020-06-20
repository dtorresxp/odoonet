using Newtonsoft.Json.Linq;
using OdooNet.Data.Client.RPC.Models;
using OdooNet.Data.Client.RPC.Models.POS;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models;
using OdooRpc.CoreCLR.Client.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdooNet.Data.Client.RPC.Helpers.POS
{
	public static class RpcHelperOrders
	{
		public static Order[] GetPosOrders(this OdooRpcClient odooRpcClient, long? companyId = null, long? terminalId = null, long? sessionId = null, DateTime? createdAfter = null, DateTime? createdBefore = null)
		{
			OdooDomainFilter filter = new OdooDomainFilter();

			if (companyId.HasValue)
				filter = filter.Filter("company_id", "=", companyId.Value);
			if (terminalId.HasValue)
				filter = filter.Filter("config_id", "=", terminalId.Value);
			if (sessionId.HasValue)
				filter = filter.Filter("session_id", "=", sessionId.Value);
			if (createdAfter.HasValue)
				filter = filter.Filter("create_date", ">", createdAfter.Value);
			if (createdBefore.HasValue)
				filter = filter.Filter("create_date", "<", createdBefore.Value);

			Task<JObject[]> task1 = odooRpcClient.Get<JObject[]>(new OdooSearchParameters(model: Order.MODEL, domainFilter: filter));

			task1.Wait();

			Task<Order[]> task = odooRpcClient.Get<Order[]>(new OdooSearchParameters(model: Order.MODEL, domainFilter: filter));

			task.Wait();

			List<Order> posOrders = task.Result.ToList();
			posOrders.ForEach(posOrders => posOrders.OdooRpcClient = odooRpcClient);


			return posOrders.ToArray();
		}

		public static OrderLine[] GetPosOrderLines(this OdooRpcClient odooRpcClient, long orderId)
		{
			
			OdooDomainFilter filter = new OdooDomainFilter().Filter("order_id", "=", orderId);

			Task<JObject[]> task1 = odooRpcClient.Get<JObject[]>(new OdooSearchParameters(OrderLine.MODEL, filter));

			task1.Wait();

			Task<OrderLine[]> task = odooRpcClient.Get<OrderLine[]>(new OdooSearchParameters(OrderLine.MODEL,filter));

			task.Wait();

			List<OrderLine> posOrderLines = task.Result.ToList();

			posOrderLines.ForEach(posOrderLine =>
			{
				posOrderLine.OdooRpcClient = odooRpcClient;
			});

			return posOrderLines.ToArray();
		}
	}
}
