using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eShop_RazorPages.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly EShopDbContext _context;

        public OrdersModel(EShopDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToPage("/Index");

            Orders = await _context.Orders
                            .Where(o => o.CustomerId == customerId)
                            .ToListAsync();

            return Page();
        }
    }
}
