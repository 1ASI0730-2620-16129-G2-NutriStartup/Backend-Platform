using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;

namespace BackendPlatform.API.Nutrition.Application.QueryServices;

public interface INutritionPlanQueryService
{
    Task<NutritionPlan?> Handle(GetNutritionPlanByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<NutritionPlan>> Handle(GetAllNutritionPlansQuery query, CancellationToken cancellationToken);
}
