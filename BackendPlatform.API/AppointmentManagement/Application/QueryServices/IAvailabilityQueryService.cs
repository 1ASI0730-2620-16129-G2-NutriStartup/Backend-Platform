using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

namespace Acme.Center.Platform.AppointmentManagement.Application.QueryServices;

/// <summary>
///     Represents the availability query service in the Appointment Management bounded context.
/// </summary>
public interface IAvailabilityQueryService
{
    /// <summary>
    ///     Handles the get availability by id query.
    /// </summary>
    Task<Availability?> Handle(GetAvailabilityByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the get availabilities query (consultar horarios).
    /// </summary>
    Task<IEnumerable<Availability>> Handle(GetAvailabilitiesQuery query, CancellationToken cancellationToken);
}
