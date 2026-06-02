using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Data;

public class MentorLabDbContext : DbContext
{
    public MentorLabDbContext(DbContextOptions<MentorLabDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    public DbSet<LearningTrack> LearningTracks => Set<LearningTrack>();

    public DbSet<Module> Modules => Set<Module>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(student => student.Id);

            entity.Property(student => student.Id)
                .ValueGeneratedOnAdd();

            entity.Property(student => student.FullName)
                .IsRequired()
                .HasMaxLength(160);

            entity.Property(student => student.Email)
                .IsRequired()
                .HasMaxLength(180);

            entity.Property(student => student.IsActive)
                .IsRequired();

            entity.Property(student => student.CreatedAt)
                .IsRequired();

            entity.HasIndex(student => student.Email);
        });

        modelBuilder.Entity<LearningTrack>(entity =>
        {
            entity.HasKey(track => track.Id);

            entity.Property(track => track.Id)
                .ValueGeneratedOnAdd();

            entity.Property(track => track.Title)
                .IsRequired()
                .HasMaxLength(160);

            entity.Property(track => track.Description)
                .HasMaxLength(500);

            entity.Property(track => track.IsActive)
                .IsRequired();

            entity.Property(track => track.CreatedAt)
                .IsRequired();

            entity.HasMany(track => track.Modules)
                .WithOne(module => module.LearningTrack)
                .HasForeignKey(module => module.LearningTrackId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(module => module.Id);

            entity.Property(module => module.Id)
                .ValueGeneratedOnAdd();

            entity.Property(module => module.Title)
                .IsRequired()
                .HasMaxLength(160);

            entity.Property(module => module.Description)
                .HasMaxLength(500);

            entity.Property(module => module.Order)
                .IsRequired()
                .HasColumnName("DisplayOrder");

            entity.Property(module => module.IsActive)
                .IsRequired();

            entity.Property(module => module.CreatedAt)
                .IsRequired();

            entity.HasIndex(module => module.LearningTrackId);
        });
    }
}
