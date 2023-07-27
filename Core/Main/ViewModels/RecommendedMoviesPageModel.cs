using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Acr.UserDialogs;
using PropertyChanged;
using Recommender.Core.Models;
using Recommender.Helpers;
using Recommender.Contracts.UseCases;

namespace Recommender.ViewModels
{
    //TODO: add caching
    //TODO: add filters for genre and rating
    //TODO: cover with unit tests
    //TODO: test with extreme values
    [AddINotifyPropertyChangedInterface]
    public class RecommendedMoviesPageModel : BasePageModel
    {
        private readonly IRecommendedMoviesUseCase _recommendedMoviesUseCase;

        private CancellationTokenSource _cancellationTokenSource;

        private bool _isRecommendationsLoading = false;

        public ObservableCollection<MovieModel> Recommendations { get; set; }

        public Command<MovieModel> OpenMovieDetailsCommand { get; }
        public Command RefreshMoviesCommand { get; }

        public RecommendedMoviesPageModel(IRecommendedMoviesUseCase recommendedMoviesUseCase)
        {
            _recommendedMoviesUseCase = recommendedMoviesUseCase;

            OpenMovieDetailsCommand = new Command<MovieModel>(async movie => await ExecuteOpenMovieDetailsCommand(movie));
            RefreshMoviesCommand = new Command(async() => await ExecuteRefreshMoviesCommand());
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (!_isRecommendationsLoading)
            {
                _isRecommendationsLoading = true;
                _ = LoadRecommendationsAsyncWithLoader();
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

        private async Task LoadRecommendationsAsyncWithLoader()
        {
            try
            {
                await Task.Run(() => LoadRecommendationsAsync().WithLoaderAsync());
            }
            finally
            {
                _isRecommendationsLoading = false;
            }
        }

        private async Task LoadRecommendationsAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var response = await _recommendedMoviesUseCase.GetRecommendationsAsync(_cancellationTokenSource.Token);
            if (response.IsSuccessful)
            {
                Recommendations = new ObservableCollection<MovieModel>(response.RecommendedMovies);
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(response.ErrorMessage);
            }
        }

        private void CancelRecommendationsLoading()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}