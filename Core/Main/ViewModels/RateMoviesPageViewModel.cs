using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using Recommender.Core.Enums;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Services;
using Recommender.Services;
using Xamarin.Forms;

namespace Recommender.ViewModels
{
    public class RateMoviesPageModel : FreshBasePageModel
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMoviesService _moviesService;
        private readonly IUsersService _usersService;

        private UserModel _userData;

        private List<MovieModel> _likedMovies { get; set; }
        private List<MovieModel> _neutralMovies { get; set; }
        private List<MovieModel> _dislikedMovies { get; set; }
        private List<MovieModel> _notSeenMovies { get; set; }
        private List<MovieModel> _allRatedMovies { get; set; }

        public ObservableCollection<MovieModel> Recommendations { get; set; }

        public ICommand AddToLikedMovieCommand { get; set; }
        public ICommand AddToNeutralMovieCommand { get; set; }
        public ICommand AddToDislikedMovieCommand { get; set; }
        public ICommand AddToNotSeenMovieCommand { get; set; }

        public RateMoviesPageModel(
            ILocalStorageService localStorageService,
            IMoviesService moviesService,
           IUsersService userService)
        {
            _localStorageService = localStorageService;
            _moviesService = moviesService;
            _usersService = userService;

            _likedMovies = new List<MovieModel>();
            _neutralMovies = new List<MovieModel>();
            _dislikedMovies = new List<MovieModel>();
            _notSeenMovies = new List<MovieModel>();
            _allRatedMovies = new List<MovieModel>();

            AddToLikedMovieCommand = new Command<MovieModel>(ExecuteAddToLikedMovieCommand);
            AddToNeutralMovieCommand = new Command<MovieModel>(ExecuteAddToNeutralMovieCommand);
            AddToDislikedMovieCommand = new Command<MovieModel>(ExecuteAddToDislikedMovieCommand);
            AddToNotSeenMovieCommand = new Command<MovieModel>(ExecuteAddToNotSeenMovieCommand);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Recommendations == null)
            {
                Task.Run(async () => await LoadRecommendationsAsync());
            }
        }

        private void ExecuteAddToNotSeenMovieCommand(MovieModel movie)
        {
            _notSeenMovies.Add(movie);

            Task.Run(() => VerifyIfAllRated(movie));
        }

        private void ExecuteAddToDislikedMovieCommand(MovieModel movie)
        {
            _dislikedMovies.Add(movie);

            Task.Run(() => VerifyIfAllRated(movie));
        }

        private void ExecuteAddToNeutralMovieCommand(MovieModel movie)
        {
            _neutralMovies.Add(movie);

            Task.Run(() => VerifyIfAllRated(movie));
        }

        private void ExecuteAddToLikedMovieCommand(MovieModel movie)
        {
            _likedMovies.Add(movie);

            Task.Run(() => VerifyIfAllRated(movie));
        }

        private async Task LoadRecommendationsAsync()
        {
            _userData = await _usersService.GetUserFromStorageAsync();
            if (_userData == null) return;

            var request = new GetRecommendationsRequest { UserId = _userData.Id };

            UserDialogs.Instance.ShowLoading("Очікуйте");

            var response = await _moviesService.GetRecommendationsAsync(request, null);
            if (response.IsSuccessful)
            {
                var movies = response.Data.RecommendedMovies.Take(20);
                Recommendations = new ObservableCollection<MovieModel>(movies);
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Не вдалося отримати рекомендації. Спробуй трохи пізніше");
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task VerifyIfAllRated(MovieModel movie)
        {
            _allRatedMovies.Add(movie);

            if (_allRatedMovies.Count < 20) return;

            await RateAllMovies();
        }

        private async Task RateAllMovies()
        {
            UserDialogs.Instance.ShowLoading("Очікуйте");

            var tasks = new List<Task>
            {
                RateMoviesAsync(_likedMovies, Rate.Like),
                RateMoviesAsync(_neutralMovies, Rate.Neutral),
                RateMoviesAsync(_dislikedMovies, Rate.Dislike),

                _localStorageService.SaveMoviesToLocalStorageAsync(_notSeenMovies)
            };

            await Task.WhenAll(tasks);

            await CoreMethods.PushPageModel<RecommendedMoviesPageModel>();

            UserDialogs.Instance.HideLoading();
        }

        private async Task RateMoviesAsync(List<MovieModel> movies, Rate rate)
        {
            var request = new RateMoviesRequest
            {
                UserId = _userData.Id,
                Movies = movies.Select(x => x.Id),
                Rate = rate
            };

            await _moviesService.RateMoviesAsync(request);
        }
    }
}