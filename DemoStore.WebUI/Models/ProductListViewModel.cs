using System.Collections.Generic;
using DemoStore.Core.Entities;

namespace DemoStore.WebUI.Models
{
    /// <summary>
    /// This class is used to transfer product data from the controller to the List View.
    /// </summary>
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}