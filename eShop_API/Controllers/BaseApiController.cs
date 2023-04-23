using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
public abstract class BaseApiController<T> : ControllerBase where T : class
{
    private readonly IRepository<T> _repository;

    protected BaseApiController(IRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<T>> CreateAsync([FromBody] T entity)
    {
        await _repository.CreateAsync(entity);

        int entityId = GetEntityId(entity);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = entityId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] T entity)
    {
        // Add a check to make sure the entity exists and the provided ID matches the entity ID
        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }

    private int GetEntityId(T entity)
    {
        var keyProperty = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0);
        if (keyProperty == null)
        {
            throw new ArgumentException("No Key attribute found on the Type");
        }

        return (int)keyProperty.GetValue(entity);
    }
}
