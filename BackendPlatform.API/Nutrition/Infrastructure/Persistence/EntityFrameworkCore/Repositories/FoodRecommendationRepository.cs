using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class FoodRecommendationRepository(AppDbContext context)
    : BaseRepository<FoodRecommendation>(context), IFoodRecommendationRepository
{
    public async Task<IEnumerable<FoodRecommendation>> FindByMealPlanIdAsNoTrackingAsync(int mealPlanId, CancellationToken cancellationToken)
    {
        return await Context.Set<FoodRecommendation>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(foodRecommendation => foodRecommendation.MealPlan)
            .Where(foodRecommendation => foodRecommendation.MealPlanId == mealPlanId)
            .ToListAsync(cancellationToken);
    }
}
