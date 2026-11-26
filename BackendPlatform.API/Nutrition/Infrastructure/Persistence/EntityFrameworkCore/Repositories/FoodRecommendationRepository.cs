using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class FoodRecommendationRepository(AppDbContext context) : BaseRepository<FoodRecommendation>(context), IFoodRecommendationRepository
{
    private IQueryable<FoodRecommendation> QueryWithRelatedEntities()
    {
        return Context.Set<FoodRecommendation>().AsNoTracking().Include(food => food.MealPlan) .ThenInclude(mealPlan => mealPlan.NutritionPlan);
    }

    public async Task<FoodRecommendation?> FindByIdAsNoTrackingAsync( FoodRecommendationId id, CancellationToken cancellationToken = default)
    {
        return await QueryWithRelatedEntities().FirstOrDefaultAsync(food => food.Id == id, cancellationToken);
    }

    public async Task<FoodRecommendation?> FindTrackedByIdAsync(FoodRecommendationId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<FoodRecommendation>().FirstOrDefaultAsync(food => food.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<FoodRecommendation>> ListWithRelatedEntitiesAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await QueryWithRelatedEntities().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FoodRecommendation>> FindByMealPlanIdAsNoTrackingAsync(int mealPlanId, CancellationToken cancellationToken)
    {
        return await QueryWithRelatedEntities().Where(food => food.MealPlanId == mealPlanId).ToListAsync(cancellationToken);
    }
}
