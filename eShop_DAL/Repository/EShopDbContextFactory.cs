using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace eShop_DAL.Repository
{
    public class EShopContextFactory : IDesignTimeDbContextFactory<EShopDbContext>
    {
        private readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = eShopDB; Trusted_Connection = True; ";
        public EShopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging(true);

            return new EShopDbContext(optionsBuilder.Options);
        }
    }
}
