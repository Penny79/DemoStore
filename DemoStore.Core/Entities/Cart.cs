using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoStore.Core.Entities
{
    /// <summary>
    ///     This class represents the shopping cart in the Sports Store.
    /// </summary>
    public class Cart
    {
        #region private fields

        private readonly CartAddress billingAddress = new CartAddress();
        private readonly List<CartLine> items = new List<CartLine>();
        private readonly CartAddress shippingAddress = new CartAddress();

        #endregion

        #region public properties

        public Guid CartId
        {
            get; set;
        }

        public IEnumerable<CartLine> Lines
        {
            get { return items; }
        }

        public bool GiftWrap { get; set; }

        public CartAddress BillingAddress
        {
            get;
            set;
        }

        public CartAddress ShippingAddress
        {
            get; set;
        }

        #endregion


        #region public methods


        public void AddItem(Product product, int quantity)
        {
            lock (this)
            {
                CartLine storedItem = items
                    .Where(prod => prod.Product.ProductID == product.ProductID)
                    .FirstOrDefault();

                if (storedItem == null)
                {
                    items.Add(new CartLine {Product = product, Quantity = quantity});
                }
                else
                {
                    storedItem.Quantity += quantity;
                }
            }
        }

        public void RemoveItem(Product product)
        {
            lock (this)
            {
                items.RemoveAll(cartLine => cartLine.Product.ProductID == product.ProductID);
            }
        }

        public decimal ComputeTotalValue()
        {
            lock (this)
            {
                return items.Sum(cartLine => cartLine.Product.Price*cartLine.Quantity);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                items.Clear();
            }
        }

        #endregion
    }
}