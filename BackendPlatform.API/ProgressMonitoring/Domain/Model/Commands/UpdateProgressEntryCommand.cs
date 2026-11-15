namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;

/// <summary>
///     Command to update an existing progress check-in.
/// </summary>
/// <param name="Id">The entry to update.</param>
/// <param name="RecordedAt">Date of the check-in.</param>
/// <param name="WeightKg">Weight in kilograms.</param>
/// <param name="BodyFatPercentage">Optional body fat percentage.</param>
/// <param name="WaistCm">Optional waist circumference in centimeters.</param>
/// <param name="Notes">Optional free-text notes.</param>
public record UpdateProgressEntryCommand(
    int Id,
    DateTime RecordedAt,
    decimal WeightKg,
    decimal? BodyFatPercentage,
    decimal? WaistCm,
    string? Notes);