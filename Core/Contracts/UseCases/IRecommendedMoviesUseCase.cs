using System.Threading;
using System.Threading.Tasks;
using Recommender.Contracts.Models.Responses;

namespace Recommender.Contracts.UseCases
{
    public interface IRecommendedMoviesUseCase
    {
        Task<RecommendedMoviesResult> GetRecommendationsAsync(CancellationToken? cancellationToken = null);
    }
}
