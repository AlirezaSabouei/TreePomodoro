using System.Linq.Expressions;
using Application.Common.Data;
using Domain;
using MediatR;
using MongoDB.Driver;

namespace Infrastructure.Common.Data;

public class DocumentStore<TEntity>(IMongoDatabase database, IMediator mediator)
    : IDocumentStore<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(g => g.Id == id).FirstOrDefaultAsync(cancellationToken);
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
        var entity = await _collection.FindOneAndDeleteAsync(g => g.Id == id);
    }
}