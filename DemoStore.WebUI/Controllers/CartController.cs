using System;
using System.Linq;
using System.Web.Mvc;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.WebUI.Infrastructure.Abstract;
using DemoStore.WebUI.Models;

namespace DemoStore.WebUI.Controllers
{
    /// <summary>
    /// This class contains the controller logic to support Cart functionality.
    /// </summary>
    public class CartController : Controller
    {
        private readonly ICatalogService catalogService;
        private readonly IOrderService orderService;
        private readonly ISessionState sessionState;


        public CartController(ICatalogService catalogService, IOrderService orderService, ISessionState sessionState)
        {
            this.catalogService = catalogService;
            this.orderService = orderService;
            this.sessionState = sessionState;
        }

        [HttpPost]
        public ViewResult Checkout(Cart postedCart)
        {
            Cart cart = GetCartFromSession();

            UpdateModel(cart);

            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                orderService.SubmitOrder(cart);
                cart.Clear();
                return View("Completed");
            }

            return View(cart);
        }

        public ViewResult Index(string returnUrl)
        {
            Cart cart = GetCartFromSession();

            var cartIndexViewModel = new CartIndexViewModel()
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                };

            return View(cartIndexViewModel);
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Cart cart = GetCartFromSession();

            var product = catalogService.GetAllProducts()
                                        .FirstOrDefault(prod => prod.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Cart cart = GetCartFromSession();

            var productLine = cart.Lines
                                  .FirstOrDefault(prod => prod.Product.ProductID == productId);

            if (productLine != null)
            {
                cart.RemoveItem(productLine.Product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public ViewResult Summary()
        {
            Cart cart = GetCartFromSession();
            return View(cart);
        }

        public ViewResult Checkout()
        {
            Cart cart = GetCartFromSession();
            return View(cart);
        }

        private Cart GetCartFromSession()
        {
            const string CartKey = "Cart";
            var cartIdValue = sessionState.Get(CartKey);
            Guid cartId;

            if (cartIdValue == null)
            {
                cartId = Guid.NewGuid();
                sessionState.Store(CartKey, cartId);
            }
            else
            {
                cartId = (Guid) cartIdValue;
            }
            return orderService.GetCart(cartId);
        }
    }
}