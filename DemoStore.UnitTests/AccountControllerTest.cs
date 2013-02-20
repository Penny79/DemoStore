using System.Web.Mvc;
using DemoStore.WebUI.Controllers;
using DemoStore.WebUI.Infrastructure.Abstract;
using DemoStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void LogOn_WhenCalled_ShouldDisplayLogOnView()
        {
            // Arrange
            var mockAuth = new Mock<IAuthProvider>();
            var controller = new AccountController(mockAuth.Object);

            // Act
            var result = controller.LogOn();

            // Assert
            Assert.AreEqual("", result.ViewName, "Should load the default view.");
        }

        [TestMethod]
        public void LogOn_WhenUsernameOrPasswordNotCoorect_ShouldReloadView()
        {
            // Arrange
            var mockAuth = new Mock<IAuthProvider>();
            var controller = new AccountController(mockAuth.Object);
            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = controller.LogOn(new LogOnViewModel(), "return");

            // Assert
            Assert.AreEqual("", ((ViewResult)result).ViewName, "Should load default view.");
        }

        [TestMethod]
        public void LogOn_WhenUserIsAuthenticaed_ShouldLogin()
        {
            // Arrange
            var viewModel = new LogOnViewModel()
            {
                Username = "UName", Password = "PWord"
            };
            var mockAuth = new Mock<IAuthProvider>();
            mockAuth.Setup(auth => auth.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new AccountController(mockAuth.Object);
            

            // Act
            var result = controller.LogOn(viewModel, "Url");

            // Assert
            mockAuth.Verify(auth => auth.Authenticate(It.Is<string>(un => un.Equals(viewModel.Username)), It.Is<string>(pw => pw.Equals(viewModel.Password))));
            Assert.AreEqual("Url", ((RedirectResult)result).Url, "Should login to Admin List");
        }

        [TestMethod]
        public void LogOn_WhenCannotBeAuthenticated_ShouldLoadLogOnView()
        {
            // Arrange
            var viewModel = new LogOnViewModel()
            {
                Username = "UName",
                Password = "PWord"
            };
            var mockAuth = new Mock<IAuthProvider>();
            mockAuth.Setup(auth => auth.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new AccountController(mockAuth.Object);

            // Act
            var result = controller.LogOn(viewModel, null);

            // Assert
            mockAuth.Verify(auth => auth.Authenticate(It.Is<string>(un => un.Equals(viewModel.Username)), It.Is<string>(pw => pw.Equals(viewModel.Password))));
            Assert.AreEqual("", ((ViewResult)result).ViewName, "Should load default view.");
            Assert.IsFalse(controller.ModelState.IsValid, "Model should be invalid.");
        }
    }
}
