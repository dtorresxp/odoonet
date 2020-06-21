using OdooNet.Apps.Services.SyncFin5.FIN5;
using OdooNet.Core.POS;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace OdooNet.Apps.Services.SyncFinanca.FIN5.Helpers
{
	public static class SqlHelperOrders
	{
		public static void Insert(this SqlFin5 sqlFin5, IOrder order)
		{
			bool inserted = false;

			try
			{
				using (SqlConnection connection = sqlFin5.Connect())
				{
					SqlCommand command;


					//check if the document is already stored
					command = connection.CreateCommand();
					command.CommandText = "SELECT COUNT(*) FROM [dbo].[FJ] WHERE [SHENIM2] = @reference";

					command.Parameters.Add("@reference", System.Data.SqlDbType.VarChar, 60).Value = order.Reference;

					int count = Convert.ToInt32(command.ExecuteScalar());

					if (count >  0)//order not yet inserted to SQL Financa 5
					{
						Log.Logger.Warning($"There is already {order.Reference} in SQL Financa 5. Insert will not be perfomed.");
					}
					else
					{ 
						//create order header
						command = connection.CreateCommand();
						command.CommandText = "[dbo].[FJ_INSERT_FJ]";
						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.Parameters.Add("@PKODUSER", System.Data.SqlDbType.VarChar, 15).Value = SyncFinanca.Properties.Settings.Default.FIN5_FJ_PKODUSER;
						command.Parameters.Add("@PDATEDOK", System.Data.SqlDbType.DateTime).Value = order.Date;
						command.Parameters.Add("@PKODFKL", System.Data.SqlDbType.VarChar, 15).Value = SyncFinanca.Properties.Settings.Default.FIN5_FJ_PKODFKL;
						command.Parameters.Add("@PKMON", System.Data.SqlDbType.VarChar, 15).Value = order.Currency.Name;
						command.Parameters.Add("@PKMAG", System.Data.SqlDbType.VarChar, 3).Value = SyncFinanca.Properties.Settings.Default.FIN5_FJ_KMAG;
						command.Parameters.Add("@PKURS2", System.Data.SqlDbType.Decimal).Value = order.Currency.Rate;
						command.Parameters.Add("@PNRRENDOR", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

						command.ExecuteNonQuery();

						int headerId = Convert.ToInt32(command.Parameters["@PNRRENDOR"].Value.ToString());

						//create order lines
						foreach (IOrderLine line in order.GetLines())
						{
							command = connection.CreateCommand();
							command.CommandText = "[dbo].[FJ_INSERT_FJSCR]";
							command.CommandType = System.Data.CommandType.StoredProcedure;

							command.Parameters.Add("@PNrRendor", System.Data.SqlDbType.Int).Value = headerId;
							command.Parameters.Add("@PTipKLL", System.Data.SqlDbType.VarChar, 1).Value = SyncFinanca.Properties.Settings.Default.FIN5_FJSCR_TIPKLL;
							command.Parameters.Add("@KODART", System.Data.SqlDbType.VarChar, 25).Value = line.Product.Code;
							command.Parameters.Add("@CMIM", System.Data.SqlDbType.Decimal).Value = line.Price;
							command.Parameters.Add("@SASI", System.Data.SqlDbType.Decimal).Value = line.Quantity;
							command.Parameters.Add("@VPATVSH", System.Data.SqlDbType.Decimal).Value = line.Value;
							command.Parameters.Add("@VTVSH", System.Data.SqlDbType.Decimal).Value = line.TaxValue;
							command.Parameters.Add("@VTOT", System.Data.SqlDbType.Decimal).Value = line.TotalValue;

							command.ExecuteNonQuery();

						}

						//create stockout document
						command = connection.CreateCommand();
						command.CommandText = "[dbo].[FJ_INSERT_FD]";
						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.Parameters.Add("@PNrRendor", System.Data.SqlDbType.Int).Value = headerId;

						command.ExecuteNonQuery();


						//update some values in order header
						command = connection.CreateCommand();
						command.CommandText = @"UPDATE [FJ] SET [SHENIM2] = @reference WHERE [NRRENDOR] = @headerId";
						command.CommandType = System.Data.CommandType.Text;

						command.Parameters.Add("@headerId", System.Data.SqlDbType.Int).Value = headerId;
						command.Parameters.Add("@reference", System.Data.SqlDbType.VarChar, 60).Value = order.Reference;

						command.ExecuteNonQuery();




						Log.Logger.Information($"{order.Reference} inserted in SQL Financa 5.");
					}
				}
			}
			catch(Exception ex)
			{
				Log.Logger.Error(ex, "Error during order creation in SQL Financa 5.");
			}
		}

		
	}
}
