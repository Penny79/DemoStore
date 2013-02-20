using System.Linq;
using DemoStore.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void AddItem_ShouldAddNewItem_ToCart()
        {
            // Arrange
            var cart = new Cart();

            // Assert
            Assert.AreEqual(0, cart.Lines.Count(), "When a new cart is created, there must not be any items.");

            // Act
            cart.AddItem(new Product() { ProductID = 1 }, 3);

            // Assert
            Assert.AreEqual(1, cart.Lines.Count(), "There should only be a single item in the cart.");
            Assert.AreEqual(3, cart.Lines.First().Quantity, "There should be 3 items in the cart.");
        }

        [TestMethod]
        public void AddItem_IncreaseQuantity_WhenExsistingItemAdded()
        {
            // Arrange
            var cart = new Cart();

            cart.AddItem(new Product() { ProductID = 2 }, 2);

            // Act
            cart.AddItem(new Product() { ProductID = 2 }, 10);

            // Assert
            Assert.AreEqual(1, cart.Lines.Count(), "There should only be a single item in the cart.");
            Assert.AreEqual(12, cart.Lines.First().Quantity, "The quantity of the items should be 12.");
        }

        [TestMethod]
        public void RemoveItem_ShouldRemove_TheGivenProduct()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 2 }, 1);

            // Act
            cart.RemoveItem(new Product() { ProductID = 2 });

            // Assert
            Assert.AreEqual(0, cart.Lines.Count(), "There should not be any item in the cart.");
        }

        [TestMethod]
        public void RemoveItem_WhenGivenItemNotFound_ShouldNotRemoveNonMatchingItem()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 2 }, 1);
            cart.AddItem(new Product() { ProductID = 3 }, 2);

            // Act
            cart.RemoveItem(new Product() { ProductID = 4 });

            // Assert
            Assert.AreEqual(2, cart.Lines.Count(), "There should be 2 items in the list.");
            Assert.AreEqual(1, cart.Lines.First().Quantity, "The quantity for ID 2 should have not changed.");
            Assert.AreEqual(2, cart.Lines.Last().Quantity, "The quantity for ID 3 should not have changed.");
        }

        [TestMethod]
        public void ComputeTotalValue_ShouldCaculate_Totals()
        {
            // Arrange
            var cart = new Cart();
            cart.AddItem(new Product() { ProductID = 2, Price = 10 }, 3);
            cart.AddItem(new Product() { ProductID = 3, Price = 5 }, 2);

            // Act
            var total = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(40, total, "The expected total should be 40.");
        }

        [TestMethod]
        public void Lines_ShouldContainAddedItems()
        {
            // Arrange
            var cart = new Cart();
            var prod1 = new Product() { ProductID = 1, Name = "M" };
            var prod2 = new Product() { ProductID = 2, Name = "N" };
            cart.AddItem(prod1, 3);
            cart.AddItem(prod2, 4);

            // Act, Assert
            Assert.AreSame(prod1, cart.Lines.First().Product, "Should have stored the same item.");
            Assert.AreEqual(3, cart.Lines.First().Quantity, "Should have 3 as the quantity");
            Assert.AreSame(prod2, cart.Lines.Last().Product, "Should have stored the same item.");
            Assert.AreEqual(4, cart.Lines.Last().Quantity, "Should have 4 as the quantity");
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllItems_FromCart()
        {
            // Arrange 
            var cart = new Cart();

            // Act
            cart.AddItem(new Product() { ProductID = 1 }, 1);

            // Assert
            Assert.AreEqual(1, cart.Lines.Count(), "There should be only 1 item.");

            // Act
            cart.Clear();

            // Assert
            Assert.AreEqual(0, cart.Lines.Count(), "There must not be any items in the cart.");
        }
    }
}
