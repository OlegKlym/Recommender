using System.Threading.Tasks;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;

namespace Recommender.Core.Services
{
    public interface IAuthService
    {
        Task<IResponseData<LogInResponse>> LogInAsync(LogInRequest request);
        Task<IResponseData<SignUpResponse>> SignUpAsync(SignUpRequest request);
    }
}
