using OdooNet.Core;
using OdooNet.Data.Client.RPC.Models;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdooNet.Data.Client
{
	public static class RPCClient
	{
		public static IOdoo Connect(string host, int port, bool isSsl, string database, string username, string password)
		{
			OdooConnectionInfo odooConnectionInfo = new OdooConnectionInfo()
			{
				Host = host,
				Port = port,
				IsSSL = isSsl,
				Database = database,
				Username = username,
				Password = password
			};

			OdooRpcClient odooRpcClient = new OdooRpcClient(odooConnectionInfo);



			Task task = odooRpcClient.Authenticate();
			task.Wait();

			Task<OdooVersionInfo> getVersionTask = odooRpcClient.GetOdooVersion();
			task.Wait();

			string serverVersion = getVersionTask.Result.ServerVersion;

			return new Odoo(odooRpcClient) { Version = serverVersion };
		}
	}
}
