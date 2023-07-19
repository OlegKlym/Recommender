using System.Threading.Tasks;
using Recommender.Core.Models.Responses;

namespace Recommender.Core.UseCases
{
    public interface ILoginUseCase
    {
        Task<LoginResult> LogInAsync(string email, string password);
    }
}
