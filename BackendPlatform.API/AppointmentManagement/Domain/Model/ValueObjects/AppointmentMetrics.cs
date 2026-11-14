using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Value object that summarizes the verification metrics of the Appointment Management bounded context.
/// </summary>
/// <param name="TotalBooked">Number of booked appointments (all statuses).</param>
/// <param name="Completed">Number of completed appointments.</param>
/// <param name="Cancelled">Number of cancelled appointments.</param>
/// <param name="Rescheduled">Number of appointments that were rescheduled at least once.</param>
/// <param name="CompletionRate">Percentage of completed appointments (0-100).</param>
/// <param name="CancellationRate">Percentage of cancelled appointments (0-100).</param>
/// <param name="RescheduleRate">Percentage of appointments rescheduled at least once (0-100).</param>
public record AppointmentMetrics(
    int TotalBooked,
    int Completed,
    int Cancelled,
    int Rescheduled,
    double CompletionRate,
    double CancellationRate,
    double RescheduleRate)
{
    /// <summary>
    ///     Calculates the metrics for a collection of appointments.
    /// </summary>
    /// <param name="appointments">The appointments to summarize.</param>
    /// <returns>The calculated <see cref="AppointmentMetrics" />.</returns>
    public static AppointmentMetrics From(IEnumerable<Appointment> appointments)
    {
        var list = appointments.ToList();
        var total = list.Count;
        var completed = list.Count(a => a.Status == EAppointmentStatus.Completed);
        var cancelled = list.Count(a => a.Status == EAppointmentStatus.Cancelled);
        var rescheduled = list.Count(a => a.RescheduleCount > 0);

        return new AppointmentMetrics(
            total,
            completed,
            cancelled,
            rescheduled,
            Percentage(completed, total),
            Percentage(cancelled, total),
            Percentage(rescheduled, total));
    }

    private static double Percentage(int part, int total)
    {
        return total == 0 ? 0 : Math.Round(part * 100.0 / total, 2);
    }
}
