using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
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
}
