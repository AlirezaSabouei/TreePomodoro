using Application.Common.Data;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Entities.Gardens;

namespace Infrastructure.Common.Data;

public class Context(DbContextOptions<Context> options, IMediator mediator) : IdentityDbContext(options), IContext
{
    // Example table
    public DbSet<Student> Students { get; set; }
    public DbSet<Garden> Gardens { get; set; }
    
    public async Task SaveChangesAsync()
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
        await base.SaveChangesAsync();
        await _dispatchDomainEvents();
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
    
    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var entityDomainEvent in events)
                await mediator.Publish(entityDomainEvent);
        }
    }
}