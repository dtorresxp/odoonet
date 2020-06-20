using OdooNet.Apps.Services.SyncFinanca.FIN5.Helpers;
using OdooNet.Core.POS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OdooNet.Apps.Services.SyncFin5.FIN5
{
	public class SqlFin5
	{
		public SqlFin5(string hostname, string database, string username, string password)
		{
			Hostname = hostname;
			Database = database;
			Username = username;
			Password = password;
		}

		public string Hostname { get; }
		public string Database { get; }
		public string Username { get; }
		public string Password { get; }

		public SqlConnection Connect()
		{
			SqlConnection connection = new SqlConnection();

			connection.ConnectionString = $"Server={this.Hostname};Database={this.Database};User Id={this.Username};Password={this.Password}";

			connection.Open();

			return connection;
		}

		public void Add(IProduct product)
		{
			int count = this.Count(product);
			if (count == 0)
			{
				this.Insert(product);
			}
			else if(count > 0)
			{
				this.Update(product);
			}
		}

		public void Add(IOrder order)
		{
			this.Insert(order);
		}
	}
}
