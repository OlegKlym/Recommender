using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;

namespace Recommender.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiRepository _apiRepository;

        public AuthService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }
        
        public Task<IResponseData<LogInResponse>> LogInAsync(LogInRequest request)
        {
            return _apiRepository.MakePostRequest<LogInResponse>(request, Constants.AUTH_LOGIN_URL);
        }

        public Task<IResponseData<SignUpResponse>> SignUpAsync(SignUpRequest request)
        {
            return _apiRepository.MakePostRequest<SignUpResponse>(request, Constants.AUTH_SIGNUP_URL);
        }
    }
}
