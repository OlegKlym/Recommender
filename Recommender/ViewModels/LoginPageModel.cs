using System.Threading.Tasks;
using Acr.UserDialogs;
using Recommender.Core.Models;
using Recommender.Core.Services;
using Xamarin.Forms;

namespace Recommender.ViewModels
{
    public class LoginPageModel : BasePageModel
    {
        private readonly IAuthService _authService;

        public string Email { get; set; } = "marta93y@gmail.com";
        public string Password { get; set; } = "26101993";

        public Command ContinueCommand { get; }
        public Command GoToSignUpCommand { get; }
        public Command GoToForgotPasswordCommand { get; }

        public LoginPageModel(IServiceProvider serviceProvider)
        {
            _authService = serviceProvider.GetService<IAuthService>();

            ContinueCommand = new Command(async() => await ExecuteContinueCommand());
            GoToSignUpCommand = new Command(ExecuteGoToSignUpCommand);
            GoToForgotPasswordCommand = new Command(ExecuteGoToForgotPasswordCommand);
        }

        private void ExecuteGoToForgotPasswordCommand()
        {
            //TODO: add ForgotPassword page
        }

        private void ExecuteGoToSignUpCommand()
        {
            CoreMethods.PushPageModel<FavouriteMoviesPageModel>();
        }

        private async Task ExecuteContinueCommand()
        {
            UserDialogs.Instance.ShowLoading("Очікуйте");

            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Email))
            {
                await UserDialogs.Instance.AlertAsync("Дозаповни поля реєстрації. Будь ласка!");
            }
            else
            {
                var request = new LogInRequest
                {
                    Email = Email,
                    Password = Password
                };

                var response = await _authService.LogInAsync(request);
                if (response.IsSuccessful)
                {
                    await CoreMethods.PushPageModel<RecommendationsViewModel>();
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Не вдалося увійти. Перевір данні!");
                }
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}
