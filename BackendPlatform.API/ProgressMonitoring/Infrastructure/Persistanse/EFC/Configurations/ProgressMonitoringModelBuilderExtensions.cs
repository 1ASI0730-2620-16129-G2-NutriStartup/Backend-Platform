using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.ProgressMonitoring.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Entity Framework mapping for the Progress Monitoring bounded context.
/// </summary>
public static class ProgressMonitoringModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Progress Monitoring mappings. Call it from <c>AppDbContext.OnModelCreating</c>.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder" /> of the DbContext.</param>
    public static void ApplyProgressMonitoringConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ProgressEntry>(entity =>
        {
            entity.ToTable("progress_entries");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.UserId).IsRequired();
            entity.Property(p => p.RecordedAt).IsRequired();
            entity.Property(p => p.Notes).HasMaxLength(500);

            entity.OwnsOne(p => p.Measurements, m =>
            {
                m.WithOwner().HasForeignKey("Id");
                m.Property(x => x.WeightKg).HasColumnName("weight_kg").HasPrecision(5, 2).IsRequired();
                m.Property(x => x.BodyFatPercentage).HasColumnName("body_fat_percentage").HasPrecision(4, 1);
                m.Property(x => x.WaistCm).HasColumnName("waist_cm").HasPrecision(5, 1);
            });
            entity.Navigation(p => p.Measurements).IsRequired();

            // Most queries filter by user and sort by date
            entity.HasIndex(p => new { p.UserId, p.RecordedAt });
        });
    }
}