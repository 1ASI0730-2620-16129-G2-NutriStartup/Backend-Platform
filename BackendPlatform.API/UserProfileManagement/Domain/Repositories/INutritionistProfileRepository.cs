using BackendPlatform.API.Shared.Domain.Repositories;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

namespace BackendPlatform.API.UserProfileManagement.Domain.Repositories;

public interface INutritionistProfileRepository : IBaseRepository<NutritionistProfile>
{
    Task<NutritionistProfile?> FindByIdAsNoTrackingAsync(int nutritionistProfileId, CancellationToken cancellationToken = default);
    
    Task<NutritionistProfile?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    
    Task<NutritionistProfile?> FindByUserIdAsNoTrackingAsync(string userId, CancellationToken cancellationToken = default);
}
