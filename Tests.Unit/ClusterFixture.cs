using Orleans.TestingHost;
using System;
using Xunit;

namespace Tests.Unit
{
	[CollectionDefinition(ClusterCollection.Name)]
	public class ClusterCollection : ICollectionFixture<ClusterFixture>
	{
		public const string Name = "ClusterCollection";
	}

	public class ClusterFixture : IDisposable
	{
		public ClusterFixture()
		{
			var builder = new TestClusterBuilder();
			Cluster = builder.Build();
			Cluster.Deploy();
		}

		public void Dispose() => Cluster.StopAllSilos();

		public TestCluster Cluster { get; }
	}
}
