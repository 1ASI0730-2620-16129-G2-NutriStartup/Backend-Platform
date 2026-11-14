using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Shared.Application.Model;

namespace BackendPlatform.API.Nutrition.Application.CommandServices;

public interface INutritionPlanCommandService
{
    Task<Result<NutritionPlan>> Handle(CreateNutritionPlanCommand command, CancellationToken cancellationToken);
}
