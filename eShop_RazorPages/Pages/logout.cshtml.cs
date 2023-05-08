using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.SetString("UserEmail", string.Empty);
        HttpContext.Session.Remove("CustomerId");
        HttpContext.Session.Set("isAdmin", false);
        return RedirectToPage("/Index");
    }
}
