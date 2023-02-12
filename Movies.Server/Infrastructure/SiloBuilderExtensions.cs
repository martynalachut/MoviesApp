using Microsoft.Extensions.Hosting;
using Movies.Core;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Diagnostics;
using System.Net;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace Movies.Server.Infrastructure
{
	public enum StorageProviderType
	{
		Memory
	}

	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class AppSiloOptions
	{
		private string DebuggerDisplay => $"GatewayPort: '{GatewayPort}', SiloPort: '{SiloPort}'";

		public int GatewayPort { get; set; } = 30000;
		public int SiloPort { get; set; } = 11111;
		public StorageProviderType? StorageProviderType { get; set; }
	}

	public class AppSiloBuilderContext
	{
		public HostBuilderContext HostBuilderContext { get; set; }
		public IAppInfo AppInfo { get; set; }
		public AppSiloOptions SiloOptions { get; set; }
	}

	public static class SiloBuilderExtensions
	{

		public static ISiloBuilder UseAppConfiguration(this ISiloBuilder siloHost, AppSiloBuilderContext context, HostBuilderContext hostBuilderContext)
		{
			var appInfo = context.AppInfo;
			if (hostBuilderContext.HostingEnvironment.IsDevelopment())
			{
				siloHost
				.AddDynamoDBGrainStorageAsDefault(options =>
				 {
					 options.Service = "http://localhost:4566";
					 options.UseJson = true;
				 })
				.Configure<ClusterOptions>(options =>
				{
					options.ClusterId = appInfo.ClusterId;
					options.ServiceId = appInfo.Name;
				});

				siloHost.UseDevelopment(context);
				siloHost.UseDevelopmentClustering(context);
			}
			
			// TODO: Use DynamoDB in Production clustering
			//.AddDynamoDBGrainStorageAsDefault(options =>
			// {
			//	 options.Service = "http://...";
			//	 options.UseJson = true;
			// })

			return siloHost;
		}

		private static ISiloBuilder UseDevelopment(this ISiloBuilder siloHost, AppSiloBuilderContext context)
		{
			siloHost
				.ConfigureServices(services =>
				{
					//services.Configure<GrainCollectionOptions>(options => { options.CollectionAge = TimeSpan.FromMinutes(1.5); });
				});

			return siloHost;
		}

		private static ISiloBuilder UseDevelopmentClustering(this ISiloBuilder siloHost, AppSiloBuilderContext context)
		{
			var siloAddress = IPAddress.Loopback;
			var siloPort = context.SiloOptions.SiloPort;
			var gatewayPort = context.SiloOptions.GatewayPort;

			return siloHost
					.UseLocalhostClustering(siloPort: siloPort, gatewayPort: gatewayPort);
		}
	}
}