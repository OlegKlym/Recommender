using Recommender.Views;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace Recommender.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecommendedMoviesPage : StyledPage
    {
        public RecommendedMoviesPage()
        {
            InitializeComponent();

            Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
    }
}