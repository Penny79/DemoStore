using System.Linq;
using DemoStore.Core.Entities;

namespace DemoStore.Core.Interface.DataAccess
{
    /// <summary>
    /// This represents a abstraction to store product data.
    /// </summary>
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        void DeleteProduct(Product product);
    }
}
