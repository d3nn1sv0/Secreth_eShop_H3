using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly EShopDbContext _context;

    public LoginModel(EShopDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == Input.Email);

            if (customer != null && BCrypt.Net.BCrypt.Verify(Input.Password, customer.Password))
            {
                HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);

                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return Page();
    }
}
