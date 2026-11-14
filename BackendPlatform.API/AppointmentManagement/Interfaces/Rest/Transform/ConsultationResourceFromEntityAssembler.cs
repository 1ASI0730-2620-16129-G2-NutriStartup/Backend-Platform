using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="Consultation" /> aggregate into a
///     <see cref="ConsultationResource" />.
/// </summary>
public static class ConsultationResourceFromEntityAssembler
{
    public static ConsultationResource ToResourceFromEntity(Consultation entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new ConsultationResource(entity.Id, entity.AppointmentId, entity.PatientId, entity.NutritionistId,
            entity.Notes, entity.Recommendations, entity.Date, entity.FollowUpDate);
    }
}
