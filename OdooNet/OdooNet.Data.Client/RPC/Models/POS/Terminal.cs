using OdooNet.Core.POS;
using OdooNet.Data.Client.RPC.Helpers.POS;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Terminal : Base, ITerminal
	{
		public static string MODEL = "pos.config";
		public static string[] FIELDS = { "id", "name", "create_date" };

		public override string ToString()
		{
			return "POS" + this.Id + "(" + this.Name + ")";
		}

		public Session[] GetSessions()
			=> this.OdooRpcClient.GetSessions(terminalId: this.Id);

		public Session[] GetSessions(DateTime? createdAfter, DateTime? createdBefore) 
			=> this.OdooRpcClient.GetSessions(terminalId: this.Id, createdAfter: createdAfter, createdBefore: createdBefore);


		public IOrder[] GetOrders(DateTime createdAfter)
			=> this.OdooRpcClient.GetPosOrders(terminalId: this.Id, createdAfter: createdAfter);
		public IOrder[] GetOrders(DateTime createdAfter, DateTime createdBefore)
			=> this.OdooRpcClient.GetPosOrders(terminalId: this.Id, createdAfter: createdAfter, createdBefore: createdBefore);

		
	}
}
