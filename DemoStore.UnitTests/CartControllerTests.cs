using System.Collections.Generic;
using System.Linq;
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
    public class CartControllerTests
    {
        [TestMethod]
        public void Index_WhenCalled_ShouldPoulateViewWithGivenCart()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 1 }, 1);
            
            var mockCatalogService = new Mock<ICatalogService>();
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);
            var controller = new CartController(mockCatalogService.Object, null, sessionStateMock.Object);

            // Act
            var result = controller.Index("Return1").Model as CartIndexViewModel;

            // Assert
            Assert.AreSame(cart, result.Cart, "Should use the given cart item.");
            Assert.AreEqual("Return1", result.ReturnUrl, "Should use the given return url.");
        }

        [TestMethod]
        public void AddToCart_WhenNoProducFoundInCart_ShouldAddToCart()
        {
            // Arrange
            var cart = new Cart();
            var mockCatalogService = new Mock<ICatalogService>();
            mockCatalogService.Setup(svc => svc.GetAllProducts()).Returns((new List<Product>() { new Product() { ProductID = 1 } }).AsQueryable());

            var orderServiceMock = MockFactory.CreateOrderServiceMock(cart);
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);

            var controller = new CartController(mockCatalogService.Object, orderServiceMock.Object, sessionStateMock.Object);

            // Act
            var result = controller.AddToCart(1, "Return1");

            // Assert
            Assert.AreEqual(1, cart.Lines.Count(), "Should have added the item to repository.");
            Assert.AreEqual(1, cart.Lines.First().Product.ProductID, "Should have added the given product id.");
            Assert.AreEqual("Index", result.RouteValues["action"].ToString(), "Should direct to the Index action.");
            Assert.AreEqual("Return1", result.RouteValues["returnUrl"].ToString(), "Should direct to the Index action.");
        }

        [TestMethod]
        public void RemoveFromCart_WhenProductIsGive_ShouldRemoveFromCart()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 1 }, 1);
            var mockCatalogService = new Mock<ICatalogService>();
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);
            var orderServiceMock = MockFactory.CreateOrderServiceMock(cart);
            var controller = new CartController(mockCatalogService.Object, orderServiceMock.Object,sessionStateMock.Object);

            // Act
            var result = controller.RemoveFromCart(1, "Return2");

            // Assert
            Assert.AreEqual(0, cart.Lines.Count(), "Should have removed from the cart.");
            Assert.AreEqual("Index", result.RouteValues["action"], "Should redirect to the the Index action.");
            Assert.AreEqual("Return2", result.RouteValues["returnUrl"], "Should used the given Url");
        }

        [TestMethod]
        public void Checkout_Cannot_Checkout_EmptyCart()
        {
            // Arrange
            var cart = new Cart();

            var orderServiceMock = MockFactory.CreateOrderServiceMock(cart);
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);
            var controller = new CartController(null, orderServiceMock.Object, sessionStateMock.Object);

            // Act
            var result = controller.Checkout(cart);

            // Assert
            Assert.IsFalse(controller.ModelState.IsValid, "Should be in the invalid state.");

            // Verify that process order has not be called.
            orderServiceMock.Verify(processor => processor.SubmitOrder(It.IsAny<Cart>()), Times.Never());

            Assert.AreEqual("", result.ViewName, "Should redirect to the same Checkout view.");
        }

        [TestMethod]
        public void Checkout_WhenCartContainsItems_ButShippingDetailsAreInValid_ShouldNotCheckout()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 10 }, 1);
           

            var mockProcessor = new Mock<IOrderService>();
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);

            var controller = new CartController(null, mockProcessor.Object, sessionStateMock.Object);
            controller.ModelState.AddModelError("test", "shipping error");

            // Act
            var result = controller.Checkout(cart);

            // Assert
            mockProcessor.Verify(processor => processor.SubmitOrder(It.IsAny<Cart>()), Times.Never(), "Should have never processed the order");
            Assert.AreEqual("", result.ViewName, "Should redirect to the same checkoutview");
        }

        [TestMethod]
        public void Checkout_WhenValidCartAndShippingDetailsArePresent_ShouldPlaceOrder()
        {
            // Arrange                       
            var cart = new Cart()
                {
                    BillingAddress =
                        new CartAddress
                            {
                                Name = "M",
                                Line1 = "Line1",
                                City = "City",
                                Country = "Country",
                                State = "State",
                                Zip = "Zip"
                            }
                };
            cart.AddItem(new Product() { ProductID = 10 }, 1);
            var mockProcessor = new Mock<IOrderService>();
            var sessionStateMock = MockFactory.CreateSessionStateMock(cart);
            var controller = new CartController(null, mockProcessor.Object, sessionStateMock.Object);

            // Act
            var result = controller.Checkout(cart);

            // Assert
            mockProcessor.Verify(process => process.SubmitOrder(cart), Times.Once(), "Should process the order.");
            Assert.AreEqual("Completed", result.ViewName, "Should go to the appropriate Completed View.");
            Assert.IsTrue(controller.ModelState.IsValid, "Model should be valid.");
        }
    }
}
