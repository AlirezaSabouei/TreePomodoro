using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Student));

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
