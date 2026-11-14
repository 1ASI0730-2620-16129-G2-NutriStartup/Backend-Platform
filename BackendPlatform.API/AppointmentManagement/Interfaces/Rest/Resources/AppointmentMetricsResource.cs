using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Appointment metrics resource for REST API
/// </summary>
/// <param name="TotalBooked">Number of booked appointments</param>
/// <param name="Completed">Number of completed appointments</param>
/// <param name="Cancelled">Number of cancelled appointments</param>
/// <param name="Rescheduled">Number of appointments rescheduled at least once</param>
/// <param name="CompletionRate">Percentage of completed appointments</param>
/// <param name="CancellationRate">Percentage of cancelled appointments</param>
/// <param name="RescheduleRate">Percentage of appointments rescheduled at least once</param>
public record AppointmentMetricsResource(
    int TotalBooked,
    int Completed,
    int Cancelled,
    int Rescheduled,
    double CompletionRate,
    double CancellationRate,
    double RescheduleRate);
