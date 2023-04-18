using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop_DAL.Repository
{
    public class DataSeeding
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EShopDbContext>();
            //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            #region Seed Datamodels
            //// Remove existing data from tables
            //context.Images.RemoveRange(context.Images);
            //context.Products.RemoveRange(context.Products);
            //context.Categories.RemoveRange(context.Categories);
            //context.Suppliers.RemoveRange(context.Suppliers);
            ////context.Customers.RemoveRange(context.Customers);
            ////context.Orders.RemoveRange(context.Orders);

            //context.SaveChanges();

            int seed = 80085;

            // Categories
            var categories = new Faker<Category>()
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First())
                    .UseSeed(seed)
                    .Generate(5);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Suppliers
            var suppliers = new Faker<Supplier>()
                    .RuleFor(s => s.Name, f => f.Company.CompanyName())
                    .UseSeed(seed)
                    .Generate(3);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            // Products
            var products = new Faker<Product>()
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.Price, f => f.Random.Decimal(1.00m, 100.00m))
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).CategoryId)
                    .RuleFor(p => p.SupplierId, f => f.PickRandom(suppliers).SupplierId)
                    .UseSeed(seed)
                    .Generate(25);

            context.Products.AddRange(products);
            context.SaveChanges();

            // Images
            var images = new Faker<Image>()
                    .RuleFor(i => i.Url, f => f.Image.PicsumUrl())
                    .RuleFor(i => i.ProductId, f => f.PickRandom(products).ProductId)
                    .UseSeed(seed)
                    .Generate(25);

            context.Images.AddRange(images);
            context.SaveChanges();

            // Customers
            var customers = new Faker<Customer>()
                    .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                    .RuleFor(c => c.LastName, f => f.Name.LastName())
                    .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                    .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.ShippingAddress, f => f.Address.StreetAddress())
                    .RuleFor(c => c.Password, f =>
                    {
                        var passwordHasher = new PasswordHasher<Customer>();
                        return passwordHasher.HashPassword(null, f.Internet.Password());
                    })
                    .UseSeed(seed)
                    .Generate(1);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Orders
            var orders = new Faker<Order>()
                    .RuleFor(o => o.OrderDate, f => f.Date.Past(1))
                    .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).CustomerId)
                    .UseSeed(seed)
                    .Generate(1);

            context.Orders.AddRange(orders);
            context.SaveChanges();
            #endregion

            #region Create Admin for Identity
            //// Create admin role if it doesn't exist
            //if (!await roleManager.RoleExistsAsync("Admin"))
            //{
            //    await roleManager.CreateAsync(new IdentityRole("Admin"));
            //}

            //// Create admin user if it doesn't exist
            //var adminUser = await userManager.FindByNameAsync("admin@example.com");
            //if (adminUser == null)
            //{
            //    adminUser = new IdentityUser
            //    {
            //        UserName = "admin@example.com",
            //        Email = "admin@example.com"
            //    };
            //    var result = await userManager.CreateAsync(adminUser, "Admin@123");
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(adminUser, "Admin");
            //    }
            //}
            #endregion

            // Create admin user if it doesn't exist
            var adminEmail = "admin@example.com";
            var adminUser = await context.Customers.SingleOrDefaultAsync(c => c.Email == adminEmail);
            if (adminUser == null)
            {
                var passwordHasher = new PasswordHasher<Customer>();
                adminUser = new Customer
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = adminEmail,
                    PhoneNumber = "1234567890",
                    ShippingAddress = "123 Admin Street",
                    Password = passwordHasher.HashPassword(null, "Admin@123")
                };
                context.Customers.Add(adminUser);
                await context.SaveChangesAsync();
            }

        }
    }
}
