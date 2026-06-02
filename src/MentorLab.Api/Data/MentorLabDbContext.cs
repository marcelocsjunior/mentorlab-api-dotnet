using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Data;

public class MentorLabDbContext : DbContext
{
    public MentorLabDbContext(DbContextOptions<MentorLabDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(student => student.Id);
            entity.Property(student => student.FullName).IsRequired().HasMaxLength(160);
            entity.Property(student => student.Email).IsRequired().HasMaxLength(180);
            entity.Property(student => student.Phone).HasMaxLength(40);
            entity.HasIndex(student => student.Email).IsUnique();
        });
    }
}
