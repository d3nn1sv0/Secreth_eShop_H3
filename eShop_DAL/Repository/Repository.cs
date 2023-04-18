using eShop_DAL.Repository;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly EShopDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(EShopDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // Retrieve all entities of type T
    public async Task<IEnumerable<T>> GetAllAsync(string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }
        }

        return await query.ToListAsync();
    }



    // Retrieve an entity of type T by its ID
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    // Add a new entity of type T to the DbSet
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Update an existing entity of type T
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // Remove an entity of type T by its ID
    public async Task DeleteAsync(int id)
    {
        T entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
