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

    public async Task<Result<MealPlan>> Handle(SeedMealPlansCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var seedPlans = new[]
            {
                (
                    NutritionPlanId: 1,
                    MealType: "Desayuno",
                    Description: "Desayuno ligero y rico en fibra.",
                    Calories: 350
                ),
                (
                    NutritionPlanId: 1,
                    MealType: "Almuerzo",
                    Description: "Almuerzo balanceado con proteínas magras y vegetales.",
                    Calories: 550
                ),
                (
                    NutritionPlanId: 1,
                    MealType: "Cena",
                    Description: "Cena ligera con proteínas y vegetales.",
                    Calories: 400
                ),
                (
                    NutritionPlanId: 2,
                    MealType: "Desayuno",
                    Description: "Desayuno alto en proteínas y carbohidratos.",
                    Calories: 650
                ),
                (
                    NutritionPlanId: 2,
                    MealType: "Almuerzo",
                    Description: "Almuerzo abundante para apoyar el crecimiento muscular.",
                    Calories: 850
                ),
                (
                    NutritionPlanId: 2,
                    MealType: "Cena",
                    Description: "Cena rica en proteínas para complementar el entrenamiento.",
                    Calories: 700
                ),
                (
                    NutritionPlanId: 3,
                    MealType: "Desayuno",
                    Description: "Desayuno variado y nutritivo para comenzar el día.",
                    Calories: 450
                ),
                (
                    NutritionPlanId: 3,
                    MealType: "Almuerzo",
                    Description: "Almuerzo equilibrado con alimentos naturales.",
                    Calories: 650
                ),
                (
                    NutritionPlanId: 3,
                    MealType: "Cena",
                    Description: "Cena moderada y nutritiva.",
                    Calories: 500
                )
            };
            
            var existingPlans = (await mealPlanRepository
                    .ListAsNoTrackingAsync(cancellationToken))
                .ToList();
            
            MealPlan? firstSeededPlan = null;

            foreach (var seed in seedPlans)
            {
                var nutritionPlan = await nutritionPlanRepository
                    .FindByIdAsNoTrackingAsync(seed.NutritionPlanId, cancellationToken);
                
                if (nutritionPlan is null)
                {
                    return Result<MealPlan>.Failure(
                        NutritionError.NutritionPlanNotFound,
                        $"Nutrition plan with id {seed.NutritionPlanId} was not found.");
                }
                
                var existingPlan = existingPlans.FirstOrDefault(plan =>
                    plan.NutritionPlanId.Value == seed.NutritionPlanId &&
                    string.Equals(
                        plan.MealType,
                        seed.MealType,
                        StringComparison.OrdinalIgnoreCase));
                
                if (existingPlan is not null)
                {
                    firstSeededPlan ??= existingPlan;
                    continue;
                }
                
                var mealPlan = new MealPlan(
                    seed.NutritionPlanId,
                    seed.MealType,
                    seed.Description,
                    seed.Calories);
                
                await mealPlanRepository.AddAsync(
                    mealPlan,
                    cancellationToken);
                
                existingPlans.Add(mealPlan);

                firstSeededPlan ??= mealPlan;
            }

            await unitOfWork.CompleteAsync(cancellationToken);
            
            if (firstSeededPlan is null)
            {
                return Result<MealPlan>.Failure(
                    NutritionError.InternalServerError,
                    "No meal plans were seeded.");
            }
            
            var loadedMealPlan = await mealPlanRepository
                .FindByIdAsNoTrackingAsync(firstSeededPlan.Id, cancellationToken);
            
            return loadedMealPlan is not null
                ? Result<MealPlan>.Success(loadedMealPlan)
                : Result<MealPlan>.Failure(
                    NutritionError.InternalServerError,
                    "The seeded meal plan could not be reloaded.");
        }
        catch (OperationCanceledException)
        {
            return Result<MealPlan>.Failure(
                NutritionError.OperationCancelled, "Seeding meal plans was cancelled.");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Result<MealPlan>.Failure(
                NutritionError.DatabaseError, "A database error occurred while seeding meal plans.");
        }
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
