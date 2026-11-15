using System.Net.Mime;
using BackendPlatform.API.Nutrition.Application.CommandServices;
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
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Food Recommendation endpoints")]
public class FoodRecommendationsController(
    IFoodRecommendationQueryService foodRecommendationQueryService,
    IFoodRecommendationCommandService foodRecommendationCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{foodRecommendationId:int}")]
    [EndpointSummary("Get a food recommendation by its id")]
    [EndpointDescription("Get a food recommendation by its id")]
    [ProducesResponseType(typeof(FoodRecommendationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFoodRecommendationById([FromRoute] int foodRecommendationId, CancellationToken cancellationToken)
    {
        var getFoodRecommendationByIdQuery = new GetFoodRecommendationByIdQuery(foodRecommendationId);
        var foodRecommendation = await foodRecommendationQueryService.Handle(getFoodRecommendationByIdQuery, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromGetFoodRecommendationByIdResult(
            this,
            foodRecommendation,
            _errorLocalizer,
            _problemDetailsFactory,
            foundFoodRecommendation => Ok(FoodRecommendationResourceFromEntityAssembler.ToResourceFromEntity(foundFoodRecommendation))
        );
    }
    
    [HttpPost]
    [EndpointSummary("Create a food recommendation")]
    [EndpointDescription("Create a food recommendation")]
    [ProducesResponseType(typeof(FoodRecommendationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFoodRecommendation([FromBody] CreateFoodRecommendationResource resource,
        CancellationToken cancellationToken)
    {
        var createFoodRecommendationCommand = CreateFoodRecommendationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await foodRecommendationCommandService.Handle(createFoodRecommendationCommand, cancellationToken);

        return NutritionActionResultAssembler.ToActionResultFromCreateFoodRecommendationResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdFoodRecommendation => CreatedAtAction(nameof(GetFoodRecommendationById), new { tutorialId = createdFoodRecommendation.Id },
                FoodRecommendationResourceFromEntityAssembler.ToResourceFromEntity(createdFoodRecommendation))
        );
    }

    [HttpGet]
    [EndpointSummary("Get all food recommendations")]
    [EndpointDescription("Get all food recommendations")]
    [ProducesResponseType(typeof(IEnumerable<FoodRecommendationResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFoodRecommendations(CancellationToken cancellationToken)
    {
        var getAllFoodRecommendationsQuery = new GetAllFoodRecommendationsQuery();
        var foodRecommendations = await foodRecommendationQueryService.Handle(getAllFoodRecommendationsQuery, cancellationToken);
        var foodRecommendationResources = foodRecommendations.Select(FoodRecommendationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(foodRecommendationResources);
    }
}
