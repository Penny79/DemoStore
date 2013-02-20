using System.Linq;
using System.Web.Mvc;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.WebUI.Controllers
{
    /// <summary>
    /// This controller contains the menu component.
    /// </summary>
    public class NavController : Controller
    {
        private readonly ICatalogService catalogService;

        public NavController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;                
        }

        /// <summary>
        /// This is the partial (child action) view content that bound to the output.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var categories = catalogService.GetAllProducts()
                .Select(prod => prod.Category)
                .Distinct()
                .OrderBy(prodCategory => prodCategory);

            return PartialView(categories);
        }

    }
}
