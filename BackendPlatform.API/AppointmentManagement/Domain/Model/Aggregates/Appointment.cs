using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Aggregates.Internal;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;

/// <summary>
///     Represents the Appointment aggregate root within the Appointment Management Bounded Context.
/// </summary>
/// <remarks>
///     An appointment (cita) links a patient with the nutritionist assigned to a booked time slot. It owns the
///     appointment lifecycle (see <see cref="EAppointmentStatus" />) and records the patient's authorization for
///     the nutritionist to access their information.
/// </remarks>
public partial class Appointment : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Appointment" /> aggregate with default values.
    ///     This constructor is primarily for persistence mechanisms.
    /// </summary>
    public Appointment()
    {
        Reason = string.Empty;
        Status = EAppointmentStatus.Pending;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Appointment" /> aggregate.
    /// </summary>
    /// <param name="patientId">The patient who books the appointment.</param>
    /// <param name="nutritionistId">The nutritionist assigned to the appointment.</param>
    /// <param name="availabilityId">The availability that backs the appointment.</param>
    /// <param name="slot">The booked time slot.</param>
    /// <param name="reason">The reason of the consultation.</param>
    /// <param name="dataAccessAuthorized">Whether the patient authorizes the nutritionist to access their data.</param>
    public Appointment(PatientId patientId, NutritionistId nutritionistId, AvailabilityId availabilityId,
        TimeSlot slot, string reason, bool dataAccessAuthorized) : this()
    {
        ArgumentNullException.ThrowIfNull(slot);
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        if (!slot.IsValid)
            throw new ArgumentException("The end time must be later than the start time.", nameof(slot));
        if (!dataAccessAuthorized)
            throw new InvalidOperationException(
                "The patient must authorize the nutritionist's access to their information.");

        PatientId = patientId;
        NutritionistId = nutritionistId;
        AvailabilityId = availabilityId;
        Date = slot.Date;
        StartTime = slot.StartTime;
        EndTime = slot.EndTime;
        Reason = reason.Trim();
        DataAccessAuthorized = true;
        DataAccessAuthorizedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new AppointmentBookedEvent(patientId, nutritionistId, availabilityId, slot));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Appointment" /> aggregate using a
    ///     <see cref="BookAppointmentCommand" />.
    /// </summary>
    /// <param name="command">The command containing the data for booking the appointment.</param>
    /// <param name="availabilityId">The availability that is being reserved.</param>
    public Appointment(BookAppointmentCommand command, AvailabilityId availabilityId)
        : this(command.PatientId, command.NutritionistId, availabilityId,
            new TimeSlot(command.Date, command.StartTime, command.EndTime), command.Reason,
            command.AuthorizeDataAccess)
    {
    }

    /// <summary>
    ///     Gets the unique identifier of the appointment.
    /// </summary>
    public AppointmentId Id { get; private set; }

    /// <summary>
    ///     Gets the patient of the appointment.
    /// </summary>
    public PatientId PatientId { get; private set; }

    /// <summary>
    ///     Gets the nutritionist assigned to the appointment (nutricionista asignado).
    /// </summary>
    public NutritionistId NutritionistId { get; private set; }

    /// <summary>
    ///     Gets the availability that is currently reserved by the appointment.
    /// </summary>
    public AvailabilityId AvailabilityId { get; private set; }

    /// <summary>
    ///     Gets the day of the appointment.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    ///     Gets the time at which the appointment starts.
    /// </summary>
    public TimeOnly StartTime { get; private set; }

    /// <summary>
    ///     Gets the time at which the appointment ends.
    /// </summary>
    public TimeOnly EndTime { get; private set; }

    /// <summary>
    ///     Gets the reason of the consultation.
    /// </summary>
    public string Reason { get; private set; }

    /// <summary>
    ///     Gets the current status of the appointment.
    /// </summary>
    public EAppointmentStatus Status { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether the patient authorized the nutritionist to access their information.
    /// </summary>
    public bool DataAccessAuthorized { get; private set; }

    /// <summary>
    ///     Gets the moment in which the patient granted the authorization.
    /// </summary>
    public DateTimeOffset? DataAccessAuthorizedAt { get; private set; }

    /// <summary>
    ///     Gets how many times the appointment has been rescheduled.
    /// </summary>
    public int RescheduleCount { get; private set; }

    /// <summary>
    ///     Gets the optional reason given when the appointment was cancelled.
    /// </summary>
    public string? CancellationReason { get; private set; }

    /// <summary>
    ///     Gets the time slot of the appointment as a value object.
    /// </summary>
    public TimeSlot Slot => new(Date, StartTime, EndTime);

    /// <summary>
    ///     Gets a value indicating whether the appointment can be confirmed.
    /// </summary>
    public bool CanBeConfirmed => Status is EAppointmentStatus.Pending or EAppointmentStatus.Rescheduled;

    /// <summary>
    ///     Gets a value indicating whether the appointment can be moved to another time slot.
    /// </summary>
    public bool CanBeRescheduled => IsActive;

    /// <summary>
    ///     Gets a value indicating whether the appointment can be cancelled.
    /// </summary>
    public bool CanBeCancelled => IsActive;

    /// <summary>
    ///     Gets a value indicating whether the appointment can be marked as completed.
    /// </summary>
    public bool CanBeCompleted => IsActive;

    /// <summary>
    ///     Gets a value indicating whether the appointment can be removed from the agenda.
    /// </summary>
    public bool CanBeDeleted => Status == EAppointmentStatus.Cancelled;

    private bool IsActive => Status is EAppointmentStatus.Pending
        or EAppointmentStatus.Confirmed
        or EAppointmentStatus.Rescheduled;

    /// <summary>
    ///     Confirms the appointment.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the current status does not allow confirming.</exception>
    public void Confirm()
    {
        if (!CanBeConfirmed)
            throw new InvalidOperationException($"An appointment in status {Status} cannot be confirmed.");

        Status = EAppointmentStatus.Confirmed;
        AddDomainEvent(new AppointmentConfirmedEvent(Id, PatientId, NutritionistId, Slot));
    }

    /// <summary>
    ///     Moves the appointment to another (already reserved) availability of the same nutritionist.
    ///     The appointment must be confirmed again afterwards.
    /// </summary>
    /// <param name="newAvailabilityId">The availability that backs the new time slot.</param>
    /// <param name="newSlot">The new time slot.</param>
    /// <exception cref="InvalidOperationException">Thrown if the current status does not allow rescheduling.</exception>
    public void Reschedule(AvailabilityId newAvailabilityId, TimeSlot newSlot)
    {
        ArgumentNullException.ThrowIfNull(newSlot);
        if (!CanBeRescheduled)
            throw new InvalidOperationException($"An appointment in status {Status} cannot be rescheduled.");
        if (!newSlot.IsValid)
            throw new ArgumentException("The end time must be later than the start time.", nameof(newSlot));

        var previousSlot = Slot;
        AvailabilityId = newAvailabilityId;
        Date = newSlot.Date;
        StartTime = newSlot.StartTime;
        EndTime = newSlot.EndTime;
        RescheduleCount++;
        Status = EAppointmentStatus.Rescheduled;
        AddDomainEvent(new AppointmentRescheduledEvent(Id, PatientId, NutritionistId, previousSlot, newSlot));
    }

    /// <summary>
    ///     Cancels the appointment.
    /// </summary>
    /// <param name="reason">The optional reason of the cancellation.</param>
    /// <exception cref="InvalidOperationException">Thrown if the current status does not allow cancelling.</exception>
    public void Cancel(string? reason)
    {
        if (!CanBeCancelled)
            throw new InvalidOperationException($"An appointment in status {Status} cannot be cancelled.");

        CancellationReason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();
        Status = EAppointmentStatus.Cancelled;
        AddDomainEvent(new AppointmentCancelledEvent(Id, PatientId, NutritionistId, Slot, CancellationReason));
    }

    /// <summary>
    ///     Marks the appointment as completed once its consultation has been registered.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the current status does not allow completing.</exception>
    public void Complete()
    {
        if (!CanBeCompleted)
            throw new InvalidOperationException($"An appointment in status {Status} cannot be completed.");

        Status = EAppointmentStatus.Completed;
    }
}
