using System;
using System.Collections.Concurrent;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.DataAccess.InMemory
{
    public class CartRepository : ICartRepository
    {
        private static readonly ConcurrentDictionary<Guid, Cart> Storage = new ConcurrentDictionary<Guid, Cart>();

        public Cart GetCart(Guid cartId)
        {
            return Storage.GetOrAdd(cartId, new Cart() {CartId = cartId});
        }
    }
}
