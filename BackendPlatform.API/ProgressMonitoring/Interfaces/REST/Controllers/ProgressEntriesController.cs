using System.Net.Mime;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;
using BackendPlatform.API.ProgressMonitoring.Domain.Services;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Controllers;

/// <summary>
///     REST controller of the Progress Monitoring bounded context.
/// </summary>
/// <param name="progressEntryCommandService">The <see cref="IProgressEntryCommandService" /> command service.</param>
/// <param name="progressEntryQueryService">The <see cref="IProgressEntryQueryService" /> query service.</param>
[ApiController]
[Route("api/v1/progress-entries")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Progress Entries")]
public class ProgressEntriesController(
    IProgressEntryCommandService progressEntryCommandService,
    IProgressEntryQueryService progressEntryQueryService) : ControllerBase
{
    /// <summary>
    ///     Registers a new progress entry (weight and optional measurements) for a user.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateProgressEntry([FromBody] CreateProgressEntryResource resource)
    {
        try
        {
            var command = CreateProgressEntryCommandFromResourceAssembler.ToCommandFromResource(resource);
            var progressEntry = await progressEntryCommandService.Handle(command);
            if (progressEntry is null) return BadRequest(new { message = "The progress entry could not be created." });

            var progressEntryResource = ProgressEntryResourceFromEntityAssembler.ToResourceFromEntity(progressEntry);
            return CreatedAtAction(nameof(GetProgressEntryById), new { id = progressEntry.Id },
                progressEntryResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    ///     Gets a progress entry by its id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProgressEntryById(int id)
    {
        var progressEntry = await progressEntryQueryService.Handle(new GetProgressEntryByIdQuery(id));
        if (progressEntry is null) return NotFound();

        return Ok(ProgressEntryResourceFromEntityAssembler.ToResourceFromEntity(progressEntry));
    }

    /// <summary>
    ///     Gets every progress entry of a user, oldest first.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllProgressEntriesByUserId([FromQuery] int userId)
    {
        var progressEntries = await progressEntryQueryService.Handle(new GetAllProgressEntriesByUserIdQuery(userId));
        var resources = progressEntries.Select(ProgressEntryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    ///     Gets the overall evolution of a user: start, current, lowest and highest weight, and total change.
    /// </summary>
    [HttpGet("summary")]
    public async Task<IActionResult> GetProgressSummaryByUserId([FromQuery] int userId)
    {
        var summary = await progressEntryQueryService.Handle(new GetProgressSummaryByUserIdQuery(userId));
        if (summary is null) return NotFound();

        return Ok(ProgressSummaryResourceFromValueObjectAssembler.ToResourceFromValueObject(summary));
    }

    /// <summary>
    ///     Updates the date, measurements and notes of a progress entry.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProgressEntry(int id, [FromBody] UpdateProgressEntryResource resource)
    {
        try
        {
            var command = UpdateProgressEntryCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var progressEntry = await progressEntryCommandService.Handle(command);
            if (progressEntry is null) return BadRequest(new { message = "The progress entry could not be updated." });

            return Ok(ProgressEntryResourceFromEntityAssembler.ToResourceFromEntity(progressEntry));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    ///     Deletes a progress entry.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProgressEntry(int id)
    {
        try
        {
            await progressEntryCommandService.Handle(new DeleteProgressEntryCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}