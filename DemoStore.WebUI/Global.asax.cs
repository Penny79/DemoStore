using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DemoStore.WebUI.Infrastructure;

namespace DemoStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Before coming here and changing the routes, write the urls that your application should support
            // Thereafter the patterns can be matched here.
            // Samples are = /, /Page1, /Soccer, /Soccer/Page1

            // This is going to match "/".
            routes.MapRoute(null, "", new { Controller = "Product", action = "List", category = (string)null, page = 1 });

            // This route going to match Page1, Page2 etc etc, but will not match PageX (see the RegEx constaint).
            routes.MapRoute(null, "Page{page}", new { Controller = "Product", action = "List", category = (string)null }, new { page = @"\d+" });

            // This route going to match /Soccer
            routes.MapRoute(null, "{Category}", new { controller = "Product", action = "List", page = 1 });

            // This route going to match Soccer/Page1 etc. Both category and page combined.
            routes.MapRoute(null, url: "{Category}/Page{page}", defaults: new { Controller = "Product", action = "List" }, constraints: new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            // Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            
            BundleTable.Bundles.EnableDefaultBundles();
        }
    }
}