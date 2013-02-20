using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoStore.Core.Entities
{
    /// <summary>
    /// This class represent a product sold by SportsStore.
    /// </summary>
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please sepcify a category.")]
        public string Category { get; set; }

        public string ImageFile { get; set; }

    }
}
