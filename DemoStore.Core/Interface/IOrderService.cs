using System;
using DemoStore.Core.Entities;

namespace DemoStore.Core.Interface
{
    /// <summary>
    /// Encapsulates the order related logic
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Saves the given cart as a purchase order to the underlying storage
        /// </summary>
        /// <param name="cart">the cart object</param>
        /// <returns>the order tracking number</returns>
        string SubmitOrder(Cart cart);

        /// <summary>
        /// retrieves the cart object from the underlying storage
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Cart GetCart(Guid cartId);

    }
}
