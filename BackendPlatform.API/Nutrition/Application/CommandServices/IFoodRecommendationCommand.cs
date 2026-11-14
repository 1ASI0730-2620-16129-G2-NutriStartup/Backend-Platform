using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Shared.Application.Model;

namespace BackendPlatform.API.Nutrition.Application.CommandServices;

public interface IFoodRecommendationCommand
{
    Task<Result<FoodRecommendation>> Handle(CreateFoodRecommendationCommand command, CancellationToken cancellationToken);
}
