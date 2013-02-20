using System.Xml.Linq;
using DemoStore.Core.Entities;
using System.Data.Entity.Migrations;
using System.Linq;
using DemoStore.DataAccess.EntityFramework;

namespace DemoStore.DataAccess.Migrations
{
    internal sealed class Configuration :  DbMigrationsConfiguration<EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EfDbContext context)
        {
            // Create the query 
            var products = from p in XElement.Load(@"D:\Projekte\DemoStore\DemoStore.WebUI\App_Data\catalog.xml").Elements("product")
                        select p;

            // Execute the query 
            foreach (var product in products)
            {

                context.Products.AddOrUpdate(new Product()
                    {
                        ProductID = int.Parse(product.Element("ProductID").Value),
                        Category = product.Element("Category").Value,
                        Description = product.Element("Description").Value,
                        Name = product.Element("Name").Value,
                        Price = decimal.Parse(product.Element("Price").Value),
                        ImageFile= product.Element("ImageFile").Value
                    });
            }
        }
    }
}
