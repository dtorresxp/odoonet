
using OdooNet.Data.Client.RPC.Models.RES;
using OdooNet.Data.Client.RPC.Helpers.RES;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Interfaces;
using OdooRpc.CoreCLR.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OdooNet.Core;
using OdooNet.Core.RES;

namespace OdooNet.Data.Client.RPC.Models
{
	public class Odoo : IOdoo
	{
		private readonly OdooRpcClient OdooRpcClient;

		public Odoo(OdooRpcClient odooRpcClient)
		{
			OdooRpcClient = odooRpcClient;
		}

		public string Version { get; set; }

		public string GetVersion()
		{
			Task<OdooVersionInfo> task = this.OdooRpcClient.GetOdooVersion();
			task.Wait();

			return task.Result.ServerVersion;
		}


		public ICompany GetCompany(long id) => this.OdooRpcClient.GetCompany(id);
		public ICompany GetCompany(string name) => this.OdooRpcClient.GetCompany(name);
		public ICompany[] GetCompanies() => this.OdooRpcClient.GetCompanies();

	}
}
