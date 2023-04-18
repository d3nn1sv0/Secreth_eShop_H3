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

    public async Task<IEnumerable<Product>> SearchAsync(string searchText, string categoryName = null, string supplierName = null)
    {
        var query = _context.Products
            .Include(p => p.Images)
            .Include(p => p.Category) 
            .Include(p => p.Supplier) 
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            searchText = searchText.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText));
        }

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            categoryName = categoryName.Trim().ToLower();
            query = query.Where(p => p.Category.Name.ToLower().Contains(categoryName));
        }

        if (!string.IsNullOrWhiteSpace(supplierName))
        {
            supplierName = supplierName.Trim().ToLower();
            query = query.Where(p => p.Supplier.Name.ToLower().Contains(supplierName));
        }

        return await query.ToListAsync();
    }


}
