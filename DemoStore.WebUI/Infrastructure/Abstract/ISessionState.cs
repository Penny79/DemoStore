using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoStore.WebUI.Infrastructure.Abstract
{
    public interface ISessionState
    {
        void Clear();
        void Delete(string key);
        object Get(string key);
        void Store(string key, object value);
    }

   
}