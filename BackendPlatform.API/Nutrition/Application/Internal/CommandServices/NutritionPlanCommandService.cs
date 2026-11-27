using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.CommandServices;

public class NutritionPlanCommandService(INutritionPlanRepository nutritionPlanRepository,
    IUnitOfWork unitOfWork) : INutritionPlanCommandService
{
    public async Task<Result<NutritionPlan>> Handle(CreateNutritionPlanCommand command, CancellationToken cancellationToken)
    {
        var nutritionPlan = new NutritionPlan(command);
        await nutritionPlanRepository.AddAsync(nutritionPlan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return Result<NutritionPlan>.Success(nutritionPlan);
    }

    public async Task<Result<NutritionPlan>> Handle(SeedNutritionPlansCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var seedPlans = new[]
            {
                (
                    Name: "Plan de Pérdida de Peso",
                    Description: "Plan equilibrado orientado a reducir grasa corporal de manera progresiva.",
                    Objective: "Pérdida de peso",
                    StartDate: "01/10/2026",
                    EndDate: "31/12/2026",
                    Status: NutritionPlanStatus.Active
                ),
                (
                    Name: "Plan de Ganancia Muscular",
                    Description: "Plan con mayor aporte energético y proteico para favorecer el desarrollo muscular.",
                    Objective: "Ganancia muscular",
                    StartDate: "01/10/2026",
                    EndDate: "31/01/2027",
                    Status: NutritionPlanStatus.Active
                )
                ,
                (
                    Name: "Plan de Alimentación Saludable",
                    Description: "Plan variado basado en alimentos naturales y una distribución equilibrada de nutrientes.",
                    Objective: "Mantener una alimentación saludable",
                    StartDate: "01/10/2026",
                    EndDate: "31/12/2026",
                    Status: NutritionPlanStatus.Inactive
                )
            };
            
            var existingPlans = await nutritionPlanRepository.ListAsNoTrackingAsync(cancellationToken); 
            
            NutritionPlan? firstSeededPlan = null;
            
            foreach (var seed in seedPlans)
            {
                var existingPlan = existingPlans.FirstOrDefault(plan => 
                    string.Equals(
                        plan.Name,
                        seed.Name,
                        StringComparison.OrdinalIgnoreCase));
                
                if (existingPlan is not null)
                {
                    firstSeededPlan ??= existingPlan;
                    continue;
                }

                var nutritionPlan = new NutritionPlan(
                    seed.Name,
                    seed.Description,
                    seed.Objective,
                    seed.StartDate,
                    seed.EndDate,
                    seed.Status);

                await nutritionPlanRepository.AddAsync(
                    nutritionPlan,
                    cancellationToken);

                firstSeededPlan ??= nutritionPlan;
            }
            
            await unitOfWork.CompleteAsync(cancellationToken);

            return firstSeededPlan is not null
                ? Result<NutritionPlan>.Success(firstSeededPlan)
                : Result<NutritionPlan>.Failure(
                    NutritionError.InternalServerError, "No nutrition plans were seeded.");
        }
        catch (OperationCanceledException)
        {
            return Result<NutritionPlan>.Failure(
                NutritionError.OperationCancelled, "Seeding nutrition plans was cancelled.");
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            return Result<NutritionPlan>.Failure(
                NutritionError.DatabaseError, "A database error occurred while seeding nutrition plans.");
        }
    }

    public async Task<Result<NutritionPlan>> Handle(UpdateNutritionPlanCommand command, CancellationToken cancellationToken)
    {
        var nutritionPlan = await nutritionPlanRepository.FindTrackedByIdAsync(command.NutritionPlanId, cancellationToken);

        if (nutritionPlan is null)
            return Result<NutritionPlan>.Failure(
                NutritionError.NutritionPlanNotFound,
                "Nutrition plan not found.");

        try
        {
            nutritionPlan.UpdateInformation(
                command.Name,
                command.Description,
                command.Objective,
                command.StartDate,
                command.EndDate,
                command.Status);

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<NutritionPlan>.Success(nutritionPlan);
        }
        catch (ArgumentException exception)
        {
            return Result<NutritionPlan>.Failure(
                NutritionError.InvalidData,
                exception.Message);
        }
    }
    
    public async Task<Result<bool>> Handle(DeleteNutritionPlanCommand command, CancellationToken cancellationToken)
    {
        var nutritionPlan = await nutritionPlanRepository.FindTrackedByIdAsync(command.NutritionPlanId, cancellationToken);

        if (nutritionPlan is null)
            return Result<bool>.Failure(
                NutritionError.NutritionPlanNotFound,
                "Nutrition plan not found.");

        nutritionPlanRepository.Remove(nutritionPlan);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
