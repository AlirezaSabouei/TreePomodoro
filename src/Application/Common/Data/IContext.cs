using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Data;

public interface IContext
{
    public DbSet<Student> Students { get; set; }
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransaction();
}