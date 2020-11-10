using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace Recommender.Views
{
    public partial class LoginPage : BaseViewPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
            Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Device.BeginInvokeOnMainThread(() =>
            // {
            //     Task.Delay(700).ContinueWith(x => LoginGroup.FadeTo(1, 1000)
            //                    .ContinueWith(l => Title.FadeTo(1, 1000)));
            // });
        }

        private void Button_OnClicked(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new FavouriteMoviesPage(), false);
        }
    }
}
