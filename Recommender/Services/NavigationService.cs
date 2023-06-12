using System.Threading.Tasks;
using FreshMvvm;
using Recommender.ViewModels;

namespace Recommender.Services
{
    public class NavigationService : INavigationService
    {
        public BasePageModel CurrentPageModel { get; private set; }

        public async Task NavigateToPageModelAsync<T>(bool isModalPage = false, bool isAnimated = true) where T : BasePageModel
        {
            var pageModel = FreshIOC.Container.Resolve<T>();
            var page = FreshPageModelResolver.ResolvePageModel<T>(pageModel);

            FreshPageModelResolver.BindingPageModel(null, page, pageModel);

            var rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>(pageModel.CurrentNavigationServiceName);

            await rootNavigation.PushPage(page, pageModel, isModalPage, isAnimated);

            CurrentPageModel = pageModel;
        }

        public async Task NavigateBackAsync(bool isModalPage = false, bool isAnimated = true)
        {
            var navServiceName = CurrentPageModel.CurrentNavigationServiceName;
            var rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>(navServiceName);

            if (CurrentPageModel.IsModalFirstChild)
            {
                var currentNavigationService = FreshIOC.Container
                    .Resolve<IFreshNavigationService>(CurrentPageModel.CurrentNavigationServiceName);

                currentNavigationService.NotifyChildrenPageWasPopped();

                FreshIOC.Container
                    .Unregister<IFreshNavigationService>(CurrentPageModel.CurrentNavigationServiceName);
            }
            else
            {
                if (isModalPage)
                {
                    CurrentPageModel.RaisePageWasPopped();
                }
            }

            await rootNavigation.PopPage(isModalPage, isAnimated);
        }
    }
}
