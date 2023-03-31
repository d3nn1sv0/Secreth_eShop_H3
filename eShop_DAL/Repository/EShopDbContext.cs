using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop_DAL.Repository
{
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

        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeeding seedData = new DataSeeding();

            modelBuilder.Entity<Category>().HasData(seedData.Categories);
            modelBuilder.Entity<Supplier>().HasData(seedData.Suppliers);
            modelBuilder.Entity<Product>().HasData(seedData.Products);
            modelBuilder.Entity<Image>().HasData(seedData.Images);
            modelBuilder.Entity<Order>().HasData(seedData.Orders);
            modelBuilder.Entity<Customer>().HasData(seedData.Customers);
        }
    }


}
