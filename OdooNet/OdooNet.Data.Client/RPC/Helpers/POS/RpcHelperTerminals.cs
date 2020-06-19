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
	public static class RpcHelperTerminals
	{
		public static Terminal GetTerminal(this OdooRpcClient odooRpcClient, long companyId, long terminalId)
		{
			OdooDomainFilter filter = new OdooDomainFilter()
				.Filter("company_id", "=", companyId)
				.Filter("id", "=", terminalId);


			Task<Terminal[]> task = odooRpcClient.Get<Terminal[]>(
				new OdooSearchParameters(
						Terminal.MODEL,
						filter
				),
				new OdooFieldParameters(OrderLine.FIELDS)
			);

			task.Wait();

			Terminal terminal = task.Result.FirstOrDefault();
			if (terminal != null)
			{
				terminal.OdooRpcClient = odooRpcClient;
			}

			return terminal;
		}

		public static Terminal GetTerminal(this OdooRpcClient odooRpcClient, long companyId, string terminalName)
		{
			OdooDomainFilter filter = new OdooDomainFilter()
				.Filter("company_id", "=", companyId)
				.Filter("display_name", "=", terminalName);


			Task<Terminal[]> task = odooRpcClient.Get<Terminal[]>(
				new OdooSearchParameters(
						Terminal.MODEL,
						filter
				),
				new OdooFieldParameters(Terminal.FIELDS)
			);

			task.Wait();

			Terminal terminal = task.Result.FirstOrDefault();
			if (terminal != null)
			{
				terminal.OdooRpcClient = odooRpcClient;
			}

			return terminal;
		}

		public static Terminal[] GetTerminals(this OdooRpcClient odooRpcClient, long companyId)
		{
			OdooDomainFilter filter = new OdooDomainFilter().Filter("company_id", "=", companyId);


			Task<Terminal[]> task = odooRpcClient.Get<Terminal[]>(
				new OdooSearchParameters(
						Terminal.MODEL,
						filter
				),
				new OdooFieldParameters(OrderLine.FIELDS)
			);

			List<Terminal> terminals = task.Result.ToList();
			terminals.ForEach(terminal => terminal.OdooRpcClient = odooRpcClient);

			return terminals.ToArray();
		}
	}
}
