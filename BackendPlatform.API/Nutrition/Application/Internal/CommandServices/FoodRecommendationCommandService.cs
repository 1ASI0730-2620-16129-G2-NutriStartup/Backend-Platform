using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.CommandServices;

public class FoodRecommendationCommandService(IFoodRecommendationRepository foodRecommendationRepository,
    IMealPlanRepository mealPlanRepository, IUnitOfWork unitOfWork) : IFoodRecommendationCommandService
{
    public async Task<Result<FoodRecommendation>> Handle(CreateFoodRecommendationCommand command, CancellationToken cancellationToken)
    {
        var foodRecommendation = new FoodRecommendation(command);
        await foodRecommendationRepository.AddAsync(foodRecommendation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        var createdFoodRecommendation = await foodRecommendationRepository
            .FindByIdAsNoTrackingAsync(foodRecommendation.Id, cancellationToken);
        
        if (createdFoodRecommendation is null)
            throw new InvalidOperationException(
                "The created food recommendation could not be reloaded.");
        
        return Result<FoodRecommendation>.Success(createdFoodRecommendation);
    }
    
    public async Task<Result<FoodRecommendation>> Handle(UpdateFoodRecommendationCommand command, CancellationToken cancellationToken)
    {
        var foodRecommendation = await foodRecommendationRepository.FindTrackedByIdAsync(command.FoodRecommendationId, cancellationToken);

        if (foodRecommendation is null)
            return Result<FoodRecommendation>.Failure(
                NutritionError.FoodRecommendationNotFound,
                "Food recommendation not found.");

        var mealPlan = await mealPlanRepository.FindByIdAsNoTrackingAsync(
            command.MealPlanId,
            cancellationToken);

        if (mealPlan is null)
            return Result<FoodRecommendation>.Failure(
                NutritionError.MealPlanNotFound,
                "Meal plan not found.");

        try
        {
            foodRecommendation.UpdateInformation(
                command.MealPlanId,
                command.FoodName,
                command.Portion,
                command.NutritionalValue);

            await unitOfWork.CompleteAsync(cancellationToken);

            var updatedRecommendation =
                await foodRecommendationRepository.FindByIdAsNoTrackingAsync(
                    command.FoodRecommendationId,
                    cancellationToken);

            if (updatedRecommendation is null)
                throw new InvalidOperationException(
                    "The updated food recommendation could not be reloaded.");

            return Result<FoodRecommendation>.Success(updatedRecommendation);
        }
        catch (ArgumentException exception)
        {
            return Result<FoodRecommendation>.Failure(
                NutritionError.InvalidData,
                exception.Message);
        }
    }
    
    public async Task<Result<bool>> Handle(DeleteFoodRecommendationCommand command, CancellationToken cancellationToken)
    {
        var foodRecommendation = await foodRecommendationRepository.FindTrackedByIdAsync(command.FoodRecommendationId, cancellationToken);

        if (foodRecommendation is null)
            return Result<bool>.Failure(
                NutritionError.FoodRecommendationNotFound,
                "Food Recommendation not found.");

        foodRecommendationRepository.Remove(foodRecommendation);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
