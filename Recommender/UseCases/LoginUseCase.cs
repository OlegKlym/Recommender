using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;
using Recommender.Core.UseCases;
using Recommender.Mappers;

namespace Recommender.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;
        private readonly ILoggingService _loggingService;

        public LoginUseCase(IServiceProvider serviceProvider)
        {
            _authService = serviceProvider.GetService<IAuthService>();
            _usersService = serviceProvider.GetService<IUsersService>();
            _loggingService = serviceProvider.GetService<ILoggingService>();
        }

        public async Task<LoginResult> LogInAsync(string email, string password)
        {
            var request = new LogInRequest
            {
                Email = email,
                Password = password
            };

            var response = await _authService.LogInAsync(request);
            if(response.IsSuccessful)
            {
                await _usersService.SetUserToStorageAsync(response.Data.MapToUserModel());
            }
            else
            {
                _loggingService.Error($"APP_ERROR: {response.ErrorMessage}");
            }

            return new LoginResult { IsSuccessful = response.IsSuccessful };
        }
    }
}
