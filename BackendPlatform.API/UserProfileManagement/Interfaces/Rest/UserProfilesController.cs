using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/user_profiles")]
[Produces("application/json")]
public class UserProfilesController(
    IPatientProfileQueryService queryService,
    IPatientProfileCommandService commandService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientProfileResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId([FromQuery(Name = "user_id")] string userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest(new { error = "user_id is required" });
        var profile = await queryService.Handle(new GetPatientProfileByUserIdQuery(userId), cancellationToken);
        var response = profile is null
            ? Array.Empty<PatientProfileResource>()
            : new[] { PatientProfileResourceFromEntityAssembler.ToResourceFromEntity(profile) };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PatientProfileResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var profile = await queryService.Handle(new GetPatientProfileByIdQuery(id), cancellationToken);
        return profile is null
            ? NotFound(new { error = "PatientProfileNotFound" })
            : Ok(PatientProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PatientProfileResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreatePatientProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = PatientProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            created => CreatedAtAction(nameof(GetById), new { id = created.Id },
                PatientProfileResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PatientProfileResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePatientProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = PatientProfileCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await commandService.Handle(command, cancellationToken);
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            updated => Ok(PatientProfileResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
