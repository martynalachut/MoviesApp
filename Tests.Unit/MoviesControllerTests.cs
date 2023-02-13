using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.GrainInterfaces;
using Movies.Server.Controllers;
using NSubstitute;
using Orleans;
using System;
using System.Net;
using System.Threading.Tasks;
using Tests.Unit.Builders;
using Xunit;

namespace Tests.Unit
{
	public class MoviesControllerTests
	{
		private readonly IClusterClient _orleansClient;
		private readonly MoviesController _controller;

		public MoviesControllerTests()
		{
			_orleansClient = Substitute.For<IClusterClient>();
			_controller = new MoviesController(_orleansClient);
		}

		[Fact]
		public async Task PostMovieAsync_WithValidRequest_ReturnsSuccess()
		{
			var movie = MovieDetailsBuilder.InMemory.WithDefaults().Build();

			var movieGrain = new Mock<IMovieGrain>();
			movieGrain.Setup(x => x.AddOrUpdateMovieAsync(movie)).Returns(Task.CompletedTask);

			var result = await _controller.AddMovieAsync(movie);
			result.Should().BeOfType<OkResult>();
			result.As<OkResult>().StatusCode.Should().Be((int) HttpStatusCode.OK);
		}

		[Fact(Skip = "TODO: Figure out why the result of GetMovieDetailsAsync() is not being mocked out")]
		public async Task GetMovieAsync_ForExistingMovie_ReturnsSuccess()
		{
			var movie = MovieDetailsBuilder.InMemory.WithDefaults().Build();

			var movieGrain = new Mock<IMovieGrain>();
			movieGrain.Setup(grain => grain.GetMovieDetailsAsync()).Returns(Task.FromResult(movie));
			

			var result = await _controller.GetMovieAsync(movie.Id);
			result.Should().BeOfType<OkObjectResult>();
			result.As<OkObjectResult>().Value.Should().BeEquivalentTo(movie);
			result.As<OkObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
		}
	}

}
