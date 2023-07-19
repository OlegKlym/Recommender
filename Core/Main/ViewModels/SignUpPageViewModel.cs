using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PropertyChanged;
using Xamarin.Forms;
using Recommender.Core.Enums;
using Recommender.Core.Models.Requests;
using Recommender.Core.Services;
using Recommender.ViewModels;

namespace Recommender.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class SignUpPageModel : BasePageModel
    {
        private readonly IAuthService _authService;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public List<string> Genders { get; }

        public Command ContinueCommand { get; }

        public SignUpPageModel(IAuthService authService)
        {
            _authService = authService;

            Genders = Enum.GetNames(typeof(Gender)).ToList();

            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
        }

        private async Task ExecuteContinueCommand()
        {
            UserDialogs.Instance.ShowLoading("Очікуйте");

            if (string.IsNullOrWhiteSpace(FirstName) ||
               string.IsNullOrWhiteSpace(LastName) ||
               string.IsNullOrWhiteSpace(Email) ||
               string.IsNullOrWhiteSpace(Password))
            {
                await UserDialogs.Instance.AlertAsync("Дозаповни поля реєстрації. Будь ласка!");
            }
            else
            {
                var request = new SignUpRequest
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    Gender = Gender == "Male" ? Core.Enums.Gender.Male : Core.Enums.Gender.Female,
                    BirthDate = BirthDate
                };

                var response = await _authService.SignUpAsync(request);
                if (response.IsSuccessful)
                {
                    await CoreMethods.PushPageModel<RecommendedMoviesPageModel>();
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Не вдалося увійти. Перевір данні!");
                }

                await CoreMethods.PushPageModel<FavouriteMoviesPageModel>();
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}
