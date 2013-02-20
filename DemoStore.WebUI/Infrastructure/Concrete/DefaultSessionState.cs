using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoStore.WebUI.Infrastructure.Abstract;

namespace DemoStore.WebUI.Infrastructure.Concrete
{
    public class DefaultSessionState : ISessionState
    {
        private readonly HttpSessionStateBase session;

        public DefaultSessionState(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public void Clear()
        {
            session.RemoveAll();
        }

        public void Delete(string key)
        {
            session.Remove(key);
        }

        public object Get(string key)
        {
            return session[key];
        }

        public void Store(string key, object value)
        {
            session[key] = value;
        }
    }

    public static class SessionExtensions
    {
        public static T Get<T>(this ISessionState sessionState, string key) where T : class
        {
            return sessionState.Get(key) as T;
        }
    }
}