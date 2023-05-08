using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eShop_DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;


public class CreateProductModel : PageModel
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Image> _imageRepository;
    private readonly ILogger<CreateProductModel> _logger;

    public CreateProductModel(
        IRepository<Product> productRepository, 
        IRepository<Supplier> supplierRepository, 
        IRepository<Category> categoryRepository, 
        IRepository<Image> imageRepository, 
        ILogger<CreateProductModel> logger)
    {
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
        _categoryRepository = categoryRepository;
        _imageRepository = imageRepository;
        _logger = logger;
    }

    public SelectList Suppliers { get; set; }
    public SelectList Categories { get; set; }
    [BindProperty]
    public ProductCreateViewModel ProductCreateViewModel { get; set; } = new ProductCreateViewModel();

    [BindProperty]
    public string? ImageUrl { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        Suppliers = new SelectList(await _supplierRepository.GetAllAsync(), "SupplierId", "Name");
        Categories = new SelectList(await _categoryRepository.GetAllAsync(), "CategoryId", "Name");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("OnPostAsync called");
        _logger.LogInformation($"SelectedSupplierId: {ProductCreateViewModel.SelectedSupplierId}");
        _logger.LogInformation($"Product.SupplierId before assignment: {ProductCreateViewModel.Product.SupplierId}");

        ProductCreateViewModel.Product.SupplierId = ProductCreateViewModel.SelectedSupplierId;
        ProductCreateViewModel.Product.CategoryId = ProductCreateViewModel.SelectedCategoryId;
        ModelState.Remove("ProductCreateViewModel.Product.Category");
        ModelState.Remove("ProductCreateViewModel.Product.Supplier");

        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Model state is not valid");
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            foreach (var error in errors)
            {
                _logger.LogInformation("Key: {Key}, Error: {ErrorMessage}", error.Key, error.Errors.First().ErrorMessage);
            }
            Suppliers = new SelectList(await _supplierRepository.GetAllAsync(), "SupplierId", "Name");
            Categories = new SelectList(await _categoryRepository.GetAllAsync(), "CategoryId", "Name");
            return Page();
        }

        ProductCreateViewModel.Product.Category = await _categoryRepository.GetByIdAsync(ProductCreateViewModel.Product.CategoryId);
        ProductCreateViewModel.Product.Supplier = await _supplierRepository.GetByIdAsync(ProductCreateViewModel.Product.SupplierId);
        ProductCreateViewModel.Product.IsVisible = true;

        _logger.LogInformation("Attempting to create product");
        await _productRepository.CreateAsync(ProductCreateViewModel.Product);
        _logger.LogInformation("Product created with ID {ProductId}", ProductCreateViewModel.Product.ProductId);

        if (!string.IsNullOrEmpty(ImageUrl))
        {
            var image = new Image
            {
                Url = ImageUrl,
                ProductId = ProductCreateViewModel.Product.ProductId
            };

            _logger.LogInformation("Attempting to create image");
            await _imageRepository.CreateAsync(image);
            _logger.LogInformation("Image created with ID {ImageId}", image.ImageId);
        }

        return RedirectToPage("./Products");
    }
}
