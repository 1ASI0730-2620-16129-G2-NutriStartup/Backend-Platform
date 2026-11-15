namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

/// <summary>
///     Resource to register a progress entry.
/// </summary>
/// <param name="UserId">The user that owns the entry.</param>
/// <param name="RecordedAt">Date of the check-in.</param>
/// <param name="WeightKg">Weight in kilograms.</param>
/// <param name="BodyFatPercentage">Optional body fat percentage.</param>
/// <param name="WaistCm">Optional waist circumference in centimeters.</param>
/// <param name="Notes">Optional free-text notes.</param>
public record CreateProgressEntryResource(
    int UserId,
    DateTime RecordedAt,
    decimal WeightKg,
    decimal? BodyFatPercentage,
    decimal? WaistCm,
    string? Notes);