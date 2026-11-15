using System.Net.Mime;
using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;
using BackendPlatform.API.Shared.Resources.Errors;
using BackendPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

[ApiController]
[Route("api/v1/mealPlans/{mealPlanId:int}/foodRecommendations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("MealPlans")]
public class MealPlanFoodRecommendationsController(
    IFoodRecommendationQueryService foodRecommendationQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ErrorMessages> errorLocalizer)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet]
    [EndpointSummary("Get all food recommendations by meal plan id")]
    [EndpointDescription("Get all food recommendations by meal plan id")]
    [ProducesResponseType(typeof(IEnumerable<FoodRecommendationResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFoodRecommendationsByMealPlanId(int mealPlanId, CancellationToken cancellationToken)
    {
        var getFoodRecommendationsByMealIdQuery = new GetAllFoodRecommendationsByMealPlanIdQuery(mealPlanId);
        var foodRecommendations = await foodRecommendationQueryService.Handle(getFoodRecommendationsByMealIdQuery, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromGetAllFoodRecommendationsByMealPlanIdResult(
            this,
            foodRecommendations,
            _errorLocalizer,
            _problemDetailsFactory,
            foundFoodRecommendations => Ok(foundFoodRecommendations.Select(FoodRecommendationResourceFromEntityAssembler.ToResourceFromEntity))
        );
    }
}
