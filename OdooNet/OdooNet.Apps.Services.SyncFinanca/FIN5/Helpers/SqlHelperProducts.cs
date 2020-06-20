using OdooNet.Apps.Services.SyncFin5.FIN5;
using OdooNet.Core.POS;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OdooNet.Apps.Services.SyncFinanca.FIN5.Helpers
{
	public static class SqlHelperProducts
	{
		private static string SQL_INSERT = @"
               INSERT INTO [dbo].[ARTIKUJ] (
	               [KOD]
                   ,[PERSHKRIM]
                   ,[KOSTMES]
                   ,[KOSTPLAN]
                   ,[NJESI]
                   ,[MINI]
                   ,[MAKS]
                   ,[NEGST]
                   ,[TIP]
                   ,[NJESB]
                   ,[KOEFB]
                   ,[CMB]
                   ,[NJESSH]
                   ,[KOEFSH]
                   ,[CMSH]
                   ,[TATIM]
                   ,[FISKAL]
                   ,[PESHA]
                   ,[KONV1]
                   ,[KONV2]
                   ,[BC]
                   ,[NOTACTIV]
                   ,[KODTVSH]
                   ,[KODLM]
                   ,[DEP]
                   ,[KOEFICENT]
               )
               VALUES (
	               @code, 
                    @name, 
                    @costPrice,
                    @costPrice, 
                    'COP',
                    0, 
                    99999999, 
                    0, 
                    'P', 
                    'COP', 
                    1, 
                    @buyPrice, 
                    'COP', 
                    1,  
                    @sellPrice, 
                    1, 
                    0, 
                    0, 
                    1, 
                    1, 
                    @barcode, 
                    0,
                    2,
                    '001',
                    '',
                    0
               )";

		public static int Insert(this SqlFin5 sqlFin5, IProduct product)
		{
               int count = -1;

               try
               {
                    Log.Logger.Information($"Updating product with code {product.Code}");

                    using (SqlConnection connection = sqlFin5.Connect())
                    {
                         SqlCommand command = connection.CreateCommand();
                         command.CommandText = SQL_INSERT;
                         command.CommandType = System.Data.CommandType.Text;

                         command.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 25).Value = product.Code;
                         command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = product.Name;
                         command.Parameters.Add("@costPrice", System.Data.SqlDbType.Decimal).Value = product.CostPrice;
                         command.Parameters.Add("@buyPrice", System.Data.SqlDbType.Decimal).Value = product.BuyPrice;
                         command.Parameters.Add("@sellPrice", System.Data.SqlDbType.Decimal).Value = product.SellPrice;
                         command.Parameters.Add("@barcode", System.Data.SqlDbType.VarChar, 25).Value = product.Barcode;

                         count = command.ExecuteNonQuery();

                         Log.Logger.Information($"Inserted {count} products with code {product.Code}");
                    }
               }
               catch(Exception ex)
               {
                    Log.Logger.Error(ex, "Error during SQL product insert.");
               }

               return count;
		}

          private static string SQL_UPDATE = @"
               UPDATE [dbo].[ARTIKUJ] SET
                    [PERSHKRIM] = @name
                   ,[CMSH] = @sellPrice
                   ,[BC] = @barcode
               WHERE 
                    [KOD] = @code";
          public static int Update(this SqlFin5 sqlFin5, IProduct product)
          {
               int count = -1;

               try
               {
                    Log.Logger.Information($"Updating product with code {product.Code}");

                    using (SqlConnection connection = sqlFin5.Connect())
                    {
                         SqlCommand command = connection.CreateCommand();
                         command.CommandText = SQL_UPDATE;
                         command.CommandType = System.Data.CommandType.Text;

                         command.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 25).Value = product.Code;
                         command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 100).Value = product.Name;
                         command.Parameters.Add("@sellPrice", System.Data.SqlDbType.Decimal).Value = product.SellPrice;
                         command.Parameters.Add("@barcode", System.Data.SqlDbType.VarChar, 25).Value = product.Barcode;

                         count = command.ExecuteNonQuery();
                    }

                    Log.Logger.Information($"Updated {count} products with code {product.Code}");
               }
               catch (Exception ex)
               {
                    Log.Logger.Error(ex, "Error during SQL product update.");
               }

               return count;
          }

          private static string SQL_COUNT = @"SELECT COUNT(*) FROM [dbo].[ARTIKUJ] WHERE [KOD] = @code";
          public static int Count(this SqlFin5 sqlFin5, IProduct product)
          {
               int count = -1;               

               try
               {
                    Log.Logger.Information($"Counting existing entries with code {product.Code}");

                    using (SqlConnection connection = sqlFin5.Connect())
                    {
                         SqlCommand command = connection.CreateCommand();
                         command.CommandText = SQL_COUNT;
                         command.CommandType = System.Data.CommandType.Text;

                         command.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 25).Value = product.Code;

                         count = Convert.ToInt32(command.ExecuteScalar());
                    }

                    Log.Logger.Information($"Found {count} occurences of product with code {product.Code}");
               }
               catch (Exception ex)
               {
                    Log.Logger.Error(ex, "Error during SQL product count!");
               }

               return count;
          }
	}
}
