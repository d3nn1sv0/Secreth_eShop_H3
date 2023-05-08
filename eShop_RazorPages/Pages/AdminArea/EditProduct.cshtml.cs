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
        ProductCreateViewModel.Product = await _productRepository.GetByIdAsyncWithIncludes(id, "Images");
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

        var existingProduct = await _productRepository.GetByIdAsyncWithIncludes(id, "Images");
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

        if (!string.IsNullOrEmpty(ImageUrl))
        {
            if (existingProduct.Images != null)
            {
                foreach (var existingImage in existingProduct.Images.ToList())
                {
                    existingProduct.Images.Remove(existingImage);
                    await _imageRepository.DeleteAsync(existingImage.ImageId);
                }
            }

            var newImage = new Image { Url = ImageUrl, ProductId = existingProduct.ProductId };
            await _imageRepository.CreateAsync(newImage);

            if (existingProduct.Images == null)
            {
                existingProduct.Images = new List<Image>();
            }
            existingProduct.Images.Add(newImage);
        }

        await _productRepository.UpdateAsync(existingProduct);

        return RedirectToPage("./Products");
    }


}
