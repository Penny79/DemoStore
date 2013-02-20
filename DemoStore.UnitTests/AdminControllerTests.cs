using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface.DataAccess;
using DemoStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Index_ShouldReturn_AllProducts()
        {
            // Arrange
            var prod1 = new Product { ProductID = 1 };
            var prod2 = new Product { ProductID = 1 };

            var products = new List<Product>()
            {
                prod1, 
                prod2
            };

            var mockCatalogService = MockFactory.CreateCatalogServiceMock(products);
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Index().Model as IEnumerable<Product>;

            // Assert
            Assert.AreEqual(2, result.Count(), "There should be 2 Products.");
            Assert.AreSame(prod1, result.First(), "Should be the same item.");
            Assert.AreSame(prod2, result.Last(), "Should be the same item.");
        }

        [TestMethod]
        public void Edit_WhenTheItemIsFound_ShouldTransferToView()
        {
            // Arrange
            var prod1 = new Product() { ProductID = 1, Name = "A" };
            var products = new List<Product>()
            {
                prod1,
                new Product() { ProductID = 2, Name = "b" },
            };

            var mockCatalogService = MockFactory.CreateCatalogServiceMock(products);
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.AreSame(prod1, (Product)result.Model, "View should receive the same product.");
        }

        [TestMethod]
        public void Edit_WhenProductNotFound_ViewShouldReceiveNull()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { ProductID = 1, Name = "A" },
                new Product() { ProductID = 2, Name = "b" },
            };

            var mockCatalogService = MockFactory.CreateCatalogServiceMock(products);
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Edit(11);

            // Assert
            Assert.IsNull(result.ViewData.Model, "When item not found in products Model should be null.");
        }

        [TestMethod]
        public void Save_WhenProductEditedSuccessfully_ShouldSaveProduct()
        {
            // Arrange
            var product = new Product() { ProductID = 1, Name = "M", Category = "C", Price = 1.1m, Description = "D" };
            var mockCatalogService = MockFactory.CreateCatalogServiceMock(new List<Product>() {product});
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Edit(product, null);

            // Assert
            Assert.IsNotNull(controller.TempData["message"], "Message should be populated.");
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"], "Should redirect to Index");
            mockCatalogService.Verify(prod => prod.SaveProduct(It.Is<Product>(p => p.GetHashCode() == product.GetHashCode())));
        }

        [TestMethod]
        public void Save_WhenProductIsNotCorrectlyPopulated_ShouldNotCallRepository()
        {
            // Arrange
            var prod = new Product();
            var mockCatalogService = MockFactory.CreateCatalogServiceMock(new List<Product>() { prod });
            var controller = new AdminController(mockCatalogService.Object);

            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = controller.Edit(prod, null);

            // Assert
            mockCatalogService.Verify(p => p.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.AreSame(prod, ((Product)((ViewResult)result).Model), "Should contain the same product object.");
        }

        [TestMethod]
        public void Create_WhenCalled_ProductConentShouldBeEmpty()
        {
            // Arrange
            var mockCatalogService = MockFactory.CreateCatalogServiceMock();
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Create();
            var model = (Product)result.Model;

            // Assert
            Assert.AreEqual(0, model.ProductID, "Product id should be 0");
            Assert.IsNull(model.Name, "Should not have been initialised.");
            Assert.IsNull(model.Category, "Should not have been initialised.");
            Assert.IsNull(model.Description, "Should not have been initialised.");
            Assert.AreEqual(0m, model.Price, "Product id should be 0");
            Assert.AreEqual("Edit", result.ViewName, "Should load edit view.");
        }

        [TestMethod]
        public void Delete_WhenProductFoundInRepository_ShouldRemoveIt()
        {
            // Arrange
            var prod1 = new Product() { ProductID = 1 };
            var prod2 = new Product() { ProductID = 1 };

            var mockCatalogService = MockFactory.CreateCatalogServiceMock(new List<Product>() {prod1, prod2});
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"], "Should redirect to the correct index.");
            Assert.IsNotNull(controller.TempData["message"], "There should be a message.");
            mockCatalogService.Verify(rep => rep.DeleteProduct(It.Is<Product>(p => p.GetHashCode() == prod1.GetHashCode())));
        }

        [TestMethod]
        public void Delete_WhenProductNotFound_ShouldNotCallDeleteProduct()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { ProductID = 1 }
            };

            var mockCatalogService = MockFactory.CreateCatalogServiceMock(products);
            var controller = new AdminController(mockCatalogService.Object);

            // Act
            var result = controller.Delete(10);

            // Assert
            mockCatalogService.Verify(repo => repo.DeleteProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsNull(controller.TempData["message"], "There should not be any message.");
        }
    }
}
