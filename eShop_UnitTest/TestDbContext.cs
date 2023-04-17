using eShop_DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.CompilerServices;

namespace eShop_UnitTest
{
    public static class TestDbContext
    {        
        public static EShopDbContext CreateContext([CallerMemberName] string Dbname = "db")
        {
            var builder = new DbContextOptionsBuilder<EShopDbContext>();

            builder.UseInMemoryDatabase(Dbname);

            var context = new EShopDbContext(builder.Options);

            return context;
        }
    }
}
