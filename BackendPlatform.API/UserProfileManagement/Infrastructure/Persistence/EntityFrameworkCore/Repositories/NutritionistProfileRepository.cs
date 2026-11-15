using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.UserProfileManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class NutritionistProfileRepository(AppDbContext context) : BaseRepository<NutritionistProfile>(context), INutritionistProfileRepository
{
    public Task<NutritionistProfile?> FindByIdAsNoTrackingAsync(int nutritionistProfileId, CancellationToken cancellationToken = default) =>
        Context.Set<NutritionistProfile>()
            .AsNoTracking()
            .FirstOrDefaultAsync(profile => profile.Id == nutritionistProfileId, cancellationToken);

    public Task<NutritionistProfile?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default) =>
        Context.Set<NutritionistProfile>()
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);

    public Task<NutritionistProfile?> FindByUserIdAsNoTrackingAsync(string userId, CancellationToken cancellationToken = default) =>
        Context.Set<NutritionistProfile>()
            .AsNoTracking()
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);
}
