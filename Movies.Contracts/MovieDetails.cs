using Orleans.Concurrency;
using System.Collections.Generic;

namespace Movies.Contracts
{
	[Immutable]
	public class MovieDetails
	{
		public long Id { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<string> Genres { get; set; }
		public string Rate { get; set; }
		public string Length { get; set; }
		public string Img { get; set; }
	}
}
