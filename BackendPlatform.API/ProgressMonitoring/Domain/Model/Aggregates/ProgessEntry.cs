using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.ValueObjects;

namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;

/// <summary>
///     Progress entry aggregate root. Represents one body-progress check-in of a user.
/// </summary>
public class ProgressEntry
{
    private const int MaxNotesLength = 500;

    // Used by EF Core
    protected ProgressEntry()
    {
    }

    /// <summary>
    ///     Creates a progress entry from a create command.
    /// </summary>
    /// <param name="command">The <see cref="CreateProgressEntryCommand" /> command.</param>
    public ProgressEntry(CreateProgressEntryCommand command)
    {
        if (command.UserId <= 0)
            throw new ArgumentException("User id must be greater than zero.", nameof(command));

        ValidateRecordedAt(command.RecordedAt);
        ValidateNotes(command.Notes);

        UserId = command.UserId;
        RecordedAt = command.RecordedAt;
        Measurements = new BodyMeasurements(command.WeightKg, command.BodyFatPercentage, command.WaistCm);
        Notes = command.Notes?.Trim();
    }

    public int Id { get; }
    public int UserId { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public BodyMeasurements Measurements { get; private set; } = null!;
    public string? Notes { get; private set; }

    /// <summary>
    ///     Updates the entry with the data of an update command.
    /// </summary>
    /// <param name="command">The <see cref="UpdateProgressEntryCommand" /> command.</param>
    public void Update(UpdateProgressEntryCommand command)
    {
        ValidateRecordedAt(command.RecordedAt);
        ValidateNotes(command.Notes);

        RecordedAt = command.RecordedAt;
        Measurements = new BodyMeasurements(command.WeightKg, command.BodyFatPercentage, command.WaistCm);
        Notes = command.Notes?.Trim();
    }

    private static void ValidateRecordedAt(DateTime recordedAt)
    {
        if (recordedAt == default)
            throw new ArgumentException("Recorded date is required.", nameof(recordedAt));


        if (recordedAt > DateTime.UtcNow.AddDays(1))
            throw new ArgumentException("Recorded date cannot be in the future.", nameof(recordedAt));
    }

    private static void ValidateNotes(string? notes)
    {
        if (notes is { Length: > MaxNotesLength })
            throw new ArgumentException($"Notes cannot exceed {MaxNotesLength} characters.", nameof(notes));
    }
}