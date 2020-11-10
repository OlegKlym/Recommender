using System.Collections.Generic;
using Recommender.Models;
using Recommender.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecommendationsPage : BaseViewPage
    {
        public RecommendationsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var movies = DataService.GetMovies();

            collection.ItemsSource = movies;
        }
    }
}