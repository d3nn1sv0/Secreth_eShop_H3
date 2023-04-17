using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop_RazorPages.Pages
{
    public class CartModel : PageModel
    {
        public List<BasketItem> ShoppingCart { get; set; }

        public void OnGet()
        {
            ShoppingCart = HttpContext.Session.Get<List<BasketItem>>("ShoppingCart") ?? new List<BasketItem>();
        }
    }
}
