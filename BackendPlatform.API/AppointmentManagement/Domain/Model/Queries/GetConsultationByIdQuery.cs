using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to get a consultation by its id.
/// </summary>
/// <param name="ConsultationId">The id of the consultation.</param>
public record GetConsultationByIdQuery(ConsultationId ConsultationId)
{
    public GetConsultationByIdQuery(int consultationId) : this(new ConsultationId(consultationId))
    {
    }
}
