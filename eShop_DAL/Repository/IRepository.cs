using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string includeProperties = null);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdAsyncWithIncludes(int id, string includeProperties);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
