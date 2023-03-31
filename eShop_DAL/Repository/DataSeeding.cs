using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop_DAL.Repository
{
    public class DataSeeding
    {
        public List<Category> Categories { get; }
        public List<Customer> Customers { get; }
        public List<Product> Products { get; }
        public List<Image> Images { get; }
        public List<Order> Orders { get; }
        public List<Supplier> Suppliers { get; }

        public DataSeeding()
        {
            int seed = 80085;
            var faker = new Faker();

            // Categories
            Categories = new Faker<Category>()
                .RuleFor(c => c.CategoryId, f => f.IndexFaker + 1)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First())
                .UseSeed(seed)
                .Generate(5);

            // Suppliers
            Suppliers = new Faker<Supplier>()
                .RuleFor(s => s.SupplierId, f => f.IndexFaker + 1)
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .UseSeed(seed)
                .Generate(3);

            // Products
            Products = new Faker<Product>()
                .RuleFor(p => p.ProductId, f => f.IndexFaker + 1)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1.00m, 100.00m))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(Categories).CategoryId)
                .RuleFor(p => p.SupplierId, f => f.PickRandom(Suppliers).SupplierId)
                .UseSeed(seed)
                .Generate(50);

            // Images
            Images = new Faker<Image>()
                .RuleFor(i => i.ImageId, f => f.IndexFaker + 1)
                .RuleFor(i => i.Url, f => f.Image.PicsumUrl())
                .RuleFor(i => i.ProductId, f => f.PickRandom(Products).ProductId)
                .UseSeed(seed)
                .Generate(50);

            // Customers
            Customers = new Faker<Customer>()
                .RuleFor(c => c.CustomerId, f => f.IndexFaker + 1)
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .UseSeed(seed)
                .Generate(10);

            // Orders
            Orders = new Faker<Order>()
                .RuleFor(o => o.OrderId, f => f.IndexFaker + 1)
                .RuleFor(o => o.OrderDate, f => f.Date.Past(1))
                .RuleFor(o => o.CustomerId, f => f.PickRandom(Customers).CustomerId)
                .UseSeed(seed)
                .Generate(20);
        }
    }
}
