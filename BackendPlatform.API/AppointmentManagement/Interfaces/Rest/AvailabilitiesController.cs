using System.Net.Mime;
using Acme.Center.Platform.AppointmentManagement.Application.CommandServices;
using Acme.Center.Platform.AppointmentManagement.Application.QueryServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;
using Acme.Center.Platform.Resources.Errors;
using Acme.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Availability endpoints")]
public class AvailabilitiesController(
    IAvailabilityQueryService availabilityQueryService,
    IAvailabilityCommandService availabilityCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [EndpointSummary("Get the availabilities (schedules)")]
    [EndpointDescription("Get the published availabilities, optionally filtered by nutritionist, date or free slots only")]
    [ProducesResponseType(typeof(IEnumerable<AvailabilityResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailabilities(
        [FromQuery] int? nutritionistId,
        [FromQuery] DateOnly? date,
        [FromQuery] bool onlyAvailable,
        CancellationToken cancellationToken)
    {
        var query = new GetAvailabilitiesQuery(nutritionistId, date, onlyAvailable);
        var availabilities = await availabilityQueryService.Handle(query, cancellationToken);
        return Ok(availabilities.Select(AvailabilityResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{availabilityId:int}")]
    [EndpointSummary("Get an availability by its id")]
    [EndpointDescription("Get an availability by its id")]
    [ProducesResponseType(typeof(AvailabilityResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailabilityById([FromRoute] int availabilityId,
        CancellationToken cancellationToken)
    {
        var availability =
            await availabilityQueryService.Handle(new GetAvailabilityByIdQuery(availabilityId), cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromQuery(
            this,
            availability,
            AppointmentManagementError.AvailabilityNotFound,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(AvailabilityResourceFromEntityAssembler.ToResourceFromEntity(found))
        );
    }

    [HttpPost]
    [EndpointSummary("Publish an availability")]
    [EndpointDescription("A nutritionist publishes a time slot that patients can book")]
    [ProducesResponseType(typeof(AvailabilityResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PublishAvailability([FromBody] PublishAvailabilityResource resource,
        CancellationToken cancellationToken)
    {
        var command = PublishAvailabilityCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await availabilityCommandService.Handle(command, cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAvailabilityById), new { availabilityId = created.Id },
                AvailabilityResourceFromEntityAssembler.ToResourceFromEntity(created))
        );
    }
}
