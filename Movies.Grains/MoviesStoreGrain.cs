using Movies.Contracts;
using Movies.GrainInterfaces;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MoviesStoreState
	{
		public Dictionary<long, MovieDetails> Store { get; set; } = new();
	}

	public class MoviesStoreGrain : Grain<MoviesStoreState>, IMoviesStoreGrain
	{
		public async Task AddOrUpdateMovieAsync(MovieDetails movie)
		{
			if (State.Store.TryGetValue(movie.Id, out var existingMovie))
			{
				State.Store[movie.Id] = movie;
			}
			else
			{
				State.Store.Add(movie.Id, movie);
			}

			await WriteStateAsync();
		}

		public Task<HashSet<MovieDetails>> GetAllMoviesAsync() 
		{
			var movies = new HashSet<MovieDetails>(State.Store.Values);
			return Task.FromResult(movies);
		}
		public Task<IEnumerable<MovieDetails>> GetMoviesByGenreAsync(string genre)
		{
			var matchedMovies = State.Store.Values.Where(m => m.Genres.Contains(genre));
			return Task.FromResult(matchedMovies);
		}

		public async Task RemoveMovieAsync(long movieId)
		{
			State.Store.Remove(movieId);

			await WriteStateAsync();
		}
	}
}