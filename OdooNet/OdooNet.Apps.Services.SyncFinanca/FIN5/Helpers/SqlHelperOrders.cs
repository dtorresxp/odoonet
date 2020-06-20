using OdooNet.Apps.Services.SyncFin5.FIN5;
using OdooNet.Core.POS;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace OdooNet.Apps.Services.SyncFinanca.FIN5.Helpers
{
	public static class SqlHelperOrders
	{
		/// <summary>
		/// Procedure call for creating order header
		/// </summary>
		private static string SQL_INSERT_HEADER = "[dbo].[FJ_INSERT_FJ]";
		//@"
		//	DECLARE @RC int
		//	DECLARE @PKODUSER varchar(10)
		//	DECLARE @PDATEDOK datetime
		//	DECLARE @PKODFKL varchar(10)
		//	DECLARE @PKMON varchar(3)
		//	DECLARE @PKMAG varchar(3)
		//	DECLARE @PKURS2 float
		//	DECLARE @PNRRENDOR int

		//	-- TODO: Set parameter values here.
		//	SET @KODUSER = @userCode
		//	SET @PDATEDOK = @orderDate
		//	SET @PKODFKL = @customerCode
		//	SET @KMON = @currencyCode
		//	SET @PKURS = @currencyRate
		//	SET @KMAG = @warehouseCode

		//	EXECUTE @RC = [dbo].[FJ_INSERT_FJ] 
		//		 @PKODUSER
		//		,@PDATEDOK
		//		,@PKODFKL
		//		,@PKMON
		//		,@PKMAG
		//		,@PKURS2
		//		,@PNRRENDOR OUTPUT

		//	SELECT @PNRRENDOR
		//	GO";

		/// <summary>
		/// Procedure call for creating order line
		/// </summary>
		private static string SQL_INSERT_LINE = "[dbo].[FJ_INSERT_FJSCR]";
		//@"
		//	DECLARE @RC int
		//	DECLARE @PNrRendor int
		//	DECLARE @PTipKLL varchar(1)
		//	DECLARE @KODART varchar(30)
		//	DECLARE @CMIM float
		//	DECLARE @SASI float
		//	DECLARE @VPATVSH float
		//	DECLARE @VTVSH float
		//	DECLARE @VTOT float

		//	-- TODO: Set parameter values here.

		//	EXECUTE @RC = [dbo].[FJ_INSERT_FJSCR] 
		//		 @PNrRendor
		//		,@PTipKLL
		//		,@KODART
		//		,@CMIM
		//		,@SASI
		//		,@VPATVSH
		//		,@VTVSH
		//		,@VTOT
		//	GO";

		/// <summary>
		/// Procedure call for creating stock out document
		/// </summary>
		private static string SQL_INSERT_STOCKOUT = "FJ_INSERT_FD";


		public static void Insert(this SqlFin5 sqlFin5, IOrder order)
		{
			try
			{
				using (SqlConnection connection = sqlFin5.Connect())
				{
					SqlCommand command = connection.CreateCommand();

					//create order header
					command.CommandText = SQL_INSERT_HEADER;
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.Add("@PKODUSER", System.Data.SqlDbType.VarChar, 15).Value = "ODOO";
					command.Parameters.Add("@PDATEDOK", System.Data.SqlDbType.DateTime).Value = order.Date;
					command.Parameters.Add("@PKODFKL", System.Data.SqlDbType.VarChar, 15).Value = "KPOS";
					command.Parameters.Add("@PKMON", System.Data.SqlDbType.VarChar, 15).Value = order.Currency.Name;
					command.Parameters.Add("@PKMAG", System.Data.SqlDbType.VarChar, 3).Value = "M01";
					command.Parameters.Add("@PKURS2", System.Data.SqlDbType.Decimal).Value = order.Currency.Rate;
					command.Parameters.Add("@PNRRENDOR", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

					int affectedRows = command.ExecuteNonQuery();

					int headerId = Convert.ToInt32(command.Parameters["@PNRRENDOR"].Value.ToString());

					if (headerId <= 0)
					{
						throw new InvalidDataException($"Something wrong trying to save order {order.Reference} in SQL Financa 5. Retured header id was {headerId}");
					}

					//create order lines
					foreach (IOrderLine line in order.GetLines())
					{
						command = connection.CreateCommand();
						command.CommandText = SQL_INSERT_LINE;
						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.Parameters.Add("@PNrRendor", System.Data.SqlDbType.Int).Value = headerId;
						command.Parameters.Add("@PTipKLL", System.Data.SqlDbType.VarChar, 1).Value = "K";
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
					command.CommandText = SQL_INSERT_STOCKOUT;
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.Add("@PNrRendor", System.Data.SqlDbType.Int).Value = headerId;

					command.ExecuteNonQuery();
				}
			}
			catch(Exception ex)
			{
				Log.Logger.Error(ex, "Error during FJ creation in Financa 5.");
			}
		}
	}
}
