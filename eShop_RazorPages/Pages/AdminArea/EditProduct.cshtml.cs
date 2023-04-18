using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EditProductModel : PageModel
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Image> _imageRepository;
    private readonly ILogger<EditProductModel> _logger;

    public EditProductModel(
        IRepository<Product> productRepository,
        IRepository<Supplier> supplierRepository,
        IRepository<Category> categoryRepository,
        IRepository<Image> imageRepository,
        ILogger<EditProductModel> logger)
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

    public async Task<IActionResult> OnGetAsync(int id)
    {
        ProductCreateViewModel.Product = await _productRepository.GetByIdAsync(id);
        if (ProductCreateViewModel.Product == null)
        {
            return NotFound();
        }

        Suppliers = new SelectList(await _supplierRepository.GetAllAsync(), "SupplierId", "Name");
        Categories = new SelectList(await _categoryRepository.GetAllAsync(), "CategoryId", "Name");

        ProductCreateViewModel.SelectedSupplierId = ProductCreateViewModel.Product.SupplierId;
        ProductCreateViewModel.SelectedCategoryId = ProductCreateViewModel.Product.CategoryId;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            Suppliers = new SelectList(await _supplierRepository.GetAllAsync(), "SupplierId", "Name");
            Categories = new SelectList(await _categoryRepository.GetAllAsync(), "CategoryId", "Name");
            return Page();
        }

        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = ProductCreateViewModel.Product.Name;
        existingProduct.Price = ProductCreateViewModel.Product.Price;
        existingProduct.Description = ProductCreateViewModel.Product.Description;

        existingProduct.CategoryId = ProductCreateViewModel.SelectedCategoryId;
        existingProduct.SupplierId = ProductCreateViewModel.SelectedSupplierId;

        existingProduct.IsVisible = ProductCreateViewModel.Product.IsVisible;

        await _productRepository.UpdateAsync(existingProduct);

        return RedirectToPage("./Products");
    }

}
