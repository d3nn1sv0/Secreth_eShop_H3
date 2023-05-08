using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eShop_RazorPages.Pages
{
    public class CartModel : PageModel
    {
        private readonly EShopDbContext _context;

        public CartModel(EShopDbContext context)
        {
            _context = context;
        }

        public List<BasketItem> ShoppingCart { get; set; }
        public decimal TotalPrice { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return NotFound();

            var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");

            if (basket != null)
            {
                TotalPrice = 0;

                // Fetch the products for each BasketItem
                foreach (var item in basket.BasketItems)
                {
                    item.Product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                    TotalPrice += item.Product.Price * item.Quantity;
                }

                ShoppingCart = (List<BasketItem>)basket.BasketItems;
            }
            else
            {
                ShoppingCart = new List<BasketItem>();
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAsync(Dictionary<int, int> quantities, int? removeItemId)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return NotFound();

            var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");
            if (basket == null) return NotFound();

            try
            {
                Console.WriteLine($"Quantities received: {JsonSerializer.Serialize(quantities)}");

                // Check if a removeItemId is present
                if (removeItemId.HasValue)
                {
                    var itemToRemove = basket.BasketItems.FirstOrDefault(x => x.ProductId == removeItemId.Value);
                    if (itemToRemove != null)
                    {
                        basket.BasketItems.Remove(itemToRemove);
                    }
                }
                else
                {
                    // Remove the items with quantity equal to zero
                    var itemsToRemove = basket.BasketItems.Where(item => quantities[item.ProductId] == 0).ToList();
                    foreach (var item in itemsToRemove)
                    {
                        basket.BasketItems.Remove(item);
                    }

                    // Update the quantities for the remaining items
                    foreach (var item in basket.BasketItems)
                    {
                        var quantity = quantities[item.ProductId];
                        if (quantity > 0)
                        {
                            item.Quantity = quantity;
                        }
                    }
                }

                HttpContext.Session.Set($"ShoppingCart_{customerId}", basket);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnPostAsync: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }



    }
}
