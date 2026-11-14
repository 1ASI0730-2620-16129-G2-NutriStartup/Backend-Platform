namespace Acme.Center.Platform.AppointmentManagement.Domain.Model;

/// <summary>
///     Error codes of the Appointment Management bounded context.
///     Each value has a matching entry in <c>ErrorMessages.resx</c> prefixed with <c>AppointmentManagementError.</c>.
/// </summary>
public enum AppointmentManagementError
{
    None,
    AvailabilityNotFound,
    AvailabilityOverlap,
    SlotNotAvailable,
    AppointmentNotFound,
    ConsultationNotFound,
    ConsultationAlreadyRegistered,
    InvalidTimeSlot,
    InvalidAppointmentData,
    InvalidConsultationData,
    InvalidFollowUpDate,
    DataAccessNotAuthorized,
    InvalidAppointmentStatus,
    AppointmentCannotBeDeleted,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
