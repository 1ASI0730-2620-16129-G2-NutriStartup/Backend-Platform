namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;

/// <summary>
///     Command to delete a progress check-in.
/// </summary>
/// <param name="Id">The entry to delete.</param>
public record DeleteProgressEntryCommand(int Id);