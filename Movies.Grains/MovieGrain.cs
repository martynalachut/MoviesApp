using Movies.Contracts;
using Movies.GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MovieGrain : Grain<MovieDetails>, IMovieGrain
	{
		public MovieGrain() { }

		public Task AddOrUpdateMovieAsync(MovieDetails movie) => UpdateStateAsync(movie);

		public Task<MovieDetails> GetMovieDetailsAsync() => Task.FromResult(State);

		private async Task UpdateStateAsync(MovieDetails movie)
		{
			if (State.Id == 0)
			{
				State.Id = movie.Id;
			}
			UpdateState(movie);

			await WriteStateAsync();

			var moviesStore = GrainFactory.GetGrain<IMoviesStoreGrain>(State.Id.ToString());
			await moviesStore.AddOrUpdateMovieAsync(movie);
		}

		private void UpdateState(MovieDetails movie)
		{
			State.Key = movie.Key;
			State.Name = movie.Name;
			State.Description = movie.Description;
			State.Genres = movie.Genres;
			State.Rate = movie.Rate;
			State.Length = movie.Length;
			State.Img = movie.Img;
		}
	}
}