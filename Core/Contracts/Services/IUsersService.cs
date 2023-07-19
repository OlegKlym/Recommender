using System.Threading.Tasks;
using Recommender.Core.Models;

namespace Recommender.Core.Services
{
    public interface IUsersService
    {
        Task<UserModel> GetUserFromStorageAsync();
        Task UpdateUserToStorageAsync(UserModel user);
        Task SetUserToStorageAsync(UserModel user);
        Task DeleteUserFromStorage();
    }
}
