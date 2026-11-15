using BackendPlatform.API.Shared.Application.Patterns;
using BackendPlatform.API.Shared.Domain.Repositories;
using BackendPlatform.API.UserProfileManagement.Application.CommandServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;

namespace BackendPlatform.API.UserProfileManagement.Application.Internal.CommandServices;

public class NutritionistProfileCommandService(
    INutritionistProfileRepository nutritionistProfileRepository,
    IUnitOfWork unitOfWork) : INutritionistProfileCommandService
{
    public async Task<Result<NutritionistProfile, UserProfileManagementError>> Handle(
        CreateNutritionistProfileCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.UserId))
            return Failure(UserProfileManagementError.InvalidData);
        if (await nutritionistProfileRepository.FindByUserIdAsync(command.UserId, cancellationToken) is not null)
            return Failure(UserProfileManagementError.NutritionistProfileAlreadyExists);
        try
        {
            var profile = new NutritionistProfile(command.UserId, command.Specialty, command.ExperienceYears);
            await nutritionistProfileRepository.AddAsync(profile, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(profile);
        }
        catch (ArgumentException)
        {
            return Failure(UserProfileManagementError.InvalidData);
        }
    }

    public async Task<Result<NutritionistProfile, UserProfileManagementError>> Handle(
        UpdateNutritionistProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await nutritionistProfileRepository.FindByIdAsync(command.NutritionistProfileId, cancellationToken);
        if (profile is null || !string.Equals(profile.UserId, command.UserId, StringComparison.Ordinal))
            return Failure(UserProfileManagementError.NutritionistProfileNotFound);
        try
        {
            profile.UpdateProfessionalInformation(command.Specialty, command.ExperienceYears);
            nutritionistProfileRepository.Update(profile);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Success(profile);
        }
        catch (ArgumentException)
        {
            return Failure(UserProfileManagementError.InvalidData);
        }
    }

    private static Result<NutritionistProfile, UserProfileManagementError> Success(NutritionistProfile value) =>
        new Result<NutritionistProfile, UserProfileManagementError>.Success(value);

    private static Result<NutritionistProfile, UserProfileManagementError> Failure(UserProfileManagementError error) =>
        new Result<NutritionistProfile, UserProfileManagementError>.Failure(error);
}
