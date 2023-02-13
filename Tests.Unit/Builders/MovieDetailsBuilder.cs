using Movies.Contracts;
using System;
using System.Collections.Generic;

namespace Tests.Unit.Builders
{
	public class MovieDetailsBuilder
	{
		string _key;
		string _name;
		string _description;
		IList<string> _genres;
		string _rate;
		string _length;
		string _img;

		public static MovieDetailsBuilder InMemory => new MovieDetailsBuilder();

		public MovieDetailsBuilder WithKey(string key)
		{
			_key = key;
			return this;
		}

		public MovieDetailsBuilder WithDefaults()
		{
			_key = "american-gangster";
			_name = "American Gangster";
			_description = "Random description";
			_genres = new List<string>
			{
				"crime",
				"drama"
			};
			_rate = "7.8";
			_length = "2hr 37mins";
			_img = "american-gangster.jpg";

			return this;
		}

		public MovieDetails Build()
		{
			var movie = new MovieDetails
			{
				Id = new Random().Next(),
				Key = _key,
				Name = _name,
				Description = _description,
				Genres = _genres,
				Rate = _rate,
				Length = _length,
				Img = _img
			};

			return movie;
		}
	}


}
