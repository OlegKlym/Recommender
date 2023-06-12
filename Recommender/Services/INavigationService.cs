using System.Threading.Tasks;
using Recommender.ViewModels;

namespace Recommender.Services
{
    public interface INavigationService
    {
        BasePageModel CurrentPageModel { get; }

        Task NavigateToPageModelAsync<T>(bool modal = false, bool isAnimated = true) where T : BasePageModel;

        Task NavigateBackAsync(bool isModalPage = false, bool isAnimated = true);
    }
}
