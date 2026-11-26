using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Shared.Application.Model;

namespace BackendPlatform.API.Nutrition.Application.CommandServices;

public interface IFoodRecommendationCommandService
{
    Task<Result<FoodRecommendation>> Handle(CreateFoodRecommendationCommand command, CancellationToken cancellationToken);
    
    Task<Result<bool>> Handle(DeleteFoodRecommendationCommand command, CancellationToken cancellationToken);
}
