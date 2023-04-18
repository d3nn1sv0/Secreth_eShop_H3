using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop_RazorPages.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class ProductsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
