using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Business.Common.Data;

public class Context(DbContextOptions<Context> options) : IdentityDbContext(options)
{

    // Example table
    public DbSet<Student> Students { get; set; } = default!;
}
