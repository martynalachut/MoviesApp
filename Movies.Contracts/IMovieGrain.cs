using Movies.Contracts;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.GrainInterfaces
{
	public interface IMovieGrain : IGrainWithIntegerKey
	{
		Task<MovieDetails> GetMovieAsync();
		Task<List<MovieDetails>> GetMoviesAsync();
		Task<List<MovieDetails>> GetMoviesByGenreAsync(string genre);
		Task AddMovie(string name);
		Task UpdateMovie(string name);
	}
}