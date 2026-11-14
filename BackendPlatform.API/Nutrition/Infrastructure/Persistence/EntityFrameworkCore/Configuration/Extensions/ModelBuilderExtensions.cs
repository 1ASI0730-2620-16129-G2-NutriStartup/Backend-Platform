using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyNutritionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<NutritionPlan>().HasKey(c => c.Id);
        builder.Entity<NutritionPlan>().Property(c => c.Id)
            .HasConversion(new StronglyTypedIdConverter<NutritionPlanId, int>(id => id.Value, value => new NutritionPlanId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<NutritionPlan>().Property(c => c.Name).IsRequired().HasMaxLength(30);
        builder.Entity<NutritionPlan>().Property(c => c.Description).IsRequired().HasMaxLength(500);
        builder.Entity<NutritionPlan>().Property(c => c.Objective).IsRequired().HasMaxLength(500);
        builder.Entity<NutritionPlan>().Property(c => c.StartDate).IsRequired().HasMaxLength(500);
        builder.Entity<NutritionPlan>().Property(c => c.EndDate).IsRequired().HasMaxLength(500);
        
        builder.Entity<MealPlan>().HasKey(t => t.Id);
        builder.Entity<MealPlan>().Property(t => t.Id)
            .HasConversion(new StronglyTypedIdConverter<MealPlanId, int>(id => id.Value, value => new MealPlanId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<MealPlan>().Property(t => t.NutritionPlanId)
            .HasConversion(new StronglyTypedIdConverter<NutritionPlanId, int>(id => id.Value, value => new NutritionPlanId(value)))
            .IsRequired();
        builder.Entity<MealPlan>().Property(t => t.MealType).IsRequired().HasMaxLength(50);
        builder.Entity<MealPlan>().Property(t => t.Description).IsRequired().HasMaxLength(240);
        builder.Entity<MealPlan>().Property(t => t.Calories).IsRequired().HasMaxLength(50);
        
        builder.Entity<FoodRecommendation>().HasKey(t => t.Id);
        builder.Entity<FoodRecommendation>().Property(t => t.Id)
            .HasConversion(new StronglyTypedIdConverter<FoodRecommendationId, int>(id => id.Value, value => new FoodRecommendationId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<FoodRecommendation>().Property(t => t.MealPlanId)
            .HasConversion(new StronglyTypedIdConverter<MealPlanId, int>(id => id.Value, value => new MealPlanId(value)))
            .IsRequired();
        builder.Entity<FoodRecommendation>().Property(t => t.FoodName).IsRequired().HasMaxLength(50);
        builder.Entity<FoodRecommendation>().Property(t => t.Portion).IsRequired().HasMaxLength(240);
        builder.Entity<FoodRecommendation>().Property(t => t.NutritionalValue).IsRequired().HasMaxLength(240);
        
        builder.Entity<NutritionPlan>().HasMany(t => t.MealPlans);
        builder.Entity<MealPlan>().HasMany(t => t.FoodRecommendations);
    }
}
