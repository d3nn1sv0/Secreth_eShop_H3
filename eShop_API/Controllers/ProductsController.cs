using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eShop_DAL.Repository;
using Newtonsoft.Json;

namespace eShop_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                IsVisible = product.IsVisible,
                CategoryId = product.CategoryId,
                Category = new CategoryDto { CategoryId = product.Category.CategoryId, Name = product.Category.Name },
                SupplierId = product.SupplierId,
                Supplier = new SupplierDto { SupplierId = product.Supplier.SupplierId, Name = product.Supplier.Name, Email = product.Supplier.Email, PhoneNumber = product.Supplier.PhoneNumber },
                Images = product.Images.Select(i => new ImageDto
                {
                    ImageId = i.ImageId,
                    Url = i.Url,
                    ProductId = i.ProductId
                }).ToList()
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync("Category,Supplier,Images");
            return Content(JsonConvert.SerializeObject(products.Select(MapToDto)), "application/json");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsyncWithIncludes(id, "Category,Supplier,Images");

            if (product == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(product));
        }


        /// <summary>
        /// Searches for products by search text, category name, and/or supplier name.
        /// </summary>
        /// <param name="searchText">The search text to filter products by</param>
        /// <param name="categoryName">The category name to filter products by (optional)</param>
        /// <param name="supplierName">The supplier name to filter products by (optional)</param>
        /// <returns>A list of Product objects that match the search criteria</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts(
            [FromQuery] string searchText = "",
            [FromQuery] string categoryName = null,
            [FromQuery] string supplierName = null)
        {
            var products = await _productRepository.SearchAsync(searchText, categoryName, supplierName);
            return Ok(products.Select(MapToDto));
        }

    }
}
