using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;

namespace BackendPlatform.API.Nutrition.Application.QueryServices;

public interface IMealPlanQueryService
{
    Task<MealPlan?> Handle(GetMealPlanByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansByNutritionPlanIdQuery query, CancellationToken cancellationToken);
}
