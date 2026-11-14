using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

namespace Acme.Center.Platform.AppointmentManagement.Application.QueryServices;

/// <summary>
///     Represents the consultation query service in the Appointment Management bounded context.
/// </summary>
public interface IConsultationQueryService
{
    /// <summary>
    ///     Handles the get consultation by id query.
    /// </summary>
    Task<Consultation?> Handle(GetConsultationByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the get consultations query (clinical history).
    /// </summary>
    Task<IEnumerable<Consultation>> Handle(GetConsultationsQuery query, CancellationToken cancellationToken);
}
