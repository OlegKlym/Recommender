using System.Threading.Tasks;
using Xamarin.Forms;
using Recommender.Core.Services;
using Recommender.Services;

namespace Recommender.ViewModels
{
    public class OnboardingPageModel : BasePageModel
    {
        private INavigationService _navigationService;

        public string IntroVideoUrl { get; set; } = "ms-appx:///video1597151024.mp4";

        public Command ContinueCommand { get; }

        public OnboardingPageModel(IServiceProvider serviceProvider)
        {
            _navigationService = serviceProvider.GetService<INavigationService>();

            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
        }

        private Task ExecuteContinueCommand()
        {
            return _navigationService.NavigateToPageModelAsync<LoginPageModel>();
        }
    }
}