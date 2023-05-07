using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var response = await _httpClient.GetAsync("api/products");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API Response: " + content);

            var products = JsonSerializer.Deserialize<List<Product>>(content);

            return products;
        }
        else
        {
            Console.WriteLine("API Error: " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<Product>();
        }
    }

    public async Task<Product> GetProductIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7094/api/products/{id}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Response: " + content);

            var product = JsonSerializer.Deserialize<Product>(content);
            return product;
        }
        else
        {
            Console.WriteLine("API Error: " + response.StatusCode + " - " + response.ReasonPhrase);
            throw new ApplicationException($"Error fetching product with ID {id}: {response.ReasonPhrase}");
        }
    }

    public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchText, string categoryName, string supplierName)
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(searchText))
            queryParams.Add($"searchText={searchText}");

        if (!string.IsNullOrEmpty(categoryName))
            queryParams.Add($"categoryName={categoryName}");

        if (!string.IsNullOrEmpty(supplierName))
            queryParams.Add($"supplierName={supplierName}");

        string queryString = string.Join("&", queryParams);
        string apiUrl = $"api/products/search?{queryString}";

        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content);
            return products;
        }
        else
        {
            Console.WriteLine("API Error: " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<Product>();
        }
    }

    public async Task UpdateProductAsync(Product updatedProduct)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/products/{updatedProduct.ProductId}", updatedProduct);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("API Error: " + response.StatusCode + " - " + response.ReasonPhrase);
            throw new ApplicationException($"Error updating product with ID {updatedProduct.ProductId}: {response.ReasonPhrase}");
        }
    }




}
