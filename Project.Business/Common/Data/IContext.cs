using Microsoft.EntityFrameworkCore.Storage;
using Project.Domain;

namespace Project.Business.Common.Data;

public interface IContext
{
    IQueryable<TEntity> QuerySet<TEntity>()
        where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
    Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task RemoveEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task<IDbContextTransaction> BeginTransaction();
}