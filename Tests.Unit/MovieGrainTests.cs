using FluentAssertions;
using Moq;
using Movies.GrainInterfaces;
using NSubstitute;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tests.Unit.Builders;
using Xunit;

namespace Tests.Unit
{
	[Collection(ClusterCollection.Name)]
	public class MovieGrainTests
	{
		private readonly TestCluster _cluster;

		public MovieGrainTests(ClusterFixture fixture)
		{
			_cluster = fixture.Cluster;
		}

		[Fact]
		public async Task AddsOrUpdatesMovieCorrectly()
		{
			var movie = MovieDetailsBuilder.InMemory.WithDefaults().Build();

			var moviesStore = new Mock<IMoviesStoreGrain>();
			moviesStore.Setup(grain => grain.AddOrUpdateMovieAsync(movie)).Returns(Task.CompletedTask);

			var movieGrain = _cluster.GrainFactory.GetGrain<IMovieGrain>(1);

			await movieGrain.Invoking(async x => await x.AddOrUpdateMovieAsync(movie)).Should().CompleteWithinAsync(TimeSpan.FromSeconds(20));
		}
	}
}
