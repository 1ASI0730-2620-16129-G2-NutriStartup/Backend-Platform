namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;

/// <summary>
///     Query to get every progress entry of a user, oldest first.
/// </summary>
/// <param name="UserId">The user id.</param>
public record GetAllProgressEntriesByUserIdQuery(int UserId);