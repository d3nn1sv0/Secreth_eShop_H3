public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(string searchText, string categoryName, string supplierName);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);

}
