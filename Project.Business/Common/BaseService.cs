using Microsoft.EntityFrameworkCore;
using Project.Domain;

namespace Project.Business.Common;

public class BaseService<TEntity>(DbContext context) where TEntity : BaseEntity
{
    private readonly DbContext _context = context;

    public async virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        _context!.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context!.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async virtual Task<bool> DeleteAsync(TEntity entity)
    {
        _context!.Set<TEntity>().Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public virtual async Task<bool> DeleteByIdAsync(Guid id)
    {
        var entity = await _context!.Set<TEntity>().FindAsync(id);
        _context!.Set<TEntity>().Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context!.Set<TEntity>().FindAsync(id);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _context!.Set<TEntity>().AsQueryable();
    }
}
