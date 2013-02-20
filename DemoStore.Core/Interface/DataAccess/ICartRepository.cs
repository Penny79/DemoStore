using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoStore.Core.Entities;

namespace DemoStore.Core.Interface.DataAccess
{
    /// <summary>
    /// An abstraction for the cart storage
    /// </summary>
    public interface ICartRepository
    {
        Cart GetCart(Guid cartId);
    }
}
