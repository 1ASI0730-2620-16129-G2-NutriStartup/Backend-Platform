using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming an <see cref="Appointment" /> aggregate into an
///     <see cref="AppointmentResource" />.
/// </summary>
public static class AppointmentResourceFromEntityAssembler
{
    /// <remarks>
    ///     The status is exposed in upper case (PENDING, CANCELLED...) to match the frontend contract.
    ///     The patient id is exposed as <c>userId</c>, also to match the frontend.
    /// </remarks>
    public static AppointmentResource ToResourceFromEntity(Appointment entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new AppointmentResource(
            entity.Id,
            entity.PatientId,
            entity.NutritionistId,
            entity.AvailabilityId,
            entity.Date,
            entity.StartTime,
            entity.EndTime,
            entity.Status.ToString().ToUpperInvariant(),
            entity.Reason,
            entity.DataAccessAuthorized);
    }
}
