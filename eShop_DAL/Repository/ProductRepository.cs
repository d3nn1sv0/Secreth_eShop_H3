using eShop_DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly EShopDbContext _context;

    public ProductRepository(EShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return await _context.Products.Include(p => p.Images).ToListAsync();
        }

        searchText = searchText.Trim().ToLower();

        return await _context.Products.Include(p => p.Images)
            .Where(p => p.Name.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchBySupplierNameAsync(string supplierName)
    {
        if (string.IsNullOrWhiteSpace(supplierName))
        {
            return await _context.Products.ToListAsync();
        }

        supplierName = supplierName.Trim().ToLower();

        return await _context.Products
            .Include(p => p.Supplier)
            .Where(p => p.Supplier.Name.ToLower().Contains(supplierName))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchByCategoryNameAsync(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return await _context.Products.ToListAsync();
        }

        categoryName = categoryName.Trim().ToLower();

        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category.Name.ToLower().Contains(categoryName))
            .ToListAsync();
    }

}
