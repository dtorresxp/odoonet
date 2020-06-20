using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdooNet.Core.POS;
using OdooNet.Core.RES;
using OdooNet.Data.Client.RPC.Helpers.POS;
using OdooNet.Data.Client.RPC.Models.POS;
using OdooRpc.CoreCLR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.RES
{
	public class Company : Base, ICompany
	{
		public static string MODEL = "res.company";



		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("street")]
		public string Street { get; set; }

		[JsonProperty("street2")]
		public string Street2 { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("state_id")]
		public JToken StateId { get; set; }

		[JsonProperty("country_id")]
		public JToken CountryId { get; set; }

		public string Address => $"{this.Street}, {this.Street2}, {this.City}, {this.StateId.SelectToken("[0]")}, {this.CountryId.SelectToken("[0]")}";





		public IProduct[] GetProducts() => this.OdooRpcClient.GetProducts();

		public ICustomer[] GetCustomers() => throw new NotImplementedException();


		public ITerminal GetTerminal(int id) => this.OdooRpcClient.GetTerminal(companyId: this.Id, terminalId: id);
		public ITerminal GetTerminal(string name) => this.OdooRpcClient.GetTerminal(companyId: this.Id, terminalName: name); 
		public ITerminal[] GetTerminals() => this.OdooRpcClient.GetTerminals(this.Id);


		public IOrder[] GetOrders(DateTime createdAfter)
			=> this.OdooRpcClient.GetPosOrders(companyId: this.Id, createdAfter: createdAfter);
		public IOrder[] GetOrders(DateTime createdAfter, DateTime createdBefore) 
			=> this.OdooRpcClient.GetPosOrders(companyId: this.Id, createdAfter: createdAfter, createdBefore: createdBefore);

		ITerminal[] ICompany.GetTerminals() => this.OdooRpcClient.GetTerminals(companyId: this.Id);

		
	}
}
