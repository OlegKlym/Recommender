using System.Threading.Tasks;
using Xamarin.Forms;
using FreshMvvm;
using Recommender.ViewModels;
using Recommender.Core.Services;

namespace Recommender.Services
{
    public interface INavigationService
    {
        BasePageModel CurrentPageModel { get; }

        Task NavigateToPageModelAsync<T>(bool isModalPage = false, bool isAnimated = true) where T : BasePageModel;

        Task NavigateBackAsync(bool isModalPage = false, bool isAnimated = true);
    }

    public class NavigationService : INavigationService
    {
        private readonly ILoggingService _loggingService;

        public NavigationService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public BasePageModel CurrentPageModel { get; private set; }

        public async Task NavigateToPageModelAsync<T>(bool isModalPage = false, bool isAnimated = true) where T : BasePageModel
        {
            var pageModel = FreshIOC.Container.Resolve<T>();
            var page = FreshPageModelResolver.ResolvePageModel<T>(pageModel);
            FreshPageModelResolver.BindingPageModel(null, page, pageModel);

            CurrentPageModel = pageModel;

            var rootNavigation = GetRootNavigationService();
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await rootNavigation.PushPage(page, pageModel, isModalPage, isAnimated);
            });

            CurrentPageModel = pageModel;

            _loggingService.Info($"[AppNavigation] Navigated to {page.GetType().Name}");
        }

        public async Task NavigateBackAsync(bool isModalPage = false, bool isAnimated = true)
        {
            var rootNavigation = GetRootNavigationService();

            if (CurrentPageModel.IsModalFirstChild)
            {
                HandleModalFirstChild();
            }
            else if (isModalPage)
            {
                CurrentPageModel.RaisePageWasPopped();
            }

            await rootNavigation.PopPage(isModalPage, isAnimated);

            _loggingService.Info($"Navigated back to {CurrentPageModel.CurrentPage.GetType().Name}");
        }

        private void HandleModalFirstChild()
        {
            var currentNavigationService = GetRootNavigationService();

            currentNavigationService.NotifyChildrenPageWasPopped();

            FreshIOC.Container.Unregister<IFreshNavigationService>(currentNavigationService.NavigationServiceName);
        }

        private IFreshNavigationService GetRootNavigationService()
        {
            var navServiceName = CurrentPageModel.CurrentNavigationServiceName;
            return FreshIOC.Container.Resolve<IFreshNavigationService>(navServiceName);
        }
    }
}
