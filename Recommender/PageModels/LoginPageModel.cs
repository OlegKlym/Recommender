using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using PropertyChanged;
using Recommender.Core.UseCases;
using Recommender.Core.Services;
using Recommender.Core.Helpers;
using Recommender.Helpers;
using Recommender.Services;
using Recommender.Resources.Localizations;
using Recommender.ViewModels;

using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginPageModel : BasePageModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private readonly ILoginUseCase _loginUseCase;
        private readonly IValidator<LoginPageModel> _validator;

        public string Email { get; set; } = "marta93y@gmail.com";
        public string Password { get; set; } = "26101993";

        public ICommand ContinueCommand { get; }
        public ICommand GoToSignUpCommand { get; }
        public ICommand GoToForgotPasswordCommand { get; }

        public LoginPageModel(
            IServiceProvider serviceProvider,
            ILoginUseCase loginUseCase,
            IValidator<LoginPageModel> validator)
        {
            _loginUseCase = loginUseCase;
            _validator = validator;

            _navigationService = serviceProvider.GetService<INavigationService>();
            _dialogService = serviceProvider.GetService<IDialogService>();

            ContinueCommand = new Command(async () => await ExecuteContinueCommand().WithLoaderAsync());
            GoToSignUpCommand = new Command(async () => await ExecuteGoToSignUpCommand());
            GoToForgotPasswordCommand = new Command(async () => await ExecuteGoToForgotPasswordCommand());
        }

        private Task ExecuteGoToForgotPasswordCommand()
        {
            return _navigationService.NavigateToPageModelAsync<ForgotPasswordPageModel>();
        }

        private Task ExecuteGoToSignUpCommand()
        {
            return _navigationService.NavigateToPageModelAsync<SignUpPageModel>();
        }

        private Task ExecuteContinueCommand()
        {
            if(_validator.ValidateModel(this))
            {
                return LoginAsync();
            }

            return Task.CompletedTask;
        }

        private async Task LoginAsync()
        {
            var response = await _loginUseCase.LogInAsync(Email, Password);
            if (response.IsSuccessful)
            {
                await _navigationService.NavigateToPageModelAsync<RecommendedMoviesPageModel>();
            }
            else
            {
                await _dialogService.ShowAlertAsync(AppResources.LoginFailed);
            }
        }
    }
}