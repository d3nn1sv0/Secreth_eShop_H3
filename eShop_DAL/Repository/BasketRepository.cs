using eShop_DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BasketRepository : Repository<Basket>, IBasketRepository
{
    private readonly EShopDbContext _context;

    public BasketRepository(EShopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Basket> GetBasketWithItemsAsync(int customerId)
    {
        return await _context.Baskets
            .Include(b => b.BasketItems)
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(b => b.CustomerId == customerId);
    }
}
