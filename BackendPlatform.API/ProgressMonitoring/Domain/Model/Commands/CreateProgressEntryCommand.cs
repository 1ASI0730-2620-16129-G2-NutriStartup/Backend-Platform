namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;

/// <summary>
///     Command to register a new progress check-in for a user.
/// </summary>
/// <param name="UserId">The user that owns the entry.</param>
/// <param name="RecordedAt">Date of the check-in.</param>
/// <param name="WeightKg">Weight in kilograms.</param>
/// <param name="BodyFatPercentage">Optional body fat percentage.</param>
/// <param name="WaistCm">Optional waist circumference in centimeters.</param>
/// <param name="Notes">Optional free-text notes.</param>
public record CreateProgressEntryCommand(
    int UserId,
    DateTime RecordedAt,
    decimal WeightKg,
    decimal? BodyFatPercentage,
    decimal? WaistCm,
    string? Notes);