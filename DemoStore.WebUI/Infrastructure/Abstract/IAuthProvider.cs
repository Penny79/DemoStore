namespace DemoStore.WebUI.Infrastructure.Abstract
{
    /// <summary>
    /// This is the interface of a authentication provider.
    /// </summary>
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
