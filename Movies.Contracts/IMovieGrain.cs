using Movies.Contracts;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.GrainInterfaces
{
	public interface IMovieGrain : IGrainWithIntegerKey
	{
		Task<MovieDetails> GetMovieAsync();
		Task<IList<MovieDetails>> GetMoviesAsync();
		Task<IList<MovieDetails>> GetMoviesByGenreAsync(string genre);
		Task AddOrUpdateMovieAsync(MovieDetails movieDetails);
	}
}