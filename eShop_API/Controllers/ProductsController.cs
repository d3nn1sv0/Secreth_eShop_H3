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
            var dto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                IsVisible = product.IsVisible,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
            };

            if (product.Category != null)
            {
                dto.Category = new CategoryDto { CategoryId = product.Category.CategoryId, Name = product.Category.Name };
            }

            if (product.Supplier != null)
            {
                dto.Supplier = new SupplierDto { SupplierId = product.Supplier.SupplierId, Name = product.Supplier.Name, Email = product.Supplier.Email, PhoneNumber = product.Supplier.PhoneNumber };
            }

            if (product.Images != null)
            {
                dto.Images = product.Images.Select(i => new ImageDto
                {
                    ImageId = i.ImageId,
                    Url = i.Url,
                    ProductId = i.ProductId
                }).ToList();
            }

            return dto;
        }

        private Product MapToProduct(CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
                IsVisible = createProductDto.IsVisible,
                CategoryId = createProductDto.CategoryId,
                SupplierId = createProductDto.SupplierId
            };
        }

        private Product MapToProduct(ProductDto productDto)
        {
            return new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                IsVisible = productDto.IsVisible,
                CategoryId = productDto.CategoryId,
                SupplierId = productDto.SupplierId,
                Images = productDto.Images.Select(i => new Image
                {
                    ImageId = i.ImageId,
                    Url = i.Url,
                    ProductId = i.ProductId
                }).ToList()
            };
        }

        private void UpdateProductFromDto(Product product, UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Name != null)
                product.Name = updateProductDto.Name;

            if (updateProductDto.Price != 0)
                product.Price = updateProductDto.Price;

            if (updateProductDto.Description != null)
                product.Description = updateProductDto.Description;

            if (updateProductDto.IsVisible)
                product.IsVisible = updateProductDto.IsVisible;

            if (updateProductDto.CategoryId != 0)
                product.CategoryId = updateProductDto.CategoryId;

            if (updateProductDto.SupplierId != 0)
                product.SupplierId = updateProductDto.SupplierId;
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

        [HttpPost("create")]
        public async Task<ActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var product = MapToProduct(createProductDto);
            await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductWithoutIncludes), new { id = product.ProductId }, MapToDto(product));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            if (id != updateProductDto.ProductId) return BadRequest();

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            UpdateProductFromDto(product, updateProductDto);
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        [HttpPut("hide/{id}")]
        public async Task<ActionResult> HideProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            product.IsVisible = false;
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        [HttpGet("without-includes/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductWithoutIncludes(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(product));
        }
    }
}
