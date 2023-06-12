using System;
using System.Threading.Tasks;
using Recommender.Core.Enums;
using Recommender.Core.Models;
using Recommender.Core.Services;

namespace Recommender.Mocks.Services
{
    public class UsersService : IUsersService
    {
        public async Task<UserModel> GetUserFromStorageAsync()
        {
            return await Task.FromResult(new UserModel
            {
                Id = 1,
                FullName = "Marta Paienska",
                Email = "marta93y@gmail.com",
                ImageUrl = string.Empty,
                Gender = (int)Gender.Female,
                BirthDate = new DateTime(1993, 10, 26)
            });
        }

        public Task SetUserToStorageAsync(object user)
        {
            return Task.CompletedTask;
        }
    }
}
