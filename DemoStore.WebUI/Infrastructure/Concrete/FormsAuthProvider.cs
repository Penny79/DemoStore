using System.Web.Security;
using DemoStore.WebUI.Infrastructure.Abstract;

namespace DemoStore.WebUI.Infrastructure.Concrete
{
    /// <summary>
    /// This class provides the forms authentication for the application.
    /// </summary>
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool authenticated = FormsAuthentication.Authenticate(username, password);

            if (authenticated)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return authenticated;
        }
    }
}