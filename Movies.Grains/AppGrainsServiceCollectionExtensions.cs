﻿// ReSharper disable once CheckNamespace
using Movies.GrainInterfaces;
using Movies.Grains;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class AppGrainsServiceCollectionExtensions
	{

		public static IServiceCollection AddAppGrains(this IServiceCollection services)
		{
			return services;
		}

		public static IServiceCollection AddAppHotsGrains(this IServiceCollection services)
		{
			return services;
		}

		public static IServiceCollection AddAppLoLGrains(this IServiceCollection services)
		{
			return services;
		}
	}
}
