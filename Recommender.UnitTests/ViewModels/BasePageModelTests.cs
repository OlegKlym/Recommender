using System;
using FreshMvvm;
using Moq;
using Recommender.Core.Models;
using Recommender.Core.Services;
using Recommender.Services;
using Recommender.ViewModels;
using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender.UnitTests.ViewModels
{
    public class BasePageModelTests<TPageModel> where TPageModel : BasePageModel
    {
        protected TPageModel _viewModel;
        protected Mock<INavigationService> _navigationServiceMock;
        protected Mock<IAuthService> _authServiceMock;
        protected IServiceProvider _serviceProvider;

        protected void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _authServiceMock = new Mock<IAuthService>();
            _serviceProvider = new ServiceProvider();

            _serviceProvider.RegisterService(_navigationServiceMock.Object);
            _serviceProvider.RegisterService(_authServiceMock.Object);

            _viewModel = Activator.CreateInstance(typeof(TPageModel), _serviceProvider) as TPageModel;
            _viewModel.CoreMethods = _navigationServiceMock.Object as IPageModelCoreMethods;
        }
    }
}
