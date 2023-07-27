using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Recommender.BussinesLogic.Services;
using Recommender.Contracts.Models.Responses;
using Recommender.Contracts.UseCases;
using Recommender.Core.Services;

using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender.BussinesLogic.UseCases
{
    public class RecommendedMoviesUseCase : IRecommendedMoviesUseCase
    {
        private readonly IMoviesService _moviesService;
        private readonly IUsersService _usersService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ILoggingService _loggingService;
        private readonly IMovieRecommendationProvider _moviesProvider;

        public RecommendedMoviesUseCase(IServiceProvider serviceProvider, IMovieRecommendationProvider moviesProvider)
        {
            _moviesService = serviceProvider.GetService<IMoviesService>();
            _usersService = serviceProvider.GetService<IUsersService>();
            _localStorageService = serviceProvider.GetService<ILocalStorageService>();
            _loggingService = serviceProvider.GetService<ILoggingService>();

            _moviesProvider = moviesProvider;
        }

        public async Task<RecommendedMoviesResult> GetRecommendationsAsync(CancellationToken? cancellationToken = null)
        {
            try
            {
                var userData = await _usersService.GetUserFromStorageAsync();
                if (userData == null)
                {
                    return HandleError("Помилка. Користувач не зареєстрований");
                }

                var recommendedMovies = await _moviesProvider.GetRecommendationsForUserAsync(userData, 20, cancellationToken);
                if (recommendedMovies == null || !recommendedMovies.Any())
                {
                    return HandleError("Не вдалося отримати рекомендації.Спробуй трохи пізніше");
                }

                await _localStorageService.SaveMoviesToLocalStorageAsync(recommendedMovies);

                return new RecommendedMoviesResult
                {
                    IsSuccessful = true,
                    RecommendedMovies = recommendedMovies
                };
            }

            catch (OperationCanceledException)
            {
                return HandleError("Завантаження рекомендацій було скасовано.");
            }

            catch (TimeoutException)
            {
                return HandleError("Завантаження рекомендацій зайняло забагато часу.");
            }

            catch (Exception ex)
            {
                return HandleError($"Помилка завантаження рекомендацій: {ex.Message}");
            }
        }

        private RecommendedMoviesResult HandleError(string errorMessage)
        {
            _loggingService.Error($"APP_ERROR: {errorMessage}");

            return new RecommendedMoviesResult
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
