namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;

/// <summary>
///     Outbound port of the notification service role. Infrastructure provides the adapter
///     (log, e-mail, push...) so the domain and application layers do not depend on any channel.
/// </summary>
public interface IAppointmentNotificationService
{
    /// <summary>
    ///     Sends a notification to one party of an appointment.
    /// </summary>
    /// <param name="notification">The notification to send.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task NotifyAsync(AppointmentNotification notification, CancellationToken cancellationToken);
}
