using System.Linq.Expressions;
using Application.Common.Data;
using Domain;
using MongoDB.Driver;

namespace Infrastructure.Common.Data;

public class DocumentStore<TEntity>(IMongoDatabase database) : IDocumentStore<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity,bool>> filter)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<TEntity>> GetByIdAsync(Guid id)
    {
        return await _collection.Find(g => g.Id == id).ToListAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _collection.ReplaceOneAsync(g => g.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(g => g.Id == id);
    }
}