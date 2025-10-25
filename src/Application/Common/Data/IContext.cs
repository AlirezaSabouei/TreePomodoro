using Domain.Entities;
using Domain.Entities.Gardens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Data;

public interface IContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Garden> Gardens { get; set; }
    
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransaction();
}