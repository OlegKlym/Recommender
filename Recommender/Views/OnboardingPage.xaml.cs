using Recommender.Views;
using Xamarin.Forms.Xaml;

namespace Recommender.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnboardingPage : BaseViewPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            introVideo.Stop();
        }
    }
}