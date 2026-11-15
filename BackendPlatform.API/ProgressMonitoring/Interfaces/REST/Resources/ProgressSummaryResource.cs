namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents the overall progress summary of a user.
/// </summary>
/// <param name="UserId">The user the summary belongs to.</param>
/// <param name="TotalEntries">How many check-ins the user has registered.</param>
/// <param name="StartWeightKg">Weight of the oldest entry.</param>
/// <param name="CurrentWeightKg">Weight of the most recent entry.</param>
/// <param name="WeightChangeKg">Current minus start (negative means the user lost weight).</param>
/// <param name="LowestWeightKg">Lowest weight ever registered.</param>
/// <param name="HighestWeightKg">Highest weight ever registered.</param>
/// <param name="FirstRecordedAt">Date of the oldest entry.</param>
/// <param name="LastRecordedAt">Date of the most recent entry.</param>
public record ProgressSummaryResource(
    int UserId,
    int TotalEntries,
    decimal StartWeightKg,
    decimal CurrentWeightKg,
    decimal WeightChangeKg,
    decimal LowestWeightKg,
    decimal HighestWeightKg,
    DateTime FirstRecordedAt,
    DateTime LastRecordedAt);