namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;

/// <summary>
///     Query to get a progress entry by its id.
/// </summary>
/// <param name="Id">The entry id.</param>
public record GetProgressEntryByIdQuery(int Id);