using System.Linq.Expressions;
using Domain;
using MongoDB.Bson;

namespace Application.Common.Data;

public interface IDocumentStore<TEntity> where TEntity: BaseEntity
{
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity,bool>> filter, CancellationToken token = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}