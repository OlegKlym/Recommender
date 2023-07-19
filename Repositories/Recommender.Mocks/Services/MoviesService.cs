using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Recommender.Core.Enums;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;

namespace Recommender.Mocks.Services
{
    public class MoviesService : BaseMocksService, IMoviesService
    {
        public async Task<IResponseData<GetRecommendationsResponse>> GetRecommendationsAsync(GetRecommendationsRequest request, CancellationToken? cancellationToken)
        {
            return await Task.FromResult(new ResponseData<GetRecommendationsResponse>
            {
                Data = new GetRecommendationsResponse
                {
                    RecommendedMovies = new List<MovieModel>
                    {
                        new MovieModel
                        {
                            Title = "Movie 1",
                            Year = 2021,
                            MainGenre = Genre.Action,
                            AdditionalGenre = Genre.Thriller,
                            Popularity = 100,
                            AverageRating = 8.5,
                            Image = "https://example.com/movie1.jpg",
                        },
                        new MovieModel
                        {
                             Title = "Movie 2",
                             Year = 2022,
                             MainGenre = Genre.Drama,
                             AdditionalGenre = Genre.Romance,
                             Popularity = 80,
                             AverageRating = 7.8,
                             Image = "https://example.com/movie2.jpg",
                        }
                    }
                },
                StatusCode = 200
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
