using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/preferences")]
[Produces("application/json")]
public class PreferencesController(IPatientProfileQueryService queryService, IPatientProfileCommandService commandService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUserId([FromQuery(Name = "user_id")] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest(new { error = "user_id is required" });
        var preferences = await queryService.Handle(new GetPreferencesByUserIdQuery(userId), cancellationToken);
        return Ok(preferences.Select(item => PreferenceResourceFromEntityAssembler.ToResourceFromEntity(item, userId)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var preference = await queryService.Handle(new GetPreferenceByIdQuery(id), cancellationToken);
        if (preference is null) return NotFound(new { error = "PreferenceNotFound" });
        var profile = await queryService.Handle(new GetPatientProfileByIdQuery(preference.PatientProfileId), cancellationToken);
        if (profile is null) return NotFound(new { error = "PatientProfileNotFound" });
        return Ok(PreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference, profile.UserId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePreferenceResource resource, CancellationToken cancellationToken)
    {
        var command = PreferenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            created => CreatedAtAction(nameof(GetById), new { id = created.Id },
                PreferenceResourceFromEntityAssembler.ToResourceFromEntity(created, resource.UserId)));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePreferenceResource resource,
        CancellationToken cancellationToken)
    {
        var command = PreferenceCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            updated => Ok(PreferenceResourceFromEntityAssembler.ToResourceFromEntity(updated, resource.UserId)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(new DeletePreferenceCommand(id), cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result, _ => NoContent());
    }
}
