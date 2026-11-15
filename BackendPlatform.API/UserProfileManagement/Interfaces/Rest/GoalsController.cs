using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/goals")]
[Produces("application/json")]
public class GoalsController(IPatientProfileQueryService queryService, IPatientProfileCommandService commandService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByUserProfileId([FromQuery(Name = "user_profile_id")] int userProfileId,
        CancellationToken cancellationToken)
    {
        if (userProfileId <= 0) return BadRequest(new { error = "user_profile_id must be positive" });
        
        var goals = await queryService.Handle(new GetGoalsByUserProfileIdQuery(userProfileId), cancellationToken);
        
        return Ok(goals.Select(GoalResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var goal = await queryService.Handle(new GetGoalByIdQuery(id), cancellationToken);
        
        return goal is null ? NotFound(new { error = "GoalNotFound" }) : Ok(GoalResourceFromEntityAssembler.ToResourceFromEntity(goal));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalResource resource, CancellationToken cancellationToken)
    {
        var command = GoalCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var result = await commandService.Handle(command, cancellationToken);
        
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            created => CreatedAtAction(nameof(GetById), new { id = created.Id }, GoalResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGoalResource resource,
        CancellationToken cancellationToken)
    {
        var command = GoalCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        
        var result = await commandService.Handle(command, cancellationToken);
        
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result,
            updated => Ok(GoalResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(new DeleteGoalCommand(id), cancellationToken);
        
        return UserProfileManagementActionResultAssembler.ToActionResult(this, result, _ => NoContent());
    }
}
