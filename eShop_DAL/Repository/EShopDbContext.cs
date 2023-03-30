using eShop_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop_DAL.Repository
{
    //db context for eShop_DAL.Models
    public class EShopDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        private readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = eShopDB; Trusted_Connection = True; ";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }


}
