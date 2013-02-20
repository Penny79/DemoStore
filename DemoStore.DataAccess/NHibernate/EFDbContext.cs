using System.Data.Entity;
using DemoStore.Core.Entities;
using DemoStore.DataAccess.Migrations;

namespace DemoStore.DataAccess.EntityFramework
{
    /// <summary>
    /// This class provides the access to a concrete database context. 
    /// </summary>
    public class EfDbContext : DbContext
    {
        public EfDbContext()
        {
            //set the initializer to migration
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfDbContext, Configuration>());
            this.Database.Initialize(false);
        }


        public DbSet<Product> Products { get; set; }
    }
}
