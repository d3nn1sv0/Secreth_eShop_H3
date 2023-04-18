using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eShop_RazorPages.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly EShopDbContext _context;

        public CheckoutModel(EShopDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Check if the user has items in their cart
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToPage("/Index");

            var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");
            if (basket == null || !basket.BasketItems.Any()) return RedirectToPage("/Cart");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string shippingAddress)
        {
            // Get the logged-in customer's ID
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return NotFound();

            // Get the customer's shopping cart
            var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");
            if (basket == null) return NotFound();

            try
            {
                // Get the customer from the database
                var customer = await _context.Customers.FindAsync(customerId);

                // Update the customer's shipping address
                customer.ShippingAddress = shippingAddress;
                _context.Entry(customer).State = EntityState.Modified;

                // Create a new order
                var order = new Order
                {
                    CustomerId = customerId.Value,
                    OrderDate = DateTime.UtcNow
                };

                // Save the order to the database
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Clear the shopping cart
                HttpContext.Session.Remove($"ShoppingCart_{customerId}");

                // Redirect to a success page or any other page you want
                return RedirectToPage("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostAsync: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


    }
}
