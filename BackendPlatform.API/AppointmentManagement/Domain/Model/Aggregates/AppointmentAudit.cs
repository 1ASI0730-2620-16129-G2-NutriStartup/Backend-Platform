using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;

public partial class Appointment : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
