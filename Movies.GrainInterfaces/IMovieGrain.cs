using Movies.Contracts;
using Orleans;

namespace Movies.GrainInterfaces
{
	public interface IMovieGrain : IGrainWithStringKey
	{
		Task<MovieModel> Get();
		Task Set(string name);
	}
}