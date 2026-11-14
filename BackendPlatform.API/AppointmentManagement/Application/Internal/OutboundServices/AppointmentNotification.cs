namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;

/// <summary>
///     A notification addressed to one party of an appointment.
/// </summary>
/// <param name="Recipient">The role of the recipient.</param>
/// <param name="RecipientId">The id of the recipient (patient id or nutritionist id).</param>
/// <param name="Type">The kind of notification.</param>
/// <param name="Details">Culture-invariant details, for example the affected time slot.</param>
public record AppointmentNotification(
    ENotificationRecipient Recipient,
    int RecipientId,
    ENotificationType Type,
    string Details);
