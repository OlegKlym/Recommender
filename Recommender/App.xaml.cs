using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using FreshMvvm;
using Xamarin.Forms;
using Recommender.API.Interfaces;
using Recommender.API.Services;
using Recommender.Core.Helpers;
using Recommender.Core.Models;
using Recommender.Core.Services;
using Recommender.Core.UseCases;
using Recommender.Helpers;
using Recommender.PageModels;
using Recommender.Resources.Localizations;
using Recommender.Services;
using Recommender.UseCases;

using IServiceProvider = Recommender.Core.Services.IServiceProvider;
using Recommender.API;

namespace Recommender
{
    public partial class App : Application
    {
        public static TelemetryClient TelemetryClient { get; private set; }

        public App()
        {
            InitializeComponent();
            InitializeApplicationInsights();

            SetDefaultLanguage();

            RegisterContainers();
            RegisterServiceProvider();
            RegisterExceptionHandlers();

            var pageModel = FreshPageModelResolver.ResolvePageModel<OnboardingPageModel>();
            MainPage = new FreshNavigationContainer(pageModel);
        }

        protected override void OnStart()
        {
            MessagingCenter.Send(this, "AppStart");
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send(this, "AppSleep");
        }

        protected override void OnResume()
        {
            MessagingCenter.Send(this, "AppResume");
        }

        // Обробник неконтрольованих винятків
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            FreshIOC.Container.Resolve<ILoggingService>().Error(exception, exception.Message);
        }

        // Обробник неконтрольованих завдань
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            FreshIOC.Container.Resolve<ILoggingService>().Error(e.Exception, e.Exception.Message);

            e.SetObserved();
        }

        private void SetDefaultLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk");
            AppResources.Culture = new CultureInfo("uk");
        }

        private void RegisterContainers()
        {
            FreshIOC.Container.Register<ILoggingService, LoggingService>();
            FreshIOC.Container.Register<IAuthService, AuthService>();
            FreshIOC.Container.Register<IUsersService, UsersService>();
            FreshIOC.Container.Register<IMoviesService, MoviesService>();
            FreshIOC.Container.Register<ILocalStorageService, LocalStorageService>();
            FreshIOC.Container.Register<INavigationService, NavigationService>();
            FreshIOC.Container.Register<IDialogService, DialogService>();

            FreshIOC.Container.Register<ILoginUseCase, LoginUseCase>();

            FreshIOC.Container.Register<IValidator<LoginPageModel>, LoginValidator>();
            FreshIOC.Container.Register<ILoaderDecorator, LoaderDecorator>();

            ApiContainerConfig.ConfigureDependencies();
        }

        private void RegisterServiceProvider()
        {
            var serviceProvider = new ServiceProvider();
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<ILoggingService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IAuthService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IUsersService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IMoviesService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<ILocalStorageService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<INavigationService>());
            serviceProvider.RegisterService(FreshIOC.Container.Resolve<IDialogService>());

            FreshIOC.Container.Register<IServiceProvider>(serviceProvider);
        }

        private void RegisterExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
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
