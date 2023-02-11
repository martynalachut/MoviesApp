using Movies.Contracts;
using Movies.GrainInterfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MovieGrain : Grain<MovieDetails>, IMovieGrain
	{
		public MovieGrain() { }

		public async Task AddOrUpdateMovieAsync(MovieDetails movieDetails)
		{
			State = movieDetails;

			await WriteStateAsync();
		}

		public Task<MovieDetails> GetMovieAsync()
		{
			return Task.FromResult(State);
		}

		public Task<IList<MovieDetails>> GetMoviesAsync() => throw new System.NotImplementedException(); //Task.FromResult(_moviesCache.Values.ToList());
		public Task<IList<MovieDetails>> GetMoviesByGenreAsync(string genre) => throw new System.NotImplementedException();
	}
}