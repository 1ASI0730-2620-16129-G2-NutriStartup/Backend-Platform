using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming <see cref="AppointmentMetrics" /> into an
///     <see cref="AppointmentMetricsResource" />.
/// </summary>
public static class AppointmentMetricsResourceFromValueObjectAssembler
{
    public static AppointmentMetricsResource ToResourceFromValueObject(AppointmentMetrics metrics)
    {
        ArgumentNullException.ThrowIfNull(metrics);
        return new AppointmentMetricsResource(metrics.TotalBooked, metrics.Completed, metrics.Cancelled,
            metrics.Rescheduled, metrics.CompletionRate, metrics.CancellationRate, metrics.RescheduleRate);
    }
}
