using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.WebUI.Infrastructure.Abstract;
using Moq;

namespace DemoStore.UnitTests
{
    internal class MockFactory
    {
        public static Mock<ICatalogService> CreateCatalogServiceMock()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { Name = "P1", ProductID = 1 },
                new Product() { Name = "P2", ProductID = 2 },
                new Product() { Name = "P3", ProductID = 3 },
                new Product() { Name = "P4", ProductID = 4 },
                new Product() { Name = "P5", ProductID = 5 },
            };

            return CreateCatalogServiceMock(products);
        }

        public static Mock<ICatalogService> CreateCatalogServiceMock(List<Product> products)
        {
           
            var mockCatalogService = new Mock<ICatalogService>();
            mockCatalogService.Setup(svc => svc.GetAllProducts()).Returns(products.AsQueryable());
            return mockCatalogService;
        }

        public static Mock<IOrderService> CreateOrderServiceMock(Cart cart)
        {
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(svc => svc.GetCart(It.IsAny<Guid>())).Returns(cart);
            return mockOrderService;
        }



        public static Mock<ISessionState> CreateSessionStateMock(Cart cart)
        {
            var cartId = Guid.NewGuid();

            var sessionState = new Mock<ISessionState>();
            sessionState.Setup(state => state.Get(It.IsAny<string>())).Returns(cartId);
            return sessionState;
        }


    }
}
