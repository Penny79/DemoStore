using System.Linq;
using System.Collections.Generic;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;
using DemoStore.WebUI.Controllers;
using DemoStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            var mockCatalogService = MockFactory.CreateCatalogServiceMock();

            var controller = new ProductController(mockCatalogService.Object);
            controller.PageSize = 3;

            // Act
            var result = (ProductListViewModel)controller.List(null, 2).Model;

            //
            Assert.IsTrue(result.Products.Count() == 2, "There should only be 2 items in the list.");
            Assert.AreEqual(result.Products.First().Name, "P4", "The first item should be P1");
            Assert.AreEqual(result.Products.Last().Name, "P5", "Last item in the list should be P2");
        }

        [TestMethod]
        public void CanSend_PagingInfo_ToView()
        {
            // Arrange
            var mockCatalogService = MockFactory.CreateCatalogServiceMock();
            var controller = new ProductController(mockCatalogService.Object) {PageSize = 3};

            // Act
            var result = (ProductListViewModel)controller.List(null, 2).Model;

            // Assert
            Assert.AreEqual(2, result.PagingInfo.CurrentPage, "Should show the second page");
            Assert.AreEqual(3, result.PagingInfo.ItemsPerPage, "Should display only 4 items per page");
            Assert.AreEqual(5, result.PagingInfo.TotalItems, "Should only contain 5 items");
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            var mockCatalogService = MockFactory.CreateCatalogServiceMock();
            var controller = new ProductController(mockCatalogService.Object) {PageSize = 2};

            // Act
            var result = controller.List("C2", 1).Model as ProductListViewModel;

            // Assert
            Assert.AreEqual("C2", result.CurrentCategory, "Should use the expected category");
            Assert.IsTrue(result.Products.All(prod => prod.Category.Equals("C2")), "All the items should be of the same category");
            Assert.AreEqual(2, result.PagingInfo.ItemsPerPage, "Should pass the correct number of page size");
            Assert.AreEqual(3, result.PagingInfo.TotalItems, "Should calculate the number of items per category.");
            Assert.AreEqual(2, result.PagingInfo.TotalPages, "There should be two pages.");
            Assert.AreEqual(1, result.PagingInfo.CurrentPage, "The first page should be displayed");
            Assert.AreEqual(2, result.Products.First().ProductID, "The first product id should match.");
            Assert.AreEqual(3, result.Products.Last().ProductID, "The first product id should match.");
        }
    }
}
