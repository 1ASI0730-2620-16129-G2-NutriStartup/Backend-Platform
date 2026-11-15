namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the overall progress summary of a user.
/// </summary>
/// <param name="UserId">The user id.</param>
public record GetProgressSummaryByUserIdQuery(int UserId);