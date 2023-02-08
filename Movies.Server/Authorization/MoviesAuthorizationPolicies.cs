namespace Movies.Server.Authorization
{
	public static class MoviesAuthorizationPolicies
	{
		public const string RetrieveScope = "movies:retrieve";
		public const string UploadScope = "movies:upload";
		public const string MoviesGlobalScope = "movies";

		public const string RetrievePolicy = nameof(RetrievePolicy);
		public const string UploadPolicy = nameof(UploadPolicy);
	}
}
