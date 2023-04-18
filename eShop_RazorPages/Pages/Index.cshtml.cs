using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace eShop_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly EShopDbContext _context;

        public const int PageSize = 6;
        public IPagedList<Product> Products { get; set; }
        public List<BasketItem> ShoppingCart { get; set; }
        public List<Category> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }


        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string SearchText { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedSupplier { get; set; }

        public IndexModel(ILogger<IndexModel> logger, EShopDbContext context, ProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _context = context;
        }

        public async Task OnGetAsync(string searchText = null, string selectedCategory = null, string selectedSupplier = null)
        {
            SearchText = searchText ?? string.Empty;
            SelectedCategory = selectedCategory;
            SelectedSupplier = selectedSupplier;

            var allProducts = await _productRepository.SearchAsync(SearchText);

            if (!string.IsNullOrWhiteSpace(SelectedCategory))
            {
                allProducts = allProducts.Where(p => p.Category != null && p.Category.Name == SelectedCategory).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SelectedSupplier))
            {
                allProducts = allProducts.Where(p => p.Supplier != null && p.Supplier.Name == SelectedSupplier).ToList();
            }


            Products = new PagedList<Product>(allProducts, PageNumber, PageSize);

            ShoppingCart = HttpContext.Session.Get<List<BasketItem>>("ShoppingCart") ?? new List<BasketItem>();

            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId != null)
            {
                var basket = HttpContext.Session.Get<Basket>($"ShoppingCart_{customerId}");
                ShoppingCart = (List<BasketItem>)(basket?.BasketItems ?? new List<BasketItem>());
            }
            else
            {
                ShoppingCart = new List<BasketItem>();
            }

            Categories = (List<Category>)await _categoryRepository.GetAllAsync();
            Suppliers = (List<Supplier>)await _supplierRepository.GetAllAsync();
        }



        public IActionResult OnPostAddToBasketAsync(int productId)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                // Redirect to the login page if the user is not logged in
                return RedirectToPage("/Login");
            }

            var shoppingCartKey = $"ShoppingCart_{customerId}";
            var basket = HttpContext.Session.Get<Basket>(shoppingCartKey);
            if (basket == null)
            {
                basket = new Basket
                {
                    CustomerId = customerId.Value,
                    BasketItems = new List<BasketItem>()
                };
            }

            var item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem
                {
                    ProductId = productId,
                    Quantity = 0,
                };
                basket.BasketItems.Add(item);
            }

            item.Quantity++;

            HttpContext.Session.Set(shoppingCartKey, basket);

            return RedirectToPage();
        }
    }
}
