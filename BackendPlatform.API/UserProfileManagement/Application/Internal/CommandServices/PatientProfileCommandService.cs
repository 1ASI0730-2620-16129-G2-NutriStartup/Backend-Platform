using BackendPlatform.API.Shared.Application.Patterns;
using BackendPlatform.API.Shared.Domain.Repositories;
using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;

namespace BackendPlatform.API.UserProfileManagement.Application.Internal.CommandServices;

public class PatientProfileCommandService(
    IPatientProfileRepository patientProfileRepository,
    IUnitOfWork unitOfWork) : IPatientProfileCommandService
{
    public async Task<Result<PatientProfile, UserProfileManagementError>> Handle(
        CreatePatientProfileCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.UserId))
            return Failure<PatientProfile>(UserProfileManagementError.InvalidData);
        if (await patientProfileRepository.FindByUserIdAsync(command.UserId, cancellationToken) is not null)
            return Failure<PatientProfile>(UserProfileManagementError.PatientProfileAlreadyExists);

        try
        {
            var profile = new PatientProfile(command.UserId, command.FirstName, command.LastName,
                command.Age, command.Gender, command.Weight, command.Height);
            await patientProfileRepository.AddAsync(profile, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(profile);
        }
        catch (ArgumentException)
        {
            return Failure<PatientProfile>(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<PatientProfile, UserProfileManagementError>> Handle(
        UpdatePatientProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByIdWithDetailsAsync(command.PatientProfileId, cancellationToken);
        if (profile is null || !string.Equals(profile.UserId, command.UserId, StringComparison.Ordinal))
            return Failure<PatientProfile>(UserProfileManagementError.PatientProfileNotFound);

        try
        {
            profile.UpdatePersonalInformation(command.FirstName, command.LastName, command.Age,
                command.Gender, command.Weight, command.Height);
            patientProfileRepository.Update(profile);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(profile);
        }
        catch (ArgumentException)
        {
            return Failure<PatientProfile>(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<Goal, UserProfileManagementError>> Handle(
        CreateGoalCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByIdWithDetailsAsync(command.UserProfileId, cancellationToken);
        if (profile is null) return Failure<Goal>(UserProfileManagementError.PatientProfileNotFound);
        try
        {
            var goal = profile.AddGoal(command.Type, command.TargetWeight, command.TargetDate, command.Status);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(goal);
        }
        catch (ArgumentException)
        {
            return Failure<Goal>(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<Goal, UserProfileManagementError>> Handle(
        UpdateGoalCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByGoalIdAsync(command.GoalId, cancellationToken);
        if (profile is null || profile.Id != command.UserProfileId)
            return Failure<Goal>(UserProfileManagementError.GoalNotFound);
        try
        {
            var goal = profile.UpdateGoal(command.GoalId, command.Type, command.TargetWeight,
                command.TargetDate, command.Status);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(goal);
        }
        catch (ArgumentException)
        {
            return Failure<Goal>(UserProfileManagementError.InvalidData);
        }
        catch (KeyNotFoundException)
        {
            return Failure<Goal>(UserProfileManagementError.GoalNotFound);
        }
    }

    public async Task<Result<bool, UserProfileManagementError>> Handle(
        DeleteGoalCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByGoalIdAsync(command.GoalId, cancellationToken);
        if (profile is null || !profile.RemoveGoal(command.GoalId))
            return Failure<bool>(UserProfileManagementError.GoalNotFound);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Success(true);
    }

    public async Task<Result<Preference, UserProfileManagementError>> Handle(
        CreatePreferenceCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByUserIdAsync(command.UserId, cancellationToken);
        if (profile is null) return Failure<Preference>(UserProfileManagementError.PatientProfileNotFound);
        try
        {
            var preference = profile.AddPreference(command.DietaryPreferences, command.FavoriteFoods);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(preference);
        }
        catch (ArgumentException)
        {
            return Failure<Preference>(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<Preference, UserProfileManagementError>> Handle(
        UpdatePreferenceCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByPreferenceIdAsync(command.PreferenceId, cancellationToken);
        if (profile is null || !string.Equals(profile.UserId, command.UserId, StringComparison.Ordinal))
            return Failure<Preference>(UserProfileManagementError.PreferenceNotFound);
        try
        {
            var preference = profile.UpdatePreference(command.PreferenceId, command.DietaryPreferences, command.FavoriteFoods);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(preference);
        }
        catch (ArgumentException)
        {
            return Failure<Preference>(UserProfileManagementError.InvalidData);
        }
        catch (KeyNotFoundException)
        {
            return Failure<Preference>(UserProfileManagementError.PreferenceNotFound);
        }
    }

    public async Task<Result<bool, UserProfileManagementError>> Handle(
        DeletePreferenceCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByPreferenceIdAsync(command.PreferenceId, cancellationToken);
        if (profile is null || !profile.RemovePreference(command.PreferenceId))
            return Failure<bool>(UserProfileManagementError.PreferenceNotFound);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Success(true);
    }

    public async Task<Result<Restriction, UserProfileManagementError>> Handle(
        CreateRestrictionCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByUserIdAsync(command.UserId, cancellationToken);
        if (profile is null) return Failure<Restriction>(UserProfileManagementError.PatientProfileNotFound);
        // The repository loads all child collections so a newly created child is attached to the aggregate.
        try
        {
            var restriction = profile.AddRestriction(command.Type, command.Description);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(restriction);
        }
        catch (ArgumentException)
        {
            return Failure<Restriction>(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<Restriction, UserProfileManagementError>> Handle(
        UpdateRestrictionCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByRestrictionIdAsync(command.RestrictionId, cancellationToken);
        if (profile is null || !string.Equals(profile.UserId, command.UserId, StringComparison.Ordinal))
            return Failure<Restriction>(UserProfileManagementError.RestrictionNotFound);
        try
        {
            var restriction = profile.UpdateRestriction(command.RestrictionId, command.Type, command.Description);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(restriction);
        }
        catch (ArgumentException)
        {
            return Failure<Restriction>(UserProfileManagementError.InvalidData);
        }
        catch (KeyNotFoundException)
        {
            return Failure<Restriction>(UserProfileManagementError.RestrictionNotFound);
        }
    }

    public async Task<Result<bool, UserProfileManagementError>> Handle(
        DeleteRestrictionCommand command, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByRestrictionIdAsync(command.RestrictionId, cancellationToken);
        if (profile is null || !profile.RemoveRestriction(command.RestrictionId))
            return Failure<bool>(UserProfileManagementError.RestrictionNotFound);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Success(true);
    }

    private static Result<T, UserProfileManagementError> Success<T>(T value) =>
        new Result<T, UserProfileManagementError>.Success(value);

    private static Result<T, UserProfileManagementError> Failure<T>(UserProfileManagementError error) =>
        new Result<T, UserProfileManagementError>.Failure(error);
}
