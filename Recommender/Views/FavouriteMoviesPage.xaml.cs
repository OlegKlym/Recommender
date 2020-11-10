using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Recommender.Models;
using Recommender.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using ImageButton = Xamarin.Forms.ImageButton;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteMoviesPage : BaseViewPage
    {
        private readonly ObservableCollection<MovieModel> _selectedMovies;

        public FavouriteMoviesPage()
        {
            InitializeComponent();
            
            _selectedMovies = new ObservableCollection<MovieModel>();

            var movies = DataService.GetMovies();

            UpdateProgress();
            
            BindableLayout.SetItemsSource(SelectedMoviesList, _selectedMovies);
            BindableLayout.SetItemsSource(SuggestionsList, movies.Take(3));
            
            Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SuggectionsComponent.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private void SearchBar_OnUnfocused(object sender, FocusEventArgs e)
        {
            //ClearSearchBar();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (sender is Grid grid && grid.BindingContext is MovieModel movie)
            {
                _selectedMovies.Add(movie);

                ClearSearchBar();
                UpdateProgress();
            }
        }
        
        private void ImageButton_OnClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is MovieModel movie)
            {
                _selectedMovies.Remove(movie);

                UpdateProgress();
            }
        }
        
        private void UpdateProgress()
        {
            var progressPercentage = _selectedMovies.Count * 0.1;
            ProgressLabel.Text = _selectedMovies.Count.ToString();
            SearchBar.IsEnabled = _selectedMovies.Count < 10;

            UpdateProgressBarColor();
            
            Device.BeginInvokeOnMainThread(() =>
            {
                Task.Delay(700).ContinueWith(async x => await ProgressBar.ProgressTo(progressPercentage, 500, Easing.Linear));
            });

            CheckIfRateCompleteAsync();
        }

        private void UpdateProgressBarColor()
        {
            if (_selectedMovies.Count <= 3)
            {
                ProgressBar.ProgressColor = Color.FromHex("#E01D1D");
            }
            else if (_selectedMovies.Count <= 7)
            {
                ProgressBar.ProgressColor = Color.FromHex("#D9F52C");
            }
            else if (_selectedMovies.Count <= 10)
            {
                ProgressBar.ProgressColor = Color.FromHex("#2CF564");
            }
        }

        private void ClearSearchBar()
        {
            SuggectionsComponent.IsVisible = false;
            SearchBar.Text = string.Empty;
        }
        
        private async Task CheckIfRateCompleteAsync()
        {
            if (_selectedMovies.Count == 10)
            {
                ConfirmButton.IsEnabled = true;
                await ScrollView.ScrollToAsync(ConfirmButton, ScrollToPosition.End, true);
            }
        }

        private void ConfirmButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesTabbedPage(), false);
        }
    }
}