using OdooNet.Core.POS;
using OdooNet.Core.RES;
using OdooNet.Data.Client;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace OdooNet.Apps.Services.SyncFin5
{
	public class Worker
	{
		private readonly Timer timer;

		public Worker()
		{
			this.timer = new Timer(1000)
			{
				AutoReset = true
			};

			this.timer.Elapsed += Timer_Elapsed;
		}

		public ICompany Company { get; private set; }

		public DateTime LastExecution 
		{
			get => SyncFinanca.Properties.Settings.Default.LAST_EXEC_TIME;
			private set
			{
				SyncFinanca.Properties.Settings.Default.LAST_EXEC_TIME = value;
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

			//read data from source created after the last execution
			Log.Logger.Information($"Reading orders created after {this.LastExecution} ...");

			var orders = this.Company.GetOrders(createdAfter: this.LastExecution);

			Log.Logger.Information($" found {orders.Length} orders.");


			//saving current datetime time
			if (orders.Length > 0)
			{
				this.LastExecution = orders.Select(order => order.Created).Max().AddSeconds(1);

				//write data to target
				this.WriteOrdersToTarget(orders);
			}

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
	}
}
