using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/nutritionist_profiles")]
[Produces("application/json")]
public class NutritionistProfilesController(
    INutritionistProfileQueryService queryService,
    INutritionistProfileCommandService commandService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NutritionistProfileResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId([FromQuery(Name = "user_id")] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest(new { error = "user_id is required" });
        var profile = await queryService.Handle(new GetNutritionistProfileByUserIdQuery(userId), cancellationToken);
        var response = profile is null
            ? Array.Empty<NutritionistProfileResource>()
            : new[] { NutritionistProfileResourceFromEntityAssembler.ToResourceFromEntity(profile) };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(NutritionistProfileResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var profile = await queryService.Handle(new GetNutritionistProfileByIdQuery(id), cancellationToken);
        return profile is null
            ? NotFound(new { error = "NutritionistProfileNotFound" })
            : Ok(NutritionistProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpPost]
    [ProducesResponseType(typeof(NutritionistProfileResource), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateNutritionistProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = NutritionistProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            created => CreatedAtAction(nameof(GetById), new { id = created.Id },
                NutritionistProfileResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(NutritionistProfileResource), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateNutritionistProfileResource resource, CancellationToken cancellationToken)
    {
        var command = NutritionistProfileCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            updated => Ok(NutritionistProfileResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
