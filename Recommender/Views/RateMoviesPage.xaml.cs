using System;
using System.Collections.Generic;
using System.Linq;
using Recommender.Models;
using Recommender.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateMoviesPage : BaseViewPage
    {
        private List<MovieModel> _movies;
        private ScrollEnum _scrollStatus  = (ScrollEnum)1;
        
        public RateMoviesPage()
        {
            InitializeComponent();

            ConstructCarousel();
            
            carousel.ItemsSource = _movies;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ScrollToBegin();
        }

        private void ConstructCarousel()
        {
            _movies = new List<MovieModel>{ new MovieModel()} ;
            _movies.AddRange(DataService.GetMovies());
            _movies.Add(new MovieModel());
        }
        
        private void Carousel_OnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (_scrollStatus == ScrollEnum.EndScrolling)
            {
                _scrollStatus = ScrollEnum.StartScrolling;
                
                if (_scrollStatus == ScrollEnum.StartScrolling)
                {
                    rates.FadeTo(1, 500, Easing.Linear);    
                }
            }
            else
            {
                if (_scrollStatus == ScrollEnum.StartScrolling)
                {
                    rates.FadeTo(0, 100, Easing.Linear);    
                }

                _scrollStatus++;
            }
        }

        private void ScrollToBegin()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    carousel.ScrollTo(_movies[1], position: ScrollToPosition.Center);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }

    public enum ScrollEnum
    {
        StartScrolling = 1,
        ContinueScrolling = 2,
        EndScrolling = 3
    }
}