using Movies.Contracts;
using Orleans;
using System.Threading.Tasks;

namespace Movies.GrainInterfaces
{
	public interface IMovieGrain : IGrainWithIntegerKey
	{
		Task<MovieDetails> GetMovieDetailsAsync();
		Task AddOrUpdateMovieAsync(MovieDetails movie);
	}
}