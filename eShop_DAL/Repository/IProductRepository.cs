public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(string searchText);
    Task<IEnumerable<Product>> SearchBySupplierNameAsync(string supplierName);
    Task<IEnumerable<Product>> SearchByCategoryNameAsync(string categoryName);
}
