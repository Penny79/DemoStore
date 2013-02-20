using System.Linq;
using System.Web.Mvc;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;
using DemoStore.WebUI.Models;

namespace DemoStore.WebUI.Controllers
{
    /// <summary>
    /// This is the controller to show/manipulate products in DemoStore.
    /// </summary>
    public class ProductController : Controller
    {
        public int PageSize = 4;
        private readonly ICatalogService catalogService;

        public ProductController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public ViewResult List(string category, int page = 1)
        {
            var totalProducts = catalogService.GetAllProducts().ToList();
            var itemsToShow = totalProducts
                .Where(prod => category == null || prod.Category.Equals(category))
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? totalProducts.Count : totalProducts.Where(prod => prod.Category.Equals(category)).Count()
            };


            return View(new ProductListViewModel() { PagingInfo = pagingInfo, Products = itemsToShow, CurrentCategory = category });
        }
    }
}
