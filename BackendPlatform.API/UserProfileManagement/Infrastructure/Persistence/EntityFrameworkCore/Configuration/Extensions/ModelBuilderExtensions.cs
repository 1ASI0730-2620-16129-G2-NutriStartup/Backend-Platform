using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.UserProfileManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyUserProfileManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<PatientProfile>().HasKey(c => c.Id);
        
        builder.Entity<PatientProfile>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<PatientProfile>().Property(c => c.UserId).IsRequired().HasMaxLength(120);

        builder.Entity<PatientProfile>().Property(c => c.FirstName).IsRequired().HasMaxLength(80);

        builder.Entity<PatientProfile>().Property(c => c.LastName).IsRequired().HasMaxLength(80);

        builder.Entity<PatientProfile>().Property(c => c.Age).IsRequired();

        builder.Entity<PatientProfile>().Property(c => c.Gender).IsRequired().HasMaxLength(30);

        builder.Entity<PatientProfile>().HasIndex(c => c.UserId).IsUnique();

        
        
        var measurements = builder.Entity<PatientProfile>().OwnsOne(c => c.Measurements);

        measurements.Property(c => c.WeightKg).IsRequired().HasPrecision(5, 2);

        measurements.Property(c => c.HeightMeters).IsRequired().HasPrecision(3, 2);

        measurements.Ignore(c => c.Bmi);

        builder.Entity<PatientProfile>().Navigation(c => c.Measurements).IsRequired();

        

        builder.Entity<PatientProfile>().HasMany(c => c.Goals).WithOne(c => c.PatientProfile).HasForeignKey(c => c.PatientProfileId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Goal>().HasKey(c => c.Id);
        
        builder.Entity<Goal>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Goal>().Property(c => c.PatientProfileId).IsRequired();

        builder.Entity<Goal>().Property(c => c.Type).IsRequired().HasMaxLength(80);

        builder.Entity<Goal>().Property(c => c.TargetWeight).IsRequired().HasPrecision(5, 2);

        builder.Entity<Goal>().Property(c => c.TargetDate).IsRequired();

        builder.Entity<Goal>().Property(c => c.Status).IsRequired().HasMaxLength(40);



        builder.Entity<PatientProfile>().HasMany(c => c.Preferences).WithOne(c => c.PatientProfile).HasForeignKey(c => c.PatientProfileId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Preference>().HasKey(c => c.Id);
        
        builder.Entity<Preference>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Preference>().Property(c => c.PatientProfileId).IsRequired();

        builder.Entity<Preference>().Property(c => c.DietaryPreferences).IsRequired().HasMaxLength(500);

        builder.Entity<Preference>().Property(c => c.FavoriteFoods).IsRequired().HasMaxLength(500);

        

        builder.Entity<PatientProfile>().HasMany(c => c.Restrictions).WithOne(c => c.PatientProfile).HasForeignKey(c => c.PatientProfileId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Restriction>().HasKey(c => c.Id);
        
        builder.Entity<Restriction>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Restriction>().Property(c => c.PatientProfileId).IsRequired();

        builder.Entity<Restriction>().Property(c => c.Type).IsRequired().HasMaxLength(80);

        builder.Entity<Restriction>().Property(c => c.Description).IsRequired().HasMaxLength(500);

        

        builder.Entity<NutritionistProfile>().HasKey(c => c.Id);
        
        builder.Entity<NutritionistProfile>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<NutritionistProfile>().Property(c => c.UserId).IsRequired().HasMaxLength(120);

        builder.Entity<NutritionistProfile>().Property(c => c.Specialty).IsRequired().HasMaxLength(120);
        
        builder.Entity<NutritionistProfile>().Property(c => c.ExperienceYears).IsRequired();

        builder.Entity<NutritionistProfile>().HasIndex(c => c.UserId).IsUnique();
    }
}
