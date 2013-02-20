using System.Collections.Generic;
using System.Linq;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;
using DemoStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { Category = "C1" },
                new Product() { Category = "C1" },
                new Product() { Category = "C2" },
                new Product() { Category = "C2" },
                new Product() { Category = "C2" },
            };

            var mockCatalogService = new Mock<ICatalogService>();
            mockCatalogService.Setup(svc => svc.GetAllProducts()).Returns(products.AsQueryable());

            var controller = new NavController(mockCatalogService.Object);

            // Act
            var result = controller.Menu().Model as IEnumerable<string>;

            // Assert
            Assert.AreEqual(2, result.Count(), "There has to be two categories");
            Assert.AreEqual("C1", result.First(), "Should contain the expected category");
            Assert.AreEqual("C2", result.Last(), "Should contain the expected category");
        }

    }
}
