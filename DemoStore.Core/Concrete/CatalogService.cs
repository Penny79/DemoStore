using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.Core.Concrete
{
    /// <summary>
    /// The core abstraction for all Catalog related logic.
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository productRepository;
       

        public CatalogService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public IQueryable<Product> GetAllProducts()
        {
            return this.productRepository.Products;
        }

        public void SaveProduct(Product product)
        {
            this.productRepository.SaveProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            this.productRepository.DeleteProduct(product);
        }
    }
}
