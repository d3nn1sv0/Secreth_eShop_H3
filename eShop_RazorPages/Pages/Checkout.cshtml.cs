using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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

        public async Task<IActionResult> OnPostAsync(string email, string name, string shippingAddress, string paymentMethod, string shippingMethod)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return NotFound();

            var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");
            if (basket == null) return NotFound();

            try
            {
                var customer = await _context.Customers.FindAsync(customerId);

                customer.ShippingAddress = shippingAddress;
                _context.Entry(customer).State = EntityState.Modified;

                var order = new Order
                {
                    CustomerId = customerId.Value,
                    OrderDate = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove($"ShoppingCart_{customerId}");

                SendEmailConfirmation(email, name, shippingAddress, paymentMethod, shippingMethod);

                return RedirectToPage("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostAsync: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        private void SendEmailConfirmation(string email, string name, string shippingAddress, string paymentMethod, string shippingMethod)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("secreth92@gmail.com"); // Replace with your email address
                mailMessage.To.Add(email);
                mailMessage.Subject = "Order Confirmation";
                mailMessage.Body = $"Hello {name},\n\n" +
                                   "Thank you for your order. Here is a summary of your order details:\n\n" +
                                   $"Shipping Address: {shippingAddress}\n" +
                                   $"Payment Method: {paymentMethod}\n" +
                                   $"Shipping Method: {shippingMethod}\n\n" +
                                   "If you have any questions or concerns, please contact us.";

                SmtpClient smtpClient = new SmtpClient("smtp.example.com"); // Replace with your SMTP server
                smtpClient.Credentials = new System.Net.NetworkCredential("youremail@example.com", "yourpassword"); // Replace with your email address and password
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEmailConfirmation: {ex.Message}");
            }
        }


    }
}
