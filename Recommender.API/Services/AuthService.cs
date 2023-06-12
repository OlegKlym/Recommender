using System.Net.Http;
using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;

namespace Recommender.API.Services
{
    public class AuthService : BaseApiService, IAuthService
    {
        private readonly IUsersService _usersService;

        public AuthService(HttpClient httpClient,
            IUsersService usersService) : base(httpClient)
        {
            _usersService = usersService;
        }
        
        public async Task<IResponseData<LogInResponse>> LogInAsync(LogInRequest request)
        {
            var response = await MakeRequest<LogInResponse>(request, Constants.AUTH_LOGIN_URL);

            await _usersService.SetUserToStorageAsync(response.Data);

            return response;
        }

        public async Task<IResponseData<SignUpResponse>> SignUpAsync(SignUpRequest request)
        {
            var response = await MakeRequest<SignUpResponse>(request, Constants.AUTH_SIGNUP_URL);

            await _usersService.SetUserToStorageAsync(response.Data);

            return response;
        }
    }
}
