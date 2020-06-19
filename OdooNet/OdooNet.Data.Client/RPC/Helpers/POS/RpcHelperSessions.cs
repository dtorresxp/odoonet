using Newtonsoft.Json.Linq;
using OdooNet.Data.Client.RPC.Models.POS;
using OdooNet.Data.Client.RPC.Models.RES;
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
	public static class RpcHelperSessions
	{
		public static Session[] GetSessions(this OdooRpcClient odooRpcClient, long terminalId, DateTime? createdAfter = null, DateTime? createdBefore = null, DateTime? openedAfter = null, DateTime? openedBefore = null, DateTime? closedAfter = null, DateTime? closedBefore = null)
		{
			OdooDomainFilter filter = new OdooDomainFilter().Filter("config_id", "=", terminalId);

			if (createdAfter.HasValue)
				filter = filter.Filter("create_date", ">", createdAfter.Value);
			if (createdBefore.HasValue)
				filter = filter.Filter("create_date", "<", createdBefore.Value);
			if (openedAfter.HasValue)
				filter = filter.Filter("start_at", ">", openedAfter.Value);
			if (openedBefore.HasValue)
				filter = filter.Filter("start_at", "<", openedBefore.Value);
			if (closedAfter.HasValue)
				filter = filter.Filter("stop_at", ">", closedAfter.Value);
			if (closedBefore.HasValue)
				filter = filter.Filter("stop_at", "<", closedBefore.Value);


			Task<Session[]> task = odooRpcClient.Get<Session[]>(
					new OdooSearchParameters(Session.MODEL, filter),
					new OdooFieldParameters(Session.FIELDS)
				);

			task.Wait();

			List<Session> sessions = task.Result.ToList();
			sessions.ForEach(session => session.OdooRpcClient = odooRpcClient);


			return sessions.ToArray();
		}
	}
}
