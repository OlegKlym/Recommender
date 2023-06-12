using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Recommender.API.Interfaces;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Mocks.Services;

namespace Recommender.API.Services
{
    public class MoviesService : BaseMocksService, IMoviesService
    {
        public async Task<IResponseData<GetRecommendationsResponse>> GetRecommendationsAsync(GetRecommendationsRequest request, CancellationToken? cancellationToken)
        {
            return await Task.FromResult(new ResponseData<GetRecommendationsResponse>
            {
                Data = new GetRecommendationsResponse
                {
                    RecommendedMovies = new List<MovieModel>()
                }
            });
        }

        public async Task<IResponseData<RateMoviesResponse>> RateMoviesAsync(RateMoviesRequest request)
        {
            return await Task.FromResult(new ResponseData<RateMoviesResponse>
            {
                Data = new RateMoviesResponse
                {
                    UserId = request.UserId,
                    Movies = request.Movies,
                    Rate = request.Rate
                }
            });
        }

        public async Task<IResponseData<IEnumerable<MovieModel>>> SearchMoviesAsync(SearchMoviesRequest request)
        {
            return await Task.FromResult(new ResponseData<IEnumerable<MovieModel>>
            {
                Data = new List<MovieModel>()
            });
        }
    }
}
