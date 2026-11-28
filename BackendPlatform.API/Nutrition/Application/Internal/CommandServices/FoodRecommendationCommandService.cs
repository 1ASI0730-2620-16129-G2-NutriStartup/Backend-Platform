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
    
    public async Task<Result<FoodRecommendation>> Handle(SeedFoodRecommendationsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var seedRecommendations = new[]
            {
                (
                    MealPlanId: 1,
                    FoodName: "Avena",
                    Portion: "50 g",
                    NutritionalValue: "190 kcal, 7 g de proteína, 32 g de carbohidratos"
                ),
                (
                    MealPlanId: 1,
                    FoodName: "Plátano",
                    Portion: "1 unidad",
                    NutritionalValue: "105 kcal, 1 g de proteína, 27 g de carbohidratos"
                ),
                (
                    MealPlanId: 2,
                    FoodName: "Pechuga de pollo",
                    Portion: "150 g",
                    NutritionalValue: "250 kcal, 46 g de proteína, 0 g de carbohidratos"
                ),
                (
                    MealPlanId: 2,
                    FoodName: "Arroz integral",
                    Portion: "120 g cocido",
                    NutritionalValue: "135 kcal, 3 g de proteína, 28 g de carbohidratos"
                ),
                (
                    MealPlanId: 3,
                    FoodName: "Ensalada de verduras",
                    Portion: "200 g",
                    NutritionalValue: "80 kcal, 3 g de proteína, 14 g de carbohidratos"
                ),
                (
                    MealPlanId: 3,
                    FoodName: "Atún",
                    Portion: "100 g",
                    NutritionalValue: "130 kcal, 29 g de proteína, 0 g de carbohidratos"
                ),
                (
                    MealPlanId: 4,
                    FoodName: "Huevos",
                    Portion: "3 unidades",
                    NutritionalValue: "210 kcal, 18 g de proteína, 1 g de carbohidratos"
                ),
                (
                    MealPlanId: 4,
                    FoodName: "Pan integral",
                    Portion: "3 rebanadas",
                    NutritionalValue: "210 kcal, 9 g de proteína, 36 g de carbohidratos"
                ),
                (
                    MealPlanId: 5,
                    FoodName: "Pechuga de pollo",
                    Portion: "200 g",
                    NutritionalValue: "330 kcal, 62 g de proteína, 0 g de carbohidratos"
                ),
                (
                    MealPlanId: 5,
                    FoodName: "Arroz blanco",
                    Portion: "250 g cocido",
                    NutritionalValue: "325 kcal, 7 g de proteína, 70 g de carbohidratos"
                ),
                (
                    MealPlanId: 5,
                    FoodName: "Palta",
                    Portion: "100 g",
                    NutritionalValue: "160 kcal, 2 g de proteína, 9 g de carbohidratos"
                ),
                (
                    MealPlanId: 6,
                    FoodName: "Carne magra",
                    Portion: "180 g",
                    NutritionalValue: "360 kcal, 47 g de proteína, 0 g de carbohidratos"
                ),
                (
                    MealPlanId: 6,
                    FoodName: "Papa sancochada",
                    Portion: "200 g",
                    NutritionalValue: "174 kcal, 4 g de proteína, 40 g de carbohidratos"
                ),
                (
                    MealPlanId: 7,
                    FoodName: "Yogur natural",
                    Portion: "200 g",
                    NutritionalValue: "120 kcal, 10 g de proteína, 12 g de carbohidratos"
                ),
                (
                    MealPlanId: 7,
                    FoodName: "Frutos rojos",
                    Portion: "100 g",
                    NutritionalValue: "50 kcal, 1 g de proteína, 12 g de carbohidratos"
                ),
                (
                    MealPlanId: 8,
                    FoodName: "Pescado a la plancha",
                    Portion: "180 g",
                    NutritionalValue: "220 kcal, 42 g de proteína, 0 g de carbohidratos"
                ),
                (
                    MealPlanId: 8,
                    FoodName: "Quinua",
                    Portion: "150 g cocida",
                    NutritionalValue: "180 kcal, 7 g de proteína, 32 g de carbohidratos"
                ),
                (
                    MealPlanId: 9,
                    FoodName: "Sopa de verduras",
                    Portion: "300 ml",
                    NutritionalValue: "120 kcal, 4 g de proteína, 20 g de carbohidratos"
                ),
                (
                    MealPlanId: 9,
                    FoodName: "Huevo cocido",
                    Portion: "2 unidades",
                    NutritionalValue: "140 kcal, 12 g de proteína, 1 g de carbohidratos"
                )
            };
            
            var existingRecommendations = (await foodRecommendationRepository
                    .ListAsNoTrackingAsync(cancellationToken))
                .ToList();
            
            FoodRecommendation? firstSeededRecommendation = null;

            foreach (var seed in seedRecommendations)
            {
                var mealPlan = await mealPlanRepository
                    .FindByIdAsNoTrackingAsync(seed.MealPlanId, cancellationToken);
                
                if (mealPlan is null)
                {
                    return Result<FoodRecommendation>.Failure(
                        NutritionError.MealPlanNotFound,
                        $"Meal plan with id {seed.MealPlanId} was not found.");
                }
                
                var existingRecommendation = existingRecommendations.FirstOrDefault(recommendation =>
                    recommendation.MealPlanId.Value == seed.MealPlanId &&
                    string.Equals(
                        recommendation.FoodName,
                        seed.FoodName,
                        StringComparison.OrdinalIgnoreCase));
                
                if (existingRecommendation is not null)
                {
                    firstSeededRecommendation ??= existingRecommendation;
                    continue;
                }
                
                var recommendation = new FoodRecommendation(
                    seed.MealPlanId,
                    seed.FoodName,
                    seed.Portion,
                    seed.NutritionalValue);
                
                await foodRecommendationRepository.AddAsync(
                    recommendation,
                    cancellationToken);
                
                existingRecommendations.Add(recommendation);
                
                firstSeededRecommendation ??= recommendation;
            }
            
            await unitOfWork.CompleteAsync(cancellationToken);
            
            if (firstSeededRecommendation is null)
            {
                return Result<FoodRecommendation>.Failure(
                    NutritionError.InternalServerError,
                    "No food recommendations were seeded.");
            }
            
            var loadedRecommendation = await foodRecommendationRepository
                .FindByIdAsNoTrackingAsync(firstSeededRecommendation.Id, cancellationToken);

            return loadedRecommendation is not null
                ? Result<FoodRecommendation>.Success(loadedRecommendation)
                : Result<FoodRecommendation>.Failure(
                    NutritionError.InternalServerError,
                    "The seeded food recommendation could not be reloaded.");
        }
        catch (OperationCanceledException)
        {
            return Result<FoodRecommendation>.Failure(
                NutritionError.OperationCancelled, "Seeding food recommendations was cancelled.");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Result<FoodRecommendation>.Failure(
                NutritionError.DatabaseError, "A database error occurred while seeding food recommendations.");
        }
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
