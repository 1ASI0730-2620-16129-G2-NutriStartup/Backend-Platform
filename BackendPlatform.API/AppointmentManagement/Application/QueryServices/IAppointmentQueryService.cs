using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Application.QueryServices;

/// <summary>
///     Represents the appointment query service in the Appointment Management bounded context.
/// </summary>
public interface IAppointmentQueryService
{
    /// <summary>
    ///     Handles the get appointment by id query.
    /// </summary>
    Task<Appointment?> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the get appointments query.
    /// </summary>
    Task<IEnumerable<Appointment>> Handle(GetAppointmentsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the get appointment metrics query.
    /// </summary>
    Task<AppointmentMetrics> Handle(GetAppointmentMetricsQuery query, CancellationToken cancellationToken);
}
