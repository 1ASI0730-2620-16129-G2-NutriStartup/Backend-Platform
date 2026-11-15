using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;
using BackendPlatform.API.ProgressMonitoring.Domain.Repositories;
using BackendPlatform.API.ProgressMonitoring.Domain.Services;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.ProgressMonitoring.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for <see cref="ProgressEntry" />.
/// </summary>
/// <param name="progressEntryRepository">The <see cref="IProgressEntryRepository" /> repository.</param>
/// <param name="unitOfWork">The <see cref="IUnitOfWork" /> unit of work.</param>
public class ProgressEntryCommandService(
    IProgressEntryRepository progressEntryRepository,
    IUnitOfWork unitOfWork) : IProgressEntryCommandService
{
    public async Task<ProgressEntry?> Handle(CreateProgressEntryCommand command)
    {
        var progressEntry = new ProgressEntry(command);
        try
        {
            await progressEntryRepository.AddAsync(progressEntry);
            await unitOfWork.CompleteAsync();
            return progressEntry;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the progress entry: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ProgressEntry?> Handle(UpdateProgressEntryCommand command)
    {
        var progressEntry = await progressEntryRepository.FindByIdAsync(command.Id)
                            ?? throw new KeyNotFoundException($"Progress entry with id {command.Id} was not found.");

        try
        {
            progressEntry.Update(command);
            progressEntryRepository.Update(progressEntry);
            await unitOfWork.CompleteAsync();
            return progressEntry;
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the progress entry: {e.Message}");
            return null;
        }
    }

    public async Task Handle(DeleteProgressEntryCommand command)
    {
        var progressEntry = await progressEntryRepository.FindByIdAsync(command.Id)
                            ?? throw new KeyNotFoundException($"Progress entry with id {command.Id} was not found.");

        progressEntryRepository.Remove(progressEntry);
        await unitOfWork.CompleteAsync();
    }
}