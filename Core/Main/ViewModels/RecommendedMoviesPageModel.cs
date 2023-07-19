using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using NLog;
using Acr.UserDialogs;
using PropertyChanged;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models;
using Recommender.Core.Services;

using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender.ViewModels
{
    //TODO: add caching
    //TODO: add filters for genre and rating
    //TODO: cover with unit tests
    //TODO: test with extreme values
    [AddINotifyPropertyChangedInterface]
    public class RecommendedMoviesPageModel : BasePageModel
    {
        private readonly IMoviesService _moviesService;
        private readonly ILocalStorageService _localStorageService;
        private readonly IUsersService _usersService;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _timeoutDurationInSeconds = 30;

        public ObservableCollection<MovieModel> Recommendations { get; set; }

        public Command<MovieModel> OpenMovieDetailsCommand { get; }
        public Command RefreshMoviesCommand { get; }

        public RecommendedMoviesPageModel(IServiceProvider serviceProvider)
        {
            _moviesService = serviceProvider.GetService<IMoviesService>();
            _localStorageService = serviceProvider.GetService<ILocalStorageService>();
            _usersService = serviceProvider.GetService<IUsersService>();

            OpenMovieDetailsCommand = new Command<MovieModel>(async movie => await ExecuteOpenMovieDetailsCommand(movie));
            RefreshMoviesCommand = new Command(async() => await ExecuteRefreshMoviesCommand());
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Recommendations == null)
            {
                Task.Run(() => LoadRecommendationsAsync());
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);

            CancelRecommendationsLoading();
        }

        private async Task ExecuteRefreshMoviesCommand()
        {
            await CoreMethods.PushPageModel<RateMoviesPageModel>();
        }

        private async Task ExecuteOpenMovieDetailsCommand(MovieModel movie)
        {
            await CoreMethods.PushPageModel<MovieDetailsPageModel>(movie);
        }

        private async Task LoadRecommendationsAsync()
        {
            //var response = await _localStorageService.GetNotSeenMoviesFromLocalStorageAsync();

            //// check if there already cached movies
            //if (response.Data == null)
            //{

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.ShowLoading("Очікуйте");
                });

                var userData = await _usersService.GetUserFromStorageAsync();
                if (userData == null)
                {
                    UserDialogs.Instance.HideLoading();

                    await UserDialogs.Instance.AlertAsync("Помилка. Користувач не зареєстрований");
                    return;
                }

                await LoadRecommendationsForUserAsync(userData);
            }

            catch (OperationCanceledException)
            {
                _logger.Error("APP_ERROR: Завантаження рекомендацій було скасовано.");
            }

            catch (TimeoutException)
            {
                _logger.Error("APP_ERROR: Завантаження рекомендацій зайняло забагато часу.");

                await UserDialogs.Instance.AlertAsync("Завантаження рекомендацій зайняло забагато часу.");
            }

            catch (Exception ex)
            {
                _logger.Error($"APP_ERROR: Помилка завантаження рекомендацій: {ex.Message}");

                await UserDialogs.Instance.AlertAsync("Не вдалося отримати рекомендації. Спробуй трохи пізніше");
            }
          
            //}
            //else
            //{
            //    Recommendations = new ObservableCollection<MovieModel>(response.Data);
            //}

            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task LoadRecommendationsForUserAsync(UserModel userData)
        {
            var notSeenMovies = await ExecuteWithTimeoutAsync(ct => GetNotSeenMoviesAsync(userData, ct), _cancellationTokenSource.Token);

            Recommendations = new ObservableCollection<MovieModel>(notSeenMovies);
        }

        private async Task<IEnumerable<MovieModel>> GetNotSeenMoviesAsync(UserModel userData, CancellationToken cancellationToken)
        {
            var request = new GetRecommendationsRequest { UserId = userData.Id };

            var response = await _moviesService.GetRecommendationsAsync(request, cancellationToken);
            if (response.IsSuccessful)
            {
                var recommendedMovies = response.Data?.RecommendedMovies?.Take(20)?.ToList() ?? new List<MovieModel>();

                await _localStorageService.SaveNotSeenMoviesToLocalStorageAsync(recommendedMovies);

                return recommendedMovies;
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Не вдалося отримати рекомендації. Спробуй трохи пізніше");
                throw new Exception(response.ErrorMessage);
            }
        }

        private void CancelRecommendationsLoading()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task<T> ExecuteWithTimeoutAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken)
        {
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(_timeoutDurationInSeconds), cancellationToken);
            var operationTask = operation(cancellationToken);

            var completedTask = await Task.WhenAny(timeoutTask, operationTask);
            if (completedTask == timeoutTask)
            {
                _cancellationTokenSource?.Cancel();
                throw new TimeoutException();
            }

            return await operationTask;
        }
    }
}