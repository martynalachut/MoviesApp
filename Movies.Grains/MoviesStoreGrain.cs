using Movies.Contracts;
using Movies.GrainInterfaces;
using Orleans;
using Orleans.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MoviesStoreGrain : Grain, IMoviesStoreGrain
	{
		private readonly IPersistentState<HashSet<long>> _moviesIds;
		private readonly Dictionary<long, MovieDetails> _moviesCache = new();

		public MoviesStoreGrain(
			[PersistentState(
			stateName: "MoviesStore", storageName: "movies-store")]
		IPersistentState<HashSet<long>> state) => _moviesIds = state;

		public async Task AddOrUpdateMovieAsync(MovieDetails movie)
		{
			_moviesIds.State.Add(movie.Id);
			_moviesCache.Add(movie.Id, movie);

			await _moviesIds.WriteStateAsync();
		}
		public override Task OnActivateAsync() => PopulateMoviesCacheAsync();

		public Task<HashSet<MovieDetails>> GetAllMoviesAsync() 
		{
			var movies = new HashSet<MovieDetails>(_moviesCache.Values);
			return Task.FromResult(movies);
		}
		public Task<IEnumerable<MovieDetails>> GetMoviesByGenreAsync(string genre) => throw new System.NotImplementedException();

		public async Task RemoveMovieAsync(long movieId)
		{
			_moviesIds.State.Remove(movieId);
			_moviesCache.Remove(movieId);

			await _moviesIds.WriteStateAsync();
		}

		private async Task PopulateMoviesCacheAsync()
		{
			if (_moviesIds is not { State.Count: > 0 })
			{
				return;
			}

			await Task.Run(() => Parallel.ForEach(_moviesIds.State, new ParallelOptions { TaskScheduler = TaskScheduler.Current },
			async (id, _) =>
			{
				var movieGrain = GrainFactory.GetGrain<IMovieGrain>(id);
				_moviesCache[id] = await movieGrain.GetMovieDetailsAsync();
			}));
		}
	}
}