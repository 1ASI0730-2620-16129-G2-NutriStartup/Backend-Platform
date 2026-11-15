using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.CommandServices;

public class FoodRecommendationCommandService(IFoodRecommendationRepository foodRecommendationRepository,
    IUnitOfWork unitOfWork) : IFoodRecommendationCommandService
{
    public async Task<Result<FoodRecommendation>> Handle(CreateFoodRecommendationCommand command, CancellationToken cancellationToken)
    {
        var foodRecommendation = new FoodRecommendation(command);
        await foodRecommendationRepository.AddAsync(foodRecommendation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return Result<FoodRecommendation>.Success(foodRecommendation);
    }
}
