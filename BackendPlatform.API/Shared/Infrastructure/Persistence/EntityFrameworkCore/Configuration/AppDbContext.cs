using BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using BackendPlatform.API.ProgressMonitoring.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using BackendPlatform.API.UserProfileManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Nutrition BC
        builder.ApplyNutritionConfiguration();
        
        // User Profile Management BC
        builder.ApplyUserProfileManagementConfiguration();
        
        // Progress Monitoring BC
        builder.ApplyProgressMonitoringConfiguration();
        
        builder.UseSnakeCaseNamingConvention();
    }
}
