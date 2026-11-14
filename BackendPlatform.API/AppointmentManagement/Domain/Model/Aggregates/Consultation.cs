using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Aggregates.Internal;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;

/// <summary>
///     Represents the Consultation aggregate root within the Appointment Management Bounded Context.
/// </summary>
/// <remarks>
///     A consultation is the record, in the patient's history, of an attended appointment (consulta realizada).
///     It is a separate aggregate that references its appointment by identity.
/// </remarks>
public partial class Consultation : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Consultation" /> aggregate with default values.
    ///     This constructor is primarily for persistence mechanisms.
    /// </summary>
    public Consultation()
    {
        Notes = string.Empty;
        Recommendations = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Consultation" /> aggregate.
    /// </summary>
    /// <param name="appointmentId">The appointment that was attended.</param>
    /// <param name="patientId">The patient who was attended.</param>
    /// <param name="nutritionistId">The nutritionist who performed the consultation.</param>
    /// <param name="notes">The notes taken by the nutritionist.</param>
    /// <param name="recommendations">The recommendations given to the patient.</param>
    /// <param name="date">The day the consultation took place.</param>
    /// <param name="followUpDate">The optional day of the follow-up.</param>
    public Consultation(AppointmentId appointmentId, PatientId patientId, NutritionistId nutritionistId,
        string notes, string? recommendations, DateOnly date, DateOnly? followUpDate) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(notes);
        if (followUpDate.HasValue && followUpDate.Value <= date)
            throw new ArgumentException("The follow-up date must be later than the consultation date.",
                nameof(followUpDate));

        AppointmentId = appointmentId;
        PatientId = patientId;
        NutritionistId = nutritionistId;
        Notes = notes.Trim();
        Recommendations = recommendations?.Trim() ?? string.Empty;
        Date = date;
        FollowUpDate = followUpDate;

        AddDomainEvent(new ConsultationPerformedEvent(appointmentId, patientId, nutritionistId, date));
        if (followUpDate.HasValue)
            AddDomainEvent(new FollowUpScheduledEvent(appointmentId, patientId, nutritionistId, followUpDate.Value));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Consultation" /> aggregate using a
    ///     <see cref="RegisterConsultationCommand" />.
    /// </summary>
    /// <param name="command">The command containing the data of the consultation.</param>
    /// <param name="appointment">The appointment that was attended.</param>
    public Consultation(RegisterConsultationCommand command, Appointment appointment)
        : this(appointment.Id, appointment.PatientId, appointment.NutritionistId, command.Notes,
            command.Recommendations, command.Date, command.FollowUpDate)
    {
    }

    /// <summary>
    ///     Gets the unique identifier of the consultation.
    /// </summary>
    public ConsultationId Id { get; private set; }

    /// <summary>
    ///     Gets the appointment that was attended.
    /// </summary>
    public AppointmentId AppointmentId { get; private set; }

    /// <summary>
    ///     Gets the patient who was attended.
    /// </summary>
    public PatientId PatientId { get; private set; }

    /// <summary>
    ///     Gets the nutritionist who performed the consultation.
    /// </summary>
    public NutritionistId NutritionistId { get; private set; }

    /// <summary>
    ///     Gets the notes taken by the nutritionist.
    /// </summary>
    public string Notes { get; private set; }

    /// <summary>
    ///     Gets the recommendations given to the patient.
    /// </summary>
    public string Recommendations { get; private set; }

    /// <summary>
    ///     Gets the day the consultation took place.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    ///     Gets the optional day of the follow-up (seguimiento).
    /// </summary>
    public DateOnly? FollowUpDate { get; private set; }
}
