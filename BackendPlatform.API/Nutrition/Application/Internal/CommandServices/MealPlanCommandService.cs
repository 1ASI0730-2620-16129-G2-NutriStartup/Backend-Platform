using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.CommandServices;

public class MealPlanCommandService(IMealPlanRepository mealPlanRepository, INutritionPlanRepository nutritionPlanRepository,
    IUnitOfWork unitOfWork) : IMealPlanCommandService
{
    public async Task<Result<MealPlan>> Handle(CreateMealPlanCommand command, CancellationToken cancellationToken)
    {
        var mealPlan = new MealPlan(command);
        await mealPlanRepository.AddAsync(mealPlan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        var createdMealPlan = await mealPlanRepository
            .FindByIdAsNoTrackingAsync(mealPlan.Id, cancellationToken);
        
        if (createdMealPlan is null)
            throw new InvalidOperationException(
                "The created meal plan could not be reloaded.");
        
        return Result<MealPlan>.Success(createdMealPlan);
    }
    
    public async Task<Result<MealPlan>> Handle(UpdateMealPlanCommand command, CancellationToken cancellationToken)
    {
        var mealPlan = await mealPlanRepository.FindTrackedByIdAsync(command.MealPlanId, cancellationToken);

        if (mealPlan is null)
            return Result<MealPlan>.Failure(
                NutritionError.MealPlanNotFound,
                "Meal plan not found.");

        var nutritionPlan = await nutritionPlanRepository.FindByIdAsNoTrackingAsync(
            command.NutritionPlanId,
            cancellationToken);

        if (nutritionPlan is null)
            return Result<MealPlan>.Failure(
                NutritionError.NutritionPlanNotFound,
                "Nutrition plan not found.");

        try
        {
            mealPlan.UpdateInformation(
                command.NutritionPlanId,
                command.MealType,
                command.Description,
                command.Calories);

            await unitOfWork.CompleteAsync(cancellationToken);

            var updatedMealPlan = await mealPlanRepository.FindByIdAsNoTrackingAsync(
                command.MealPlanId,
                cancellationToken);

            if (updatedMealPlan is null)
                throw new InvalidOperationException(
                    "The updated meal plan could not be reloaded.");

            return Result<MealPlan>.Success(updatedMealPlan);
        }
        catch (ArgumentException exception)
        {
            return Result<MealPlan>.Failure(
                NutritionError.InvalidData,
                exception.Message);
        }
    }
    
    public async Task<Result<bool>> Handle(DeleteMealPlanCommand command, CancellationToken cancellationToken)
    {
        var mealPlan = await mealPlanRepository.FindTrackedByIdAsync(command.MealPlanId, cancellationToken);

        if (mealPlan is null)
            return Result<bool>.Failure(
                NutritionError.MealPlanNotFound,
                "Meal plan not found.");

        mealPlanRepository.Remove(mealPlan);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
