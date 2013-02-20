using System;
using System.Globalization;
using System.Runtime.Caching;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.Core.Concrete
{
    /// <summary>
    /// The core abstraction for all order related logic.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ICartRepository cartRepository;

        public OrderService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public string SubmitOrder(Cart cart)
        {
            return new Random(10000).Next().ToString(CultureInfo.InvariantCulture);
        }

        public Cart GetCart(Guid cartId)
        {
            return this.cartRepository.GetCart(cartId);
        }
    }
}
