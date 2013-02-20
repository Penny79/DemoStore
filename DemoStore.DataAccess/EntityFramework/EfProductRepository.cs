using System.Linq;
using DemoStore.Core.Entities;
using DemoStore.Core.Interface.DataAccess;

namespace DemoStore.DataAccess.EntityFramework
{
    /// <summary>
    /// This class represent the concrete implementation to access the data store using Entity Framework.
    /// </summary>
    public class EfProductRepository : IProductRepository
    {
        private readonly EfDbContext context = new EfDbContext();
 
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
                context.Entry(product).State = System.Data.EntityState.Added;
            }
            else
            {
                context.Entry(product).State = System.Data.EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
