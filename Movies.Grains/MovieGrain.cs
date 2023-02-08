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

		public Task AddMovie(string name) => throw new System.NotImplementedException();
		public Task<MovieDetails> GetMovieAsync() => Task.FromResult(State);
		public Task<List<MovieDetails>> GetMoviesAsync() => throw new System.NotImplementedException();
		public Task<List<MovieDetails>> GetMoviesByGenreAsync(string genre) => throw new System.NotImplementedException();
		public Task UpdateMovie(string name) => throw new System.NotImplementedException();
	}
}