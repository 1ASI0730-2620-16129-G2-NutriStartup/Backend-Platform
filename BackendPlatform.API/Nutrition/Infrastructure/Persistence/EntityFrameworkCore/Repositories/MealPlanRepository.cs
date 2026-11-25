using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MealPlanRepository(AppDbContext context) : BaseRepository<MealPlan>(context), IMealPlanRepository
{
    public async Task<MealPlan?> FindByIdAsNoTrackingAsync( MealPlanId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<MealPlan>() .AsNoTracking() .Include(mealPlan => mealPlan.NutritionPlan) .FirstOrDefaultAsync(mealPlan => mealPlan.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<MealPlan>> ListWithNutritionPlanAsNoTrackingAsync( CancellationToken cancellationToken = default)
    {
        return await Context.Set<MealPlan>() .AsNoTracking() .Include(mealPlan => mealPlan.NutritionPlan) .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MealPlan>> FindByNutritionPlanIdAsNoTrackingAsync( int nutritionPlanId, CancellationToken cancellationToken)
    {
        return await Context.Set<MealPlan>() .AsNoTracking() .Include(mealPlan => mealPlan.NutritionPlan) .Where(mealPlan => mealPlan.NutritionPlanId == nutritionPlanId) .ToListAsync(cancellationToken);
    }
}
