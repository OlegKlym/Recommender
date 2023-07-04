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
        protected Mock<IDialogService> _dialogServiceMock;
        protected Mock<IAuthService> _authServiceMock;
        protected IServiceProvider _serviceProvider;


        protected virtual void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _authServiceMock = new Mock<IAuthService>();
            _serviceProvider = new ServiceProvider();

            _serviceProvider.RegisterService(_navigationServiceMock.Object);
            _serviceProvider.RegisterService(_dialogServiceMock.Object);
            _serviceProvider.RegisterService(_authServiceMock.Object);
        }
    }
}
