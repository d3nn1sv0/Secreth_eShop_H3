public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(string searchText, string categoryName, string supplierName);
}
