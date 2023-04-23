using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eShop_DAL.Repository;

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

        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        /// <returns>A list of Product objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product</param>
        /// <returns>A Product object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Searches for products by search text, category name, and/or supplier name.
        /// </summary>
        /// <param name="searchText">The search text to filter products by</param>
        /// <param name="categoryName">The category name to filter products by (optional)</param>
        /// <param name="supplierName">The supplier name to filter products by (optional)</param>
        /// <returns>A list of Product objects that match the search criteria</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(
            [FromQuery] string searchText,
            [FromQuery] string categoryName = null,
            [FromQuery] string supplierName = null)
        {
            var products = await _productRepository.SearchAsync(searchText, categoryName, supplierName);
            return Ok(products);
        }

    }
}
