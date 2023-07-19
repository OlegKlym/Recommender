using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;

namespace Recommender.API.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IApiRepository _apiRepository;

        public MoviesService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public Task<IResponseData<GetRecommendationsResponse>> GetRecommendationsAsync(GetRecommendationsRequest request, CancellationToken? cancellationToken)
        {
            return _apiRepository.MakePostRequest<GetRecommendationsResponse>(request, Constants.GET_RECOMMENDATIONS_URL, cancellationToken);
        }

        public Task<IResponseData<RateMoviesResponse>> RateMoviesAsync(RateMoviesRequest request)
        {
            return _apiRepository.MakePostRequest<RateMoviesResponse>(request, Constants.RATE_MOVIES_URL);
        }

        public Task<IResponseData<IEnumerable<MovieModel>>> SearchMoviesAsync(SearchMoviesRequest request)
        {
            return _apiRepository.MakePostRequest<IEnumerable<MovieModel>>(request, Constants.SEARCH_MOVIES_URL);
        }
    }
}
