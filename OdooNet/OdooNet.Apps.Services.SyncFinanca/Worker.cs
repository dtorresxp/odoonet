using OdooNet.Apps.Services.SyncFin5.FIN5;
using OdooNet.Apps.Services.SyncFinanca.FIN5.Helpers;
using OdooNet.Core.POS;
using OdooNet.Core.RES;
using OdooNet.Data.Client;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Timers;

namespace OdooNet.Apps.Services.SyncFin5
{
	public class Worker
	{
		private readonly Timer timer;

		public Worker()
		{
			this.timer = new Timer(5000)
			{
				AutoReset = true
			};

			this.timer.Elapsed += Timer_Elapsed;
		}

		public ICompany Company { get; private set; }

		public SqlFin5 SqlFin5 { get; private set; }

		public DateTime LastSyncOrderDate 
		{
			get => SyncFinanca.Properties.Settings.Default.LAST_SYNC_ORDER_DATE;
			private set
			{
				SyncFinanca.Properties.Settings.Default.LAST_SYNC_ORDER_DATE = value;
				SyncFinanca.Properties.Settings.Default.Save();
			}
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.Execute();
		}

		public void Start()
		{
			this.Company = RPCClient.Connect(
				host: SyncFinanca.Properties.Resources.ODOO_HOSTNAME,
				port: int.Parse(SyncFinanca.Properties.Resources.ODOO_PORT),
				isSsl: bool.Parse(SyncFinanca.Properties.Resources.ODOO_ISSL),
				database: SyncFinanca.Properties.Resources.ODOO_DATABE,
				username: SyncFinanca.Properties.Resources.ODOO_USERNAME,
				password: SyncFinanca.Properties.Resources.ODOO_PASSWORD
			).GetCompany(SyncFinanca.Properties.Resources.ODOO_COMPANY);

			this.SqlFin5 = new SqlFin5(
				SyncFinanca.Properties.Resources.FIN5_HOSTNAME,
				SyncFinanca.Properties.Resources.FIN5_DATABASE,
				SyncFinanca.Properties.Resources.FIN5_USERNAME,
				SyncFinanca.Properties.Resources.FIN5_PASSWORD
			);

			this.timer.Start();
		}

		public void Stop()
		{
			this.timer.Stop();

			this.Company = null;
		}

		public void Execute()
		{
			//stop timer
			this.timer.Stop();

			//this.SyncProducts();

			this.SyncOrders();
			
			//start timer
			this.timer.Start();
		}

		private void WriteOrdersToTarget(IOrder[] orders)
		{
			orders.ToList().ForEach(order =>
			{
				Log.Logger.Information($"Saving order {order.Reference} to Financa 5 database...");

				Task.Delay(1000).Wait();

				Log.Logger.Information("done!");				
			});
		}



		private void SyncProducts()
		{
			if (this.Company != null)
			{
				IProduct[] products = this.Company.GetProducts();

				foreach (IProduct product in products)
				{
					this.SqlFin5.Add(product);
				}
				
			}
		}



		private void SyncOrders()
		{
			if (this.Company != null)
			{
				foreach (IOrder order in this.Company.GetOrders(createdAfter: this.LastSyncOrderDate))
				{
					Log.Logger.Information($"Storing order {order.Reference} to SQL Financa 5.");

					this.SqlFin5.Add(order);

					if (this.LastSyncOrderDate < order.Created)
					{
						this.LastSyncOrderDate = order.Created.AddSeconds(1);

						Log.Logger.Information($"Create date flag setted to {this.LastSyncOrderDate}");
					}
				}
			}

		}
	}
}
