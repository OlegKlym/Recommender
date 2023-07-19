using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using PropertyChanged;
using Recommender.Core.Enums;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Services;
using Xamarin.Forms;

namespace Recommender.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class FavouriteMoviesPageModel : FreshBasePageModel
    {
        private readonly IMoviesService _moviesService;
        private readonly IUsersService _usersService;

        public List<MovieModel> SearchedMovies { get; set; }
        public ObservableCollection<MovieModel> FavouriteMovies { get; set; }

        public int AddedMoviesCount { get; set; }
        public string SearchTerm { get; set; }

        [DependsOn(nameof(AddedMoviesCount))]
        public bool IsContinueAvailable => AddedMoviesCount == 10;
        [DependsOn(nameof(AddedMoviesCount))]
        public bool IsSearchBarAvailable => AddedMoviesCount < 10;
        [DependsOn(nameof(AddedMoviesCount))]
        public double ProgressBarPercentage => AddedMoviesCount * 0.1;
        [DependsOn(nameof(SearchedMovies))]
        public bool AreSuggestionsVisible => SearchedMovies != null && SearchedMovies.Any();

        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromFavouritesCommand { get; }
        public ICommand ContinueCommand { get; }

        public FavouriteMoviesPageModel(IMoviesService moviesService,
            IUsersService usersService)
        {
            _moviesService = moviesService;
            _usersService = usersService;

            FavouriteMovies = new ObservableCollection<MovieModel>();

            SearchCommand = new Command(async() => await ExecuteSearchCommand());
            ClearSearchCommand = new Command(ExecuteClearSearchCommand);
            AddToFavouritesCommand = new Command<MovieModel>(ExecuteAddToFavouritesCommand);
            RemoveFromFavouritesCommand = new Command<MovieModel>(ExecuteRemoveFromFavouritesCommand);
            ContinueCommand = new Command(async() => await ExecuteContinueCommand(), () => IsContinueAvailable);
        }

        private void ExecuteAddToFavouritesCommand(MovieModel movie)
        {
            AddedMoviesCount++;
            FavouriteMovies.Add(movie);

            ClearSearchData();
        }

        private void ExecuteRemoveFromFavouritesCommand(MovieModel movie)
        {
            AddedMoviesCount--;
            FavouriteMovies.Remove(movie);
        }

        private async Task ExecuteSearchCommand()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm)) return;

            UserDialogs.Instance.ShowLoading("Очікуйте");

            var request = new SearchMoviesRequest { SearchTerm = SearchTerm };

            var response = await _moviesService.SearchMoviesAsync(request);
            if (response.IsSuccessful)
            {
                var selectedMoviesIds = FavouriteMovies.Select(x => x.Id);
                SearchedMovies = response.Data.Where(x => !selectedMoviesIds.Contains(x.Id))
                                              .OrderByDescending(x => x.Popularity)
                                              .Take(3).ToList();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Не вдалося отримати фільми. Спробуй трохи пізніше");
            }

            UserDialogs.Instance.HideLoading();
        }

        private void ExecuteClearSearchCommand()
        {
            ClearSearchData();
        }

        private void ClearSearchData()
        {
            SearchTerm = string.Empty;
            SearchedMovies = new List<MovieModel>();
        }

        private async Task ExecuteContinueCommand()
        {
            var userData = await _usersService.GetUserFromStorageAsync();
            if (userData == null) return;

            UserDialogs.Instance.ShowLoading("Очікуйте");

            var request = new RateMoviesRequest
            {
                UserId = userData.Id,
                Movies = FavouriteMovies.Select(x => x.Id),
                Rate = Rate.Like
            };

            var response = await _moviesService.RateMoviesAsync(request);
            if (response.IsSuccessful)
            {
                await CoreMethods.PushPageModel<RateMoviesPageModel>();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Не вдалося оцінити фільми. Спробуй трохи пізніше");
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}