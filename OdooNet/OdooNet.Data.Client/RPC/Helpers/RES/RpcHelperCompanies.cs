using OdooNet.Data.Client.RPC.Models.RES;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models;
using OdooRpc.CoreCLR.Client.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OdooNet.Data.Client.RPC.Helpers.RES
{
	public static class RpcHelperCompanies
	{
		

		public static Company GetCompany(this OdooRpcClient odooRpcClient, long id)
		{
			Task<Company[]> task = odooRpcClient.Get<Company[]>(
				new OdooSearchParameters(
					model: Company.MODEL,
					domainFilter: new OdooDomainFilter().Filter("id", "=", id)
				)
			);

			task.Wait();

			List<Company> companies = task.Result.ToList();
			companies.ForEach(company => company.OdooRpcClient = odooRpcClient);

			return task.Result.FirstOrDefault();
		}

		public static Company GetCompany(this OdooRpcClient odooRpcClient, string name)
		{
			Task<Company[]> task = odooRpcClient.Get<Company[]>(
				new OdooSearchParameters(
					model: Company.MODEL,
					domainFilter: new OdooDomainFilter().Filter("name", "=", name)
				)
			);

			task.Wait();

			List<Company> companies = task.Result.ToList();
			companies.ForEach(company => company.OdooRpcClient = odooRpcClient);

			return task.Result.FirstOrDefault();
		}

		public static Company[] GetCompanies(this OdooRpcClient odooRpcClient)
		{
			Task<Company[]> task = odooRpcClient.Get<Company[]>(new OdooSearchParameters(model: Company.MODEL));

			task.Wait();

			List<Company> companies = task.Result.ToList();
			companies.ForEach(company => company.OdooRpcClient = odooRpcClient);

			return companies.ToArray();
		}
	}
}
