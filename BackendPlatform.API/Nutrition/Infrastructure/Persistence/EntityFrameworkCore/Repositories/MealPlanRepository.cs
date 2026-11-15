using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MealPlanRepository(AppDbContext context)
    : BaseRepository<MealPlan>(context), IMealPlanRepository
{
    public async Task<IEnumerable<MealPlan>> FindByNutritionPlanIdAsNoTrackingAsync(int nutritionPlanId, CancellationToken cancellationToken)
    {
        return await Context.Set<MealPlan>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(mealPlan => mealPlan.NutritionPlan)
            .Where(mealPlan => mealPlan.NutritionPlanId == nutritionPlanId)
            .ToListAsync(cancellationToken);
    }
}
