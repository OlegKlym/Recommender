using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Recommender.Core.Models;
using Recommender.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateMoviesPage : StyledPage
    {
        public List<MovieModel> Movies { get; set; }

        private int _currentItemIndex = 0;

        public RateMoviesPage()
        {
            InitializeComponent();
        }

        private void ImageButton_OnClicked(object sender, EventArgs e)
        {
            if (_currentItemIndex < Movies.Count)
            {
                carousel.ScrollTo(Movies[_currentItemIndex], position: ScrollToPosition.Center, animate: true);
            }
        }

        private void Carousel_OnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            _currentItemIndex = e.CenterItemIndex;
        }

        private void Carousel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == ItemsView.ItemsSourceProperty.PropertyName &&
                sender is CollectionView collectionView &&
                collectionView.ItemsSource != null)
            {
                Movies = (collectionView.ItemsSource as IEnumerable<MovieModel>).ToList();
            }
        }

        private void ImageButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}