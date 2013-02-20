using System;
using System.Web;
using System.Web.Mvc;
using DemoStore.Core;
using DemoStore.Core.Concrete;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;
using DemoStore.DataAccess.EntityFramework;
using DemoStore.DataAccess.InMemory;
using DemoStore.WebUI.Infrastructure.Abstract;
using DemoStore.WebUI.Infrastructure.Concrete;
using Ninject;

namespace DemoStore.WebUI.Infrastructure
{
    /// <summary>
    /// This is the controller factory that will resolve dependencies in each controller.
    /// This class is registered with the Asp.NET MVC framework in Global.asax file.
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        /// <summary>
        /// The <see cref="DefaultControllerFactory"/> is used to create controllers.
        /// Once this controller is registered in the Global.asax file then this factory method will
        /// be called to create a controller by the Asp.NET MVC framework. The Ninject DI container
        /// will behave as a factory and resolve the dependency.
        /// </summary>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = null;

            if (controllerType != null)
            {
                controller = (IController)kernel.Get(controllerType);
            }

            return controller;
        }

        private void AddBindings()
        {
            // Presentation Layer Bindings
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            // session
            kernel.Bind<ISessionState>().To<DefaultSessionState>();
            kernel.Bind<HttpSessionStateBase>().ToMethod(ctx => new HttpSessionStateWrapper(HttpContext.Current.Session));

            // Core Bindings
            kernel.Bind<ICatalogService>().To<CatalogService>();
            kernel.Bind<IOrderService>().To<OrderService>();

            // Data Access Bindings
            kernel.Bind<IProductRepository>().To<EfProductRepository>();
            kernel.Bind<ICartRepository>().To<CartRepository>();
           
          
        }
    }
}