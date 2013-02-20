using DemoStore.Core.Entities;

namespace DemoStore.WebUI.Models
{
    /// <summary>
    /// This class contains the information to return the user back to continue shopping.
    /// </summary>
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}