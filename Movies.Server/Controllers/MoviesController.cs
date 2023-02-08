using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Contracts;
using Movies.GrainInterfaces;
using Movies.Server.Authorization;
using Orleans;
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
		//[Authorize(Policy = MoviesAuthorizationPolicies.RetrievePolicy)]
		public async Task<MovieDetails> GetMovieAsync(long movieId)
		{
			var grain = _orleansClient.GetGrain<IMovieGrain>(movieId);
			return await grain.GetMovieAsync();
		}

		//[HttpGet("")]
		//[Authorize(Policy = MoviesAuthorizationPolicies.RetrievePolicy)]
		//public async Task<IActionResult> GetMoviesAsync()
		//{

		//}

		//[HttpPost]
		//[Authorize(Policy = MoviesAuthorizationPolicies.UploadPolicy)]
		//public async Task<IActionResult> AddMovieAsync()
		//{

		//}

		//[HttpPut("{movieId}")]
		//[Authorize(Policy = MoviesAuthorizationPolicies.UploadPolicy)]
		//public async Task<IActionResult> UpdateMovieDetailsAsync(string movieId)
		//{

		//}
	}
}