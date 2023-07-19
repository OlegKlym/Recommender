using FreshMvvm;
using Recommender.API.Services;
using Recommender.Core.Services;

namespace Recommender.API
{
    public class ApiContainerConfig
    {
        public static void ConfigureDependencies()
        {
            var apiLoggingService = new ApiLoggingService(FreshIOC.Container.Resolve<ILoggingService>());
            FreshIOC.Container.Register(apiLoggingService);

            FreshIOC.Container.Register<IApiRepository, ApiRepository>();
        }
    }
}
