using Recommender.Views;
using Xamarin.Forms;

namespace Recommender
{
    public partial class App :  Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new OnboardingPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
