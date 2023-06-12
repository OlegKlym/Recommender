using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Recommender.ViewModels;
using FreshMvvm;

namespace Recommender.UnitTests.ViewModels
{
    [TestFixture()]
    public class OnboardingPageModelTests : BasePageModelTests<OnboardingPageModel>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }

        [Test]
        public async Task ExecuteContinueCommand_ShouldNavigateToLoginPageModel()
        {
            // Arrange
            BasePageModel loginPageModel = null;

            _navigationServiceMock
                .Setup(x => x.NavigateToPageModelAsync<LoginPageModel>(It.IsAny<bool>(), It.IsAny<bool>()))
                .Callback(() =>
                {
                    loginPageModel = new LoginPageModel(_serviceProvider)
                    {
                        CoreMethods = _navigationServiceMock.Object as IPageModelCoreMethods
                    };
                })
                .Returns(Task.CompletedTask);

            _navigationServiceMock.Setup(x => x.CurrentPageModel).Returns(() => loginPageModel);

            // Act
            await Task.Run(() => _viewModel.ContinueCommand.Execute(null));

            // Assert
            _navigationServiceMock.Verify(x => x.NavigateToPageModelAsync<LoginPageModel>(false, true), Times.Once);

            Assert.AreNotEqual(_viewModel, _navigationServiceMock.Object.CurrentPageModel);
            Assert.That(_navigationServiceMock.Object.CurrentPageModel, Is.InstanceOf<LoginPageModel>());
        }
    }
}
