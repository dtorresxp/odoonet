
using Serilog;
using System;
using System.Diagnostics;
using Topshelf;

namespace OdooNet.Apps.Services.SyncFin5
{
	/// <summary>
	/// Inspired by https://www.youtube.com/watch?v=y64L-3HKuP0&t=1227s
	/// </summary>
	public static class Program
	{
		
		public static void Main(string[] args)
		{
			//if (Debugger.IsAttached)
			//{
			//	SyncFinanca.Properties.Settings.Default.LAST_SYNC_ORDER_DATE = new DateTime(2020, 01, 01, 00, 00, 00);
			//	SyncFinanca.Properties.Settings.Default.LAST_SYNC_PRODUCT_DATE = new DateTime(2020, 01, 01, 00, 00, 00);
			//	SyncFinanca.Properties.Settings.Default.Save();
			//}

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File(@".\log.txt", flushToDiskInterval: TimeSpan.FromSeconds(5), rollOnFileSizeLimit: true, fileSizeLimitBytes: 500000000)		
				.CreateLogger();



			var exitCode = HostFactory.Run(x =>
			{
				x.UseSerilog();
				
				x.Service<Worker>(s =>
				{
					s.ConstructUsing(heatbeat => new Worker());
					
					s.WhenStarted(heartbeat => heartbeat.Start());

					s.WhenStopped(heartbeat =>
					{
						heartbeat.Stop();
						Log.CloseAndFlush();
					});
				});
				
				x.RunAsLocalSystem();

				x.SetServiceName("OdooFin5Sync");
				x.SetDisplayName("Odoo Financa 5 Sync");
				x.SetDisplayName("Sherbim i sinkronizimit midis Odoo dhe Financa 5");
			});

			int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());

			Environment.ExitCode = exitCodeValue;
		}
	}
}
