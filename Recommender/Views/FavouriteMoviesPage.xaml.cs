using System.Threading.Tasks;
using Recommender.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace Recommender.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteMoviesPage : BaseViewPage
    {
        public FavouriteMoviesPage()
        {
            InitializeComponent();
            
            Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }

        private async void UpdateProgress()
        {
            var selectedMoviesCount = SelectedMoviesList.Children.Count;

            UpdateProgressBarColor(selectedMoviesCount);

            UpdateProgressBarValue();

            await CheckIfRateCompleteAsync(selectedMoviesCount);
        }

        private void UpdateProgressBarValue()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Task.Delay(700).ContinueWith(async x => await ProgressBar.ProgressTo(ProgressBar.Progress, 500, Easing.Linear));
            });
        }

        private void UpdateProgressBarColor(int moviesCount)
        {
            if (moviesCount <= 3)
            {
                ProgressBar.ProgressColor = Color.FromHex("#E01D1D");
            }
            else if (moviesCount <= 7)
            {
                ProgressBar.ProgressColor = Color.FromHex("#D9F52C");
            }
            else if (moviesCount <= 10)
            {
                ProgressBar.ProgressColor = Color.FromHex("#2CF564");
            }
        }

        private async Task CheckIfRateCompleteAsync(int moviesCount)
        {
            if (moviesCount == 10)
            {
                await ScrollView.ScrollToAsync(ConfirmButton, ScrollToPosition.End, true);
            }
        }
    }
}