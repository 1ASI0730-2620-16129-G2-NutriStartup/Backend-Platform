using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="BookAppointmentResource" /> into a
///     <see cref="BookAppointmentCommand" />.
/// </summary>
public static class BookAppointmentCommandFromResourceAssembler
{
    public static BookAppointmentCommand ToCommandFromResource(BookAppointmentResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new BookAppointmentCommand(resource.UserId, resource.NutritionistId, resource.Date,
            resource.StartTime, resource.EndTime, resource.Reason, resource.AuthorizeDataAccess);
    }
}
