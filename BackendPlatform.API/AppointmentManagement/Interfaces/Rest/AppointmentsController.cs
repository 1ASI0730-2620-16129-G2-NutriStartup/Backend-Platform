using System.Net.Mime;
using Acme.Center.Platform.AppointmentManagement.Application.CommandServices;
using Acme.Center.Platform.AppointmentManagement.Application.QueryServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
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
[Tags("Available Appointment endpoints")]
public class AppointmentsController(
    IAppointmentQueryService appointmentQueryService,
    IAppointmentCommandService appointmentCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [EndpointSummary("Get the appointments")]
    [EndpointDescription("Get the appointments, optionally filtered by patient (userId), nutritionist or status")]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointments(
        [FromQuery] int? userId,
        [FromQuery] int? nutritionistId,
        [FromQuery] EAppointmentStatus? status,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsQuery(userId, nutritionistId, status);
        var appointments = await appointmentQueryService.Handle(query, cancellationToken);
        return Ok(appointments.Select(AppointmentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{appointmentId:int}")]
    [EndpointSummary("Get an appointment by its id")]
    [EndpointDescription("Get an appointment by its id")]
    [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAppointmentById([FromRoute] int appointmentId,
        CancellationToken cancellationToken)
    {
        var appointment =
            await appointmentQueryService.Handle(new GetAppointmentByIdQuery(appointmentId), cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromQuery(
            this,
            appointment,
            AppointmentManagementError.AppointmentNotFound,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(found))
        );
    }

    [HttpGet("metrics")]
    [EndpointSummary("Get the appointment metrics")]
    [EndpointDescription("Booked, completed, cancelled and rescheduled appointments with their rates")]
    [ProducesResponseType(typeof(AppointmentMetricsResource), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointmentMetrics([FromQuery] int? nutritionistId,
        CancellationToken cancellationToken)
    {
        var metrics = await appointmentQueryService.Handle(new GetAppointmentMetricsQuery(nutritionistId),
            cancellationToken);
        return Ok(AppointmentMetricsResourceFromValueObjectAssembler.ToResourceFromValueObject(metrics));
    }

    [HttpPost]
    [EndpointSummary("Book an appointment")]
    [EndpointDescription("A patient books an appointment inside a published availability")]
    [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentResource resource,
        CancellationToken cancellationToken)
    {
        var command = BookAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await appointmentCommandService.Handle(command, cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = created.Id },
                AppointmentResourceFromEntityAssembler.ToResourceFromEntity(created))
        );
    }

    [HttpPost("{appointmentId:int}/confirm")]
    [EndpointSummary("Confirm an appointment")]
    [EndpointDescription("Confirm a pending or rescheduled appointment")]
    [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ConfirmAppointment([FromRoute] int appointmentId,
        CancellationToken cancellationToken)
    {
        var result = await appointmentCommandService.Handle(new ConfirmAppointmentCommand(appointmentId),
            cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            confirmed => Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(confirmed))
        );
    }

    [HttpPost("{appointmentId:int}/reschedule")]
    [EndpointSummary("Reschedule an appointment")]
    [EndpointDescription("Move an appointment to another availability of the same nutritionist")]
    [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RescheduleAppointment([FromRoute] int appointmentId,
        [FromBody] RescheduleAppointmentResource resource, CancellationToken cancellationToken)
    {
        var command =
            RescheduleAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource, appointmentId);
        var result = await appointmentCommandService.Handle(command, cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            rescheduled => Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(rescheduled))
        );
    }

    [HttpPost("{appointmentId:int}/cancel")]
    [EndpointSummary("Cancel an appointment")]
    [EndpointDescription("Cancel an appointment and release its slot. The request body is optional")]
    [ProducesResponseType(typeof(AppointmentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CancelAppointment([FromRoute] int appointmentId,
        [FromBody] CancelAppointmentResource? resource, CancellationToken cancellationToken)
    {
        var command = CancelAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource, appointmentId);
        var result = await appointmentCommandService.Handle(command, cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            cancelled => Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(cancelled))
        );
    }

    [HttpDelete("{appointmentId:int}")]
    [EndpointSummary("Delete a cancelled appointment")]
    [EndpointDescription("Remove a cancelled appointment from the agenda. Active appointments must be cancelled first")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteAppointment([FromRoute] int appointmentId,
        CancellationToken cancellationToken)
    {
        var result = await appointmentCommandService.Handle(new DeleteAppointmentCommand(appointmentId),
            cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            NoContent
        );
    }
}
