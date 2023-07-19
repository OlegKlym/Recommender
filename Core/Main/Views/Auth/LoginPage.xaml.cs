using Recommender.Views;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace Recommender.Pages
{
    public partial class LoginPage : StyledPage
    {
        public LoginPage()
        {
            InitializeComponent();

            Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
    }
}
