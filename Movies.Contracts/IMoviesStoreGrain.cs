using Movies.Contracts;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.GrainInterfaces
{
	public interface IMoviesStoreGrain : IGrainWithStringKey
	{
		Task<HashSet<MovieDetails>> GetAllMoviesAsync();
		Task<IEnumerable<MovieDetails>> GetMoviesByGenreAsync(string genre);
		Task AddOrUpdateMovieAsync(MovieDetails movie);
		Task RemoveMovieAsync(long movieId);
	}
}