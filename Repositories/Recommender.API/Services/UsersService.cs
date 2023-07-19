using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Recommender.Core.Models;
using Recommender.Core.Services;

namespace Recommender.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly ILoggingService _loggingService;

        public UsersService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public Task<UserModel> GetUserFromStorageAsync()
        {
            try
            {
                return GetUserDataFromStorageAsync();
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message);

                return null;
            }
        }

        public async Task UpdateUserToStorageAsync(UserModel user)
        {
            try
            {
                var existingUser = await GetUserDataFromStorageAsync();
                if (existingUser != null)
                {

                    var updatedUser = new UserModel
                    {
                        Id = existingUser.Id,
                        Email = existingUser.Email,
                        FullName = existingUser.FullName,
                        ImageUrl = existingUser.ImageUrl,
                        Gender = existingUser.Gender,
                        BirthDate = existingUser.BirthDate
                    };

                    await StoreUserDataToStorageAsync(updatedUser);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message);
            }
        }

        public Task SetUserToStorageAsync(UserModel user)
        {
            try
            {
                return StoreUserDataToStorageAsync(user);
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message);

                return Task.CompletedTask;
            }
        }

        public Task DeleteUserFromStorage()
        {
            try
            {
                SecureStorage.Remove("userData");
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message);
            }

           return Task.CompletedTask;
        }

        private async Task<UserModel> GetUserDataFromStorageAsync()
        {
            var userData = await SecureStorage.GetAsync("userData");
            return JsonConvert.DeserializeObject<UserModel>(userData);
        }

        private async Task StoreUserDataToStorageAsync(UserModel user)
        {
            var userData = JsonConvert.SerializeObject(user);
            await SecureStorage.SetAsync("userData", userData);
        }
    }
}
