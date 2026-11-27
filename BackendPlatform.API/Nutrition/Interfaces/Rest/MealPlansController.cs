using System.Net.Mime;
using BackendPlatform.API.Nutrition.Application.CommandServices;
using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;
using BackendPlatform.API.Shared.Resources.Errors;
using BackendPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Meal Plan endpoints")]
public class MealPlansController(
    IMealPlanQueryService mealPlanQueryService,
    IMealPlanCommandService mealPlanCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet("{mealPlanId:int}")]
    [EndpointSummary("Get a meal plan by its id")]
    [EndpointDescription("Get a meal plan by its id")]
    [ProducesResponseType(typeof(MealPlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMealPlanById([FromRoute] int mealPlanId, CancellationToken cancellationToken)
    {
        var getMealPlanByIdQuery = new GetMealPlanByIdQuery(mealPlanId);
        var mealPlan = await mealPlanQueryService.Handle(getMealPlanByIdQuery, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromGetMealPlanByIdResult(
            this,
            mealPlan,
            _errorLocalizer,
            _problemDetailsFactory,
            foundMealPlan => Ok(MealPlanResourceFromEntityAssembler.ToResourceFromEntity(foundMealPlan))
        );
    }
    
    [HttpPost]
    [EndpointSummary("Create a meal plan")]
    [EndpointDescription("Create a meal plan")]
    [ProducesResponseType(typeof(MealPlanResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMealPlan([FromBody] CreateMealPlanResource resource,
        CancellationToken cancellationToken)
    {
        var createMealPlanCommand = CreateMealPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await mealPlanCommandService.Handle(createMealPlanCommand, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromCreateMealPlanResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdMealPlan => CreatedAtAction(nameof(GetMealPlanById), new { mealPlanId = createdMealPlan.Id },
                MealPlanResourceFromEntityAssembler.ToResourceFromEntity(createdMealPlan))
        );
    }
    
    [HttpGet]
    [EndpointSummary("Get all meal plans")]
    [EndpointDescription("Get all meal plans")]
    [ProducesResponseType(typeof(IEnumerable<MealPlanResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMealPlans(CancellationToken cancellationToken)
    {
        var getAllMealPlansQuery = new GetAllMealPlansQuery();
        var mealPlans = await mealPlanQueryService.Handle(getAllMealPlansQuery, cancellationToken);
        var mealPlanResources = mealPlans.Select(MealPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(mealPlanResources);
    }
    
    [HttpPut("{mealPlanId:int}")]
    [EndpointSummary("Update a meal plan")]
    [EndpointDescription("Update a meal plan by its id.")]
    [ProducesResponseType(typeof(MealPlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMealPlan(
        [FromRoute] int mealPlanId,
        [FromBody] UpdateMealPlanResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateMealPlanCommandFromResourceAssembler.ToCommandFromResource(resource, mealPlanId);

        var result = await mealPlanCommandService.Handle(command, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromUpdateMealPlanResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            updated => Ok(
                MealPlanResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
    
    [HttpDelete("{mealPlanId:int}")]
    [EndpointSummary("Delete meal plan by its id")]
    [EndpointDescription("Delete meal plan by its id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMealPlan(
        [FromRoute] int mealPlanId,
        CancellationToken cancellationToken)
    {
        var result = await mealPlanCommandService.Handle(
            new DeleteMealPlanCommand(mealPlanId),
            cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromDeleteMealPlanResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory);
    }
}
