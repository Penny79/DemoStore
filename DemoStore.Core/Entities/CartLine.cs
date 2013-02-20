namespace DemoStore.Core.Entities
{
    /// <summary>
    /// This class represents a shopping cart item in the <see cref="Cart"/>.
    /// </summary>
    public class CartLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
