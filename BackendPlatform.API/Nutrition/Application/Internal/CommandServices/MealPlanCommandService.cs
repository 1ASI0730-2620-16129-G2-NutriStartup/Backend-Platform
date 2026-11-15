using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Repositories;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.CommandServices;

public class MealPlanCommandService(IMealPlanRepository mealPlanRepository,
    IUnitOfWork unitOfWork) : IMealPlanCommandService
{
    public async Task<Result<MealPlan>> Handle(CreateMealPlanCommand command, CancellationToken cancellationToken)
    {
        var mealPlan = new MealPlan(command);
        await mealPlanRepository.AddAsync(mealPlan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        
        return Result<MealPlan>.Success(mealPlan);
    }
}
