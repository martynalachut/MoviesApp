using Microsoft.AspNetCore.Mvc;
using Movies.Contracts;
using Movies.GrainInterfaces;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Server.Controllers
{
	[ApiController]
	[Route(ApiRoute.Movies)]
	public class MoviesController : Controller
	{
		private IClusterClient _orleansClient;

		public MoviesController(IClusterClient clusterClient)
		{
			_orleansClient = clusterClient;
		}

		[HttpGet("{movieId}")]
		public async Task<IActionResult> GetMovieAsync(int movieId)
		{
			var movie = await _orleansClient.GetGrain<IMovieGrain>(movieId).GetMovieDetailsAsync();

			return Ok(movie);
		}

		[HttpGet()]
		public async Task<IActionResult> GetAllMoviesAsync()
		{
			var movies = await _orleansClient.GetGrain<IMoviesStoreGrain>("MoviesStore").GetAllMoviesAsync();

			return Ok(movies.ToList());
		}

		[HttpPost]
		public async Task<IActionResult> AddMovieAsync(MovieDetails movie)
		{
			await _orleansClient.GetGrain<IMovieGrain>(movie.Id).AddOrUpdateMovieAsync(movie);

			return Ok();
		}

		[HttpPut("{movieId}")]
		public async Task<IActionResult> UpdateMovieAsync(int movieId, MovieDetails movie)
		{
			await _orleansClient.GetGrain<IMovieGrain>(movieId).AddOrUpdateMovieAsync(movie);

			return Ok();
		}

		[HttpDelete("{movieId}")]
		public async Task<IActionResult> DeleteMovieAsync(int movieId)
		{
			await _orleansClient.GetGrain<IMoviesStoreGrain>(movieId.ToString()).RemoveMovieAsync(movieId);

			return Ok();
		}
	}
}