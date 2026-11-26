using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.Nutrition.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class NutritionPlanRepository(AppDbContext context)
    : BaseRepository<NutritionPlan>(context), INutritionPlanRepository
{
    public async Task<NutritionPlan?> FindByIdAsNoTrackingAsync(NutritionPlanId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<NutritionPlan>().AsNoTracking().FirstOrDefaultAsync(plan => plan.Id == id, cancellationToken);
    }
    
    public async Task<NutritionPlan?> FindTrackedByIdAsync(NutritionPlanId id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<NutritionPlan>().FirstOrDefaultAsync(plan => plan.Id == id, cancellationToken);
    }
}
