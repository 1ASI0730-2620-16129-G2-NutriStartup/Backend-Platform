using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/restrictions")]
[Produces("application/json")]
public class RestrictionsController(IPatientProfileQueryService queryService, IPatientProfileCommandService commandService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery(Name = "user_id")] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest(new { error = "user_id is required" });
        var restrictions = await queryService.Handle(new GetRestrictionsByUserIdQuery(userId), cancellationToken);
        return Ok(restrictions.Select(item => RestrictionResourceFromEntityAssembler.ToResourceFromEntity(item, userId)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var restriction = await queryService.Handle(new GetRestrictionByIdQuery(id), cancellationToken);
        if (restriction is null) return NotFound(new { error = "RestrictionNotFound" });
        var profile = await queryService.Handle(new GetPatientProfileByIdQuery(restriction.PatientProfileId), cancellationToken);
        if (profile is null) return NotFound(new { error = "PatientProfileNotFound" });
        return Ok(RestrictionResourceFromEntityAssembler.ToResourceFromEntity(restriction, profile.UserId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestrictionResource resource, CancellationToken cancellationToken)
    {
        var command = RestrictionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            created => CreatedAtAction(nameof(GetById), new { id = created.Id },
                RestrictionResourceFromEntityAssembler.ToResourceFromEntity(created, resource.UserId)));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRestrictionResource resource,
        CancellationToken cancellationToken)
    {
        var command = RestrictionCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            updated => Ok(RestrictionResourceFromEntityAssembler.ToResourceFromEntity(updated, resource.UserId)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(new DeleteRestrictionCommand(id), cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result, _ => NoContent());
    }
}
