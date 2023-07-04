using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Recommender.PageModels;
using Recommender.Core.UseCases;
using Recommender.Core.Helpers;

namespace Recommender.UnitTests.ViewModels
{
    [TestFixture()]
    public class OnboardingPageModelTests : BasePageModelTests<OnboardingPageModel>
    {
        protected Mock<ILoginUseCase> _loginUseCaseMock;
        protected Mock<IValidator<LoginPageModel>> _loginPageValidatorMock;

        [SetUp]
        protected override void Setup()
        {
            base.Setup();

            _viewModel = new OnboardingPageModel(_serviceProvider);
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldNavigateToLoginPageModel()
        {
            // Arrange

            // Act
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(x => x.NavigateToPageModelAsync<LoginPageModel>(false, true), Times.Once);
        }
    }
}
