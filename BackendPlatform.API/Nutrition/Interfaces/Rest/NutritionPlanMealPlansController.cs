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
[Route("api/v1/nutritionPlans/{nutritionPlanId:int}/mealPlans")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("NutritionPlans")]
public class NutritionPlanMealPlansController(
    IMealPlanQueryService mealPlanQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ErrorMessages> errorLocalizer)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet]
    [EndpointSummary("Get all meal plans by nutrition plan id")]
    [EndpointDescription("Get all meal plans by nutrition plan id")]
    [ProducesResponseType(typeof(IEnumerable<MealPlanResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMealPlansByNutritionPlanId(int nutritionPlanId, CancellationToken cancellationToken)
    {
        var getMealPlansByNutritionPlanIdQuery = new GetAllMealPlansByNutritionPlanIdQuery(nutritionPlanId);
        var mealPlans = await mealPlanQueryService.Handle(getMealPlansByNutritionPlanIdQuery, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromGetAllMealPlansByNutritionPlanIdResult(
            this,
            mealPlans,
            _errorLocalizer,
            _problemDetailsFactory,
            foundMealPlans => Ok(foundMealPlans.Select(MealPlanResourceFromEntityAssembler.ToResourceFromEntity))
        );
    }
}
