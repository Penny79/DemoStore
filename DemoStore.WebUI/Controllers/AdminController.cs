using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.WebUI.Controllers
{
    /// <summary>
    /// This class contains the CRUD functionality supported by Electronics Store application.
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICatalogService catalogService;

        public AdminController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public ViewResult Index()
        {
            return View(catalogService.GetAllProducts());
        }

        public ViewResult Edit(int productId)
        {
            var products = catalogService.GetAllProducts();

            var product = products.Where(prod => prod.ProductID == productId).FirstOrDefault();

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileWrapper image)
        {
            if (ModelState.IsValid)
            {
                

                catalogService.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been added.", product.Name);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var products = catalogService.GetAllProducts();
            var product = products.Where(prod => prod.ProductID == productId).FirstOrDefault();

            if (product != null)
            {
                catalogService.DeleteProduct(product);
                TempData["message"] = string.Format("{0} has been deleted", product.Name);
            }

            return RedirectToAction("Index");
        }
    }
}
