using Acme.Center.Platform.Shared.Domain.Model.Errors;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Errors;

public static class AppointmentManagementErrors
{
    public static readonly Error AvailabilityNotFound =
        Error.NotFound("AppointmentManagement.AvailabilityNotFound",
            "The specified availability was not found.");

    public static readonly Error AvailabilityOverlap =
        Error.Conflict("AppointmentManagement.AvailabilityOverlap",
            "The nutritionist already has an availability that overlaps with the requested time slot.");

    public static readonly Error SlotNotAvailable =
        Error.Conflict("AppointmentManagement.SlotNotAvailable",
            "The requested time slot is no longer available.");

    public static readonly Error AppointmentNotFound =
        Error.NotFound("AppointmentManagement.AppointmentNotFound",
            "The specified appointment was not found.");

    public static readonly Error ConsultationNotFound =
        Error.NotFound("AppointmentManagement.ConsultationNotFound",
            "The specified consultation was not found.");

    public static readonly Error ConsultationAlreadyRegistered =
        Error.Conflict("AppointmentManagement.ConsultationAlreadyRegistered",
            "A consultation has already been registered for this appointment.");

    public static readonly Error InvalidTimeSlot =
        Error.Validation("AppointmentManagement.InvalidTimeSlot",
            "The time slot is invalid: the end time must be later than the start time.");

    public static readonly Error InvalidAppointmentData =
        Error.Validation("AppointmentManagement.InvalidAppointmentData",
            "The appointment data provided is invalid: the reason is required.");

    public static readonly Error InvalidConsultationData =
        Error.Validation("AppointmentManagement.InvalidConsultationData",
            "The consultation data provided is invalid: the notes are required.");

    public static readonly Error InvalidFollowUpDate =
        Error.Validation("AppointmentManagement.InvalidFollowUpDate",
            "The follow-up date must be later than the consultation date.");

    public static readonly Error DataAccessNotAuthorized =
        Error.Validation("AppointmentManagement.DataAccessNotAuthorized",
            "The patient must authorize the nutritionist's access to their information to book an appointment.");

    public static readonly Error InvalidAppointmentStatus =
        Error.Conflict("AppointmentManagement.InvalidAppointmentStatus",
            "The operation is not allowed for the current status of the appointment.");

    public static readonly Error AppointmentCannotBeDeleted =
        Error.Conflict("AppointmentManagement.AppointmentCannotBeDeleted",
            "Only cancelled appointments can be deleted. Cancel the appointment first.");
}
