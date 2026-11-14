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
[Tags("Available Consultation endpoints")]
public class ConsultationsController(
    IConsultationQueryService consultationQueryService,
    IConsultationCommandService consultationCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [EndpointSummary("Get the consultations (history)")]
    [EndpointDescription("Get the registered consultations, optionally filtered by patient (userId), nutritionist or appointment")]
    [ProducesResponseType(typeof(IEnumerable<ConsultationResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConsultations(
        [FromQuery] int? userId,
        [FromQuery] int? nutritionistId,
        [FromQuery] int? appointmentId,
        CancellationToken cancellationToken)
    {
        var query = new GetConsultationsQuery(userId, nutritionistId, appointmentId);
        var consultations = await consultationQueryService.Handle(query, cancellationToken);
        return Ok(consultations.Select(ConsultationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{consultationId:int}")]
    [EndpointSummary("Get a consultation by its id")]
    [EndpointDescription("Get a consultation by its id")]
    [ProducesResponseType(typeof(ConsultationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConsultationById([FromRoute] int consultationId,
        CancellationToken cancellationToken)
    {
        var consultation =
            await consultationQueryService.Handle(new GetConsultationByIdQuery(consultationId), cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromQuery(
            this,
            consultation,
            AppointmentManagementError.ConsultationNotFound,
            _errorLocalizer,
            _problemDetailsFactory,
            found => Ok(ConsultationResourceFromEntityAssembler.ToResourceFromEntity(found))
        );
    }

    [HttpPost]
    [EndpointSummary("Register a consultation")]
    [EndpointDescription("Register the consultation of an attended appointment in the history")]
    [ProducesResponseType(typeof(ConsultationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterConsultation([FromBody] RegisterConsultationResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterConsultationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await consultationCommandService.Handle(command, cancellationToken);

        return AppointmentManagementActionResultAssembler.ToActionResultFromResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetConsultationById), new { consultationId = created.Id },
                ConsultationResourceFromEntityAssembler.ToResourceFromEntity(created))
        );
    }
}
