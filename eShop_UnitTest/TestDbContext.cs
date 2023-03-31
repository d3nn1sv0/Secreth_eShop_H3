using eShop_DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.CompilerServices;

namespace eShop_UnitTest
{
    public static class TestDbContext
    {
        //public static EShopDbContext CreateInMemoryDbContext()
        //{
        //    var options = new DbContextOptionsBuilder<EShopDbContext>()
        //    .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
        //    .Options;

        //    var context = new EShopDbContext(options);

        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();

        //    var seedData = new DataSeeding();
        //    context.Categories.AddRange(seedData.Categories);
        //    context.Suppliers.AddRange(seedData.Suppliers);
        //    context.Products.AddRange(seedData.Products);
        //    context.Images.AddRange(seedData.Images);
        //    context.Customers.AddRange(seedData.Customers);
        //    context.Orders.AddRange(seedData.Orders);

        //    context.SaveChanges();

        //    return context;
        //}

        public static EShopDbContext CreateContext([CallerMemberName] string Dbname = "db")
        {
            var builder = new DbContextOptionsBuilder<EShopDbContext>();

            builder.UseInMemoryDatabase(Dbname);

            var context = new EShopDbContext(builder.Options);
            var seedData = new DataSeeding();  

            context.Categories.AddRange(seedData.Categories);
            context.Suppliers.AddRange(seedData.Suppliers);
            context.Products.AddRange(seedData.Products);
            context.Images.AddRange(seedData.Images);
            context.Customers.AddRange(seedData.Customers);
            context.Orders.AddRange(seedData.Orders);

            context.SaveChanges();

            return context;
        }
    }
}
