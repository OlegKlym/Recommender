using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Services;

namespace Recommender.BussinesLogic.Services
{
    public interface IMovieRecommendationProvider
    {
        Task<IEnumerable<MovieModel>> GetRecommendationsForUserAsync(UserModel user, int moviesCount, CancellationToken? cancellationToken = null);
    }

    public class MovieRecommendationProvider : IMovieRecommendationProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMoviesService _moviesService;

        public MovieRecommendationProvider(ILocalStorageService localStorageService, IMoviesService moviesService)
        {
            _localStorageService = localStorageService;
            _moviesService = moviesService;
        }

        public async Task<IEnumerable<MovieModel>> GetRecommendationsForUserAsync(UserModel user, int moviesCount, CancellationToken? cancellationToken = null)
        {
            var localStorageResponse = await _localStorageService.GetMoviesFromLocalStorageAsync();
            if (localStorageResponse.Data != null && localStorageResponse.Data.Any())
            {
                return localStorageResponse.Data;
            }

            var request = new GetRecommendationsRequest { UserId = user.Id };
            var response = await _moviesService.GetRecommendationsAsync(request, cancellationToken);

            if (response.IsSuccessful)
            {
                return response.Data?.RecommendedMovies?.Take(moviesCount)?.ToList() ?? new List<MovieModel>();
            }

            return Enumerable.Empty<MovieModel>();
        }
    }
}
