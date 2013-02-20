using System;
using System.Linq;
using DemoStore.Core.Entities;

namespace DemoStore.Core.Interface
{
    /// <summary>
    /// Encapsulates the catalog related logic
    /// </summary>
    public interface ICatalogService
    {
        IQueryable<Product> GetAllProducts();

        void SaveProduct(Product product);

        void DeleteProduct(Product product);

    }
}
