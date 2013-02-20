using System.Web.Mvc;
using DemoStore.WebUI.Infrastructure.Abstract;
using DemoStore.WebUI.Models;

namespace DemoStore.WebUI.Controllers
{
    /// <summary>
    /// This class is used to login to the application.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAuthProvider _auth;

        public AccountController(IAuthProvider auth)
        {
            _auth = auth;
        }

        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var authenticated = _auth.Authenticate(viewModel.Username, viewModel.Password);

                if (authenticated)
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
