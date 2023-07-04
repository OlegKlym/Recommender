using System;
using System.Threading.Tasks;
using Recommender.Core.Enums;
using Recommender.Core.Models;
using Recommender.Core.Models.Requests;
using Recommender.Core.Models.Responses;
using Recommender.Core.Services;

namespace Recommender.Mocks.Services
{
    public class AuthService : BaseMocksService, IAuthService
    {
        public async Task<IResponseData<LogInResponse>> LogInAsync(LogInRequest request)
        {
            return await Task.FromResult(new ResponseData<LogInResponse>
            {
                Data = new LogInResponse
                {
                    Id = 1,
                    FullName = "Marta Paienska",
                    Email = "marta93y@gmail.com",
                    ImageUrl = string.Empty,
                    Gender = (int)Gender.Female,
                    BirthDate = new DateTime(1993,10,26)
                }
            });
        }

        public async Task<IResponseData<SignUpResponse>> SignUpAsync(SignUpRequest request)
        {
            return await Task.FromResult(new ResponseData<SignUpResponse>
            {
                Data = new SignUpResponse
                {
                    Id = 1,
                    FullName = "Marta Paienska",
                    Email = "marta93y@gmail.com",
                    ImageUrl = string.Empty,
                    Gender = (int)Gender.Female,
                    BirthDate = new DateTime(1993, 10, 26)
                }
            });
        }
    }
}
