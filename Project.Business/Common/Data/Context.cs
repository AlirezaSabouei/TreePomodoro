using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Domain;
using Project.Domain.Entities;

namespace Project.Business.Common.Data;

public class Context(DbContextOptions<Context> options) : IdentityDbContext(options), IContext
{
    // Example table
    public DbSet<Student> Students { get; set; }

    public async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        await base.Set<TEntity>().AddAsync(entity);
    }
    
    public Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
    
    public Task RemoveEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        base.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
    
    public IQueryable<TEntity> QuerySet<TEntity>() where TEntity : BaseEntity
    {
        return base.Set<TEntity>();
    }

    public Task<int> SaveChangesAsync()
    {
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateDate = utcNow;
                entry.Entity.UpdateDate = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdateDate = utcNow;
                
                // prevent overwriting CreatedDate on updates
                entry.Property(e => e.CreateDate).IsModified = false;
            }
        }
        return base.SaveChangesAsync();
    }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     builder.Entity<Sample>().OwnsOne(a => a.Cost, ConfigureUnitCost);
    //     base.OnModelCreating(builder);
    // }

    // private void ConfigureSampleCost(OwnedNavigationBuilder<Sample, Cost> ownedNavigationBuilder)
    // {
    //     ownedNavigationBuilder
    //         .Property(a => a.Food)
    //         .HasColumnName(nameof(Unit.Cost) + nameof(Cost.Food))
    //         .IsRequired();
    //     
    //     ownedNavigationBuilder
    //         .Property(a => a.Lumber)
    //         .HasColumnName(nameof(Unit.Cost) + nameof(Cost.Lumber))
    //         .IsRequired();
    //     
    //     ownedNavigationBuilder
    //         .Property(a => a.Stone)
    //         .HasColumnName(nameof(Unit.Cost) + nameof(Cost.Stone))
    //         .IsRequired();
    //     
    //     ownedNavigationBuilder
    //         .Property(a => a.Metal)
    //         .HasColumnName(nameof(Unit.Cost) + nameof(Cost.Metal))
    //         .IsRequired();
    //     
    //     ownedNavigationBuilder
    //         .Property(a => a.Gold)
    //         .HasColumnName(nameof(Unit.Cost) + nameof(Cost.Gold))
    //         .IsRequired();
    // }

    public Task<IDbContextTransaction> BeginTransaction()
    {
        return Database.BeginTransactionAsync();
    }
    
    public Task CommitTransaction()
    {
        return Database.CommitTransactionAsync();
    }
    
    public Task RollBackTransaction()
    {
        return Database.RollbackTransactionAsync();
    }
}