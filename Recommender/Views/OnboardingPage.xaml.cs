using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnboardingPage : BaseViewPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage(), false);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            video.Stop();
        }
    }
}