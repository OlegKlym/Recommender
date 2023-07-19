using System.Threading.Tasks;
using FreshMvvm;
using Moq;
using NUnit.Framework;
using Recommender.Core.Helpers;
using Recommender.Core.Models.Responses;
using Recommender.Core.UseCases;
using Recommender.Helpers;
using Recommender.PageModels;
using Recommender.Resources.Localizations;
using Recommender.ViewModels;

namespace Recommender.UnitTests.ViewModels
{
    [TestFixture()]
    public class LoginPageModelTests : BasePageModelTests<LoginPageModel>
    {
        protected Mock<ILoginUseCase> _loginUseCaseMock;
        protected Mock<ILoaderDecorator> _loaderDecoratorMock;
        protected Mock<IValidator<LoginPageModel>> _loginPageValidatorMock;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();

            _loginUseCaseMock = new Mock<ILoginUseCase>();
            _loaderDecoratorMock = new Mock<ILoaderDecorator>();
            _loginPageValidatorMock = new Mock<IValidator<LoginPageModel>>();

            _viewModel = new LoginPageModel(
                serviceProvider: _serviceProvider,
                loginUseCase: _loginUseCaseMock.Object,
                validator: _loginPageValidatorMock.Object);
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldLogInIfCredentialsAreCorrect()
        {
            // Arrange
            SetupMocksForCommandExecution(validationModelResult: true, isSuccessfulLogin: true);

            // Act;
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _loginUseCaseMock.Verify(x => x.LogInAsync(_viewModel.Email, _viewModel.Password), Times.Once);
            _navigationServiceMock.Verify(x => x.NavigateToPageModelAsync<RecommendationsViewModel>(false,true), Times.Once);
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldShowAPIErrorIfCredentialsAreWrong()
        {
            // Arrange
            SetupMocksForCommandExecution(validationModelResult: true, isSuccessfulLogin: false);

            // Act
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(
                navService => navService.NavigateToPageModelAsync<RecommendationsViewModel>(false,true),
                Times.Never);

            _dialogServiceMock.Verify(
                dialogService => dialogService.ShowAlertAsync(AppResources.LoginFailed, It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldNotNavigateIfEmailIsMissing()
        {
            // Arrange
            SetupMocksForCommandExecution(validationModelResult: false, isSuccessfulLogin: false);

            // Act
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(
                navService => navService.NavigateToPageModelAsync<RecommendationsViewModel>(false, true),
                Times.Never);
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldNotNavigateIfPasswordIsMissing()
        {
            // Arrange
            _viewModel.Email = It.IsAny<string>();

            SetupMocksForCommandExecution(validationModelResult: false, isSuccessfulLogin: false);

            // Act
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(
                navService => navService.NavigateToPageModelAsync<RecommendationsViewModel>(false, true),
                Times.Never);
        }

        [Test]
        public async Task ExecuteGoToSignUpCommand_ShouldNavigateToSignUpPageModel()
        {
            // Arrange

            // Act
            await Task.Run(() => _viewModel.GoToSignUpCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(navService =>
               navService.NavigateToPageModelAsync<SignUpPageModel>(false, true), Times.Once);
        }

        [Test]
        public async Task ExecuteGoToForgotPasswordCommand_ShouldNavigateToForgotPasswordPageModel()
        {
            // Arrange

            // Act
            await Task.Run(() => _viewModel.GoToForgotPasswordCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(navService =>
                navService.NavigateToPageModelAsync<ForgotPasswordPageModel>(false, true), Times.Once);
        }

        private void SetupMocksForCommandExecution(bool validationModelResult, bool isSuccessfulLogin)
        {
            FreshIOC.Container.Register(_loaderDecoratorMock.Object);

            _loginPageValidatorMock.Setup(v => v.ValidateModel(_viewModel)).Returns(validationModelResult);

            if (isSuccessfulLogin)
            {
                _loginUseCaseMock.Setup(x => x.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new LoginResult { IsSuccessful = true });
            }
            else
            {
                _loginUseCaseMock.Setup(useCase => useCase.LogInAsync(_viewModel.Email, _viewModel.Password))
                    .ReturnsAsync(new LoginResult { IsSuccessful = false });
            }
        }
    }
}
