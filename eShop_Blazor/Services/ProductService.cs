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

        // Check if the response was successful
        if (response.IsSuccessStatusCode)
        {
            // Read the content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Print the content to the console (for debugging)
            Console.WriteLine("API Response: " + content);

            // Deserialize the content to a list of products
            var products = JsonSerializer.Deserialize<List<Product>>(content);

            return products;
        }
        else
        {
            // Handle the error case here (e.g., throw an exception, return an empty list, etc.)
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



}
