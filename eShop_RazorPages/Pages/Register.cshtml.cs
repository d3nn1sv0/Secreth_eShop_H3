using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly EShopDbContext _context;

    public RegisterModel(EShopDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Customer Customer { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Add hashing for the password before saving it to the database
        Customer.Password = BCrypt.Net.BCrypt.HashPassword(Customer.Password);

        _context.Customers.Add(Customer);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}
