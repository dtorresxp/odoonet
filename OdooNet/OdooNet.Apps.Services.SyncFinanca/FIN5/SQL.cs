using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OdooNet.Apps.Services.SyncFin5.FIN5
{
	public class SQL
	{
		public static SqlConnection Connect()
		{
			string server = SyncFinanca.Properties.Resources.FIN5_HOSTNAME;
			string database = SyncFinanca.Properties.Resources.FIN5_DATABASE;
			string username = SyncFinanca.Properties.Resources.FIN5_USERNAME;
			string password = SyncFinanca.Properties.Resources.FIN5_PASSWORD;

			SqlConnection connection = new SqlConnection();

			connection.ConnectionString = $"Server={server};Database={database};User Id={username};Password={password}";

			connection.Open();

			return connection;
		}
	}
}
