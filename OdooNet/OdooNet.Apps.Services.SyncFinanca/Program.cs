
using Serilog;
using System;
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
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
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
