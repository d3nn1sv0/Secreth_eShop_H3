using System.Collections.Generic;
using System.Threading.Tasks;
using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProductsModel : PageModel
{
    private readonly IRepository<Product> _productRepository;

    public ProductsModel(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> Products { get; set; }

    public async Task OnGetAsync()
    {
        Products = await _productRepository.GetAllAsync(includeProperties: "Supplier,Category");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        int productId = Convert.ToInt32(Request.Form["productId"]);
        if (productId == 0)
        {
            return NotFound();
        }

        var product = await _productRepository.GetByIdAsync(productId);
        if (product != null)
        {
            product.IsVisible = !product.IsVisible;
            await _productRepository.UpdateAsync(product);
        }

        return RedirectToPage();
    }
}
