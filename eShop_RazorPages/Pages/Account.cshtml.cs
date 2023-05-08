using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eShop_RazorPages.Pages
{
    public class AccountModel : PageModel
    {
        private readonly EShopDbContext _context;

        public AccountModel(EShopDbContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToPage("/Index");

            Customer = await _context.Customers.FindAsync(customerId);
            if (Customer == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string phoneNumber, string shippingAddress)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return NotFound();

            Customer = await _context.Customers.FindAsync(customerId);
            if (Customer == null) return NotFound();

            Customer.PhoneNumber = phoneNumber;
            Customer.ShippingAddress = shippingAddress;

            _context.Entry(Customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
