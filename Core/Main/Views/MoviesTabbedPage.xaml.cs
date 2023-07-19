using Recommender.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesTabbedPage : TabbedPage
    {
        public MoviesTabbedPage()
        {
            InitializeComponent();
            
            Children.Add(new NavigationPage(new RecommendedMoviesPage())
            {
                Title = "Рекомендації",
                IconImageSource = "recommendations.png"
            });
            
            Children.Add(new NavigationPage(new RateMoviesPage())
            {
                Title = "Оцінка",
                IconImageSource = "cards.png"
            });
            
            Children.Add(new NavigationPage(new ProfilePage())
            {
                Title = "Профіль",
                IconImageSource = "profile.png"
            });
        }

        // protected override bool OnBackButtonPressed()
        // {
        //     return true;
        // }
    }
}