using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Recommender.Core.Services;
using Recommender.Services;
using Recommender.ViewModels;

namespace Recommender.PageModels
{
    public class OnboardingPageModel : BasePageModel
    {
        private readonly INavigationService _navigationService;

        public string IntroVideoUrl { get; set; } = "ms-appx:///video1597151024.mp4";

        public ICommand ContinueCommand { get; }

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