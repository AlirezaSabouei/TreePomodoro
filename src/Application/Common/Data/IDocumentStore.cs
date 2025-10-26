using System.Linq.Expressions;
using Domain;
using MongoDB.Bson;

namespace Application.Common.Data;

public interface IDocumentStore<TEntity> where TEntity: BaseEntity
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity,bool>> filter);
    Task<List<TEntity>> GetByIdAsync(Guid id);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}