using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Shared.Application.Model;

namespace BackendPlatform.API.Nutrition.Application.CommandServices;

public interface IMealPlanCommandService
{
    Task<Result<MealPlan>> Handle(CreateMealPlanCommand command, CancellationToken cancellationToken);
    
    Task<Result<MealPlan>> Handle(UpdateMealPlanCommand command, CancellationToken cancellationToken);
    
    Task<Result<bool>> Handle(DeleteMealPlanCommand command, CancellationToken cancellationToken);
}
