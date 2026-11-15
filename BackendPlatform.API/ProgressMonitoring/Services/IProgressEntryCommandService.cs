using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;

namespace BackendPlatform.API.ProgressMonitoring.Domain.Services;

/// <summary>
///     Command service contract for <see cref="ProgressEntry" />.
/// </summary>
public interface IProgressEntryCommandService
{
    /// <summary>
    ///     Handles the creation of a progress entry.
    /// </summary>
    /// <param name="command">The <see cref="CreateProgressEntryCommand" /> command.</param>
    /// <returns>The created entry, or null if it could not be persisted.</returns>
    Task<ProgressEntry?> Handle(CreateProgressEntryCommand command);

    /// <summary>
    ///     Handles the update of a progress entry.
    /// </summary>
    /// <param name="command">The <see cref="UpdateProgressEntryCommand" /> command.</param>
    /// <returns>The updated entry.</returns>
    Task<ProgressEntry?> Handle(UpdateProgressEntryCommand command);

    /// <summary>
    ///     Handles the deletion of a progress entry.
    /// </summary>
    /// <param name="command">The <see cref="DeleteProgressEntryCommand" /> command.</param>
    Task Handle(DeleteProgressEntryCommand command);
}