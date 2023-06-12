using System.Globalization;
using System.Net.Http;
using System.Threading;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using FreshMvvm;
using Recommender.API.Interfaces;
using Recommender.API.Services;
using Recommender.Core.Models;
using Recommender.Core.Services;
using Recommender.Resources.Localizations;
using Recommender.Services;
using Recommender.ViewModels;
using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender
{
    public partial class App :  Xamarin.Forms.Application
    {
        public static TelemetryClient TelemetryClient { get; private set; }

        public App()
        {
            InitializeComponent();
            InitializeApplicationInsights();

            SetDefaultLanguage();

            RegisterContainers();
            RegisterServiceProvider();

            var pageModel = FreshPageModelResolver.ResolvePageModel<OnboardingPageModel>();
            MainPage = new FreshNavigationContainer(pageModel);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        private void SetDefaultLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk");
            AppResources.Culture = new CultureInfo("uk");
        }

        private void RegisterContainers()
        {
            FreshIOC.Container.Register(new HttpClient());
            FreshIOC.Container.Register<IAuthService, AuthService>();
            FreshIOC.Container.Register<IUsersService, UsersService>();
            FreshIOC.Container.Register<IMoviesService, MoviesService>();
            FreshIOC.Container.Register<ILocalStorageService, LocalStorageService>();
            FreshIOC.Container.Register<INavigationService, NavigationService>();
        }

        private void RegisterServiceProvider()
        {
            var serviceProvider = new ServiceProvider();
            var httpClient = new HttpClient();

            serviceProvider.RegisterService(httpClient);
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IAuthService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IUsersService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IMoviesService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<ILocalStorageService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<INavigationService>());

            FreshIOC.Container.Register<IServiceProvider>(serviceProvider);
        }

        private void InitializeApplicationInsights()
        {
            var configuration = TelemetryConfiguration.CreateDefault();

            // TODO: set telemetric
            //configuration.ConnectionString = "YOUR_INSTRUMENTATION_KEY";

            TelemetryClient = new TelemetryClient(configuration);
        }
    }
}
