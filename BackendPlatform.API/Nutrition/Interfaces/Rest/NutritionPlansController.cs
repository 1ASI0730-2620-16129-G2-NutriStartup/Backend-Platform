using System.Net.Mime;
using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;
using BackendPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using BackendPlatform.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Nutrition Plan Endpoints.")]
public class NutritionPlansController(
    INutritionPlanCommandService nutritionPlanCommandService,
    INutritionPlanQueryService nutritionPlanQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet("{nutritionPlanId:int}")]
    [EndpointSummary("Get all nutrition plans")]
    [EndpointDescription("Get all nutrition plans")]
    [ProducesResponseType(typeof(NutritionPlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNutritionPlanById(int nutritionPlanId, CancellationToken cancellationToken)
    {
        var getNutritionPlanByIdQuery = new GetNutritionPlanByIdQuery(nutritionPlanId);
        var nutritionPlan = await nutritionPlanQueryService.Handle(getNutritionPlanByIdQuery, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromGetNutritionPlanByIdResult(
            this,
            nutritionPlan,
            _errorLocalizer,
            _problemDetailsFactory,
            foundNutritionPlan => Ok(NutritionPlanResourceFromEntityAssembler.ToResourceFromEntity(foundNutritionPlan))
        );
    }
    
    [HttpPost]
    [EndpointSummary("Create a new nutrition plan")]
    [EndpointDescription("Create a new nutrition plan")]
    [ProducesResponseType(typeof(NutritionPlanResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNutritionPlan([FromBody] CreateNutritionPlanResource resource,
        CancellationToken cancellationToken)
    {
        var createNutritionPlanCommand = CreateNutritionPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await nutritionPlanCommandService.Handle(createNutritionPlanCommand, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromCreateNutritionPlanResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdNutritionPlan => CreatedAtAction(nameof(GetNutritionPlanById), new { nutritionPlanId = createdNutritionPlan.Id },
                NutritionPlanResourceFromEntityAssembler.ToResourceFromEntity(createdNutritionPlan))
        );
    }
    
    [HttpGet]
    [EndpointSummary("Get all nutrition plans")]
    [EndpointDescription("Get all nutrition plans")]
    [ProducesResponseType(typeof(IEnumerable<NutritionPlanResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllNutritionPlans(CancellationToken cancellationToken)
    {
        var getAllNutritionPlansQuery = new GetAllNutritionPlansQuery();
        var nutritionPlans = await nutritionPlanQueryService.Handle(getAllNutritionPlansQuery, cancellationToken);
        var nutritionPlanResources = nutritionPlans.Select(NutritionPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(nutritionPlanResources);
    }
}
