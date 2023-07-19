using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;

namespace Recommender.Core.Services
{
    public interface IMoviesService
    {
        Task<IResponseData<GetRecommendationsResponse>> GetRecommendationsAsync(GetRecommendationsRequest request, CancellationToken? cancellationToken);
        Task<IResponseData<IEnumerable<MovieModel>>> SearchMoviesAsync(SearchMoviesRequest request);
        Task<IResponseData<RateMoviesResponse>> RateMoviesAsync(RateMoviesRequest request);
    }
}
