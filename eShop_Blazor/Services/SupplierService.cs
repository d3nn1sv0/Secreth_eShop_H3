using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class SupplierService
{
    private readonly HttpClient _httpClient;

    public SupplierService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Supplier>>("api/suppliers");
    }
}
