using Microsoft.EntityFrameworkCore;
using Project.Business.Common.Data;
using Project.Domain;

namespace Project.Business.Common;

public class BaseService<TEntity>(IContext context) where TEntity : BaseEntity
{
    protected async Task CreateAsync(TEntity entity)
    {
        await context.AddEntityAsync(entity);
        await context.SaveChangesAsync();
    }

    protected async Task<TEntity> CreateIfNotExistsAsync(TEntity entity)
    {
        if (context.QuerySet<TEntity>().Any(a => a.Id == entity.Id))
        {
            return entity;
        }

        await context!.AddEntityAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    protected async Task<TEntity> UpdateAsync(TEntity entity)
    {
        //await context!.UpdateEntityAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    protected async Task<bool> DeleteAsync(TEntity entity)
    {
        await context!.RemoveEntityAsync(entity);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    protected async Task<bool> DeleteByIdAsync(Guid id)
    {
        var entity = await context!.QuerySet<TEntity>().FirstOrDefaultAsync(a => a.Id == id);
        if (entity == null) return false;
        await context!.RemoveEntityAsync(entity);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    protected async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await context!.QuerySet<TEntity>().FirstOrDefaultAsync(a => a.Id == id);
    }

    protected IQueryable<TEntity> GetAll()
    {
        return context!.QuerySet<TEntity>();
    }
}
