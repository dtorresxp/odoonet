using OdooNet.Data.Client.RPC.Helpers.POS;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdooNet.Data.Client.RPC.Models.POS
{
	public class Terminal : Base
	{
		public static string MODEL = "pos.config";
		public static string[] FIELDS = { "id", "display_name", "create_date" };







		public Session[] GetSessions()
			=> this.OdooRpcClient.GetSessions(terminalId: this.Id);

		public Session[] GetSessions(DateTime? createdAfter, DateTime? createdBefore) 
			=> this.OdooRpcClient.GetSessions(terminalId: this.Id, createdAfter: createdAfter, createdBefore: createdBefore);
	}
}
