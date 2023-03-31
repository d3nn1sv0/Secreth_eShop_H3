using System.Collections;
namespace eShop_UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestSearchAsync()
        {
            using var context = TestDbContext.CreateContext();
            var productRepository = new ProductRepository(context);

            // Test searching by product name
            string productName = context.Products.First().Name;
            var searchResult1 = await productRepository.SearchAsync(productName);
            Assert.NotEmpty(searchResult1);

            // Test searching by a part of the product description
            string productDescriptionPart = context.Products.First().Description.Substring(0, 5);
            var searchResult2 = await productRepository.SearchAsync(productDescriptionPart);
            Assert.NotEmpty(searchResult2);

            // Test searching with an empty query (should return all products)
            var searchResult3 = await productRepository.SearchAsync("");
            Assert.Equal(context.Products.Count(), searchResult3.Count());

            // Test searching with a non-existent term (should return an empty result)
            var searchResult4 = await productRepository.SearchAsync("Non-existent term");
            Assert.Empty(searchResult4);

            // Test searching by category name
            string categoryName = context.Categories.First().Name;
            var searchResult5 = await productRepository.SearchByCategoryNameAsync(categoryName);
            Assert.NotEmpty(searchResult5);

            // Test searching by supplier name
            string supplierName = context.Suppliers.First().Name;
            var searchResult6 = await productRepository.SearchBySupplierNameAsync(supplierName);
            Assert.NotEmpty(searchResult6);
        }
    }
}