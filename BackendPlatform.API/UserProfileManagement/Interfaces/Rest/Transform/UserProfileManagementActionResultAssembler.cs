using BackendPlatform.API.Shared.Application.Patterns;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class UserProfileManagementActionResultAssembler
{
    public static IActionResult ToActionResult<T>(ControllerBase controller,
        Result<T, UserProfileManagementError> result, Func<T, IActionResult> onSuccess)
    {
        return result switch
        {
            Result<T, UserProfileManagementError>.Success success => onSuccess(success.Value),
            Result<T, UserProfileManagementError>.Failure failure => ToErrorResult(controller, failure.Error),
            _ => controller.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    private static IActionResult ToErrorResult(ControllerBase controller, UserProfileManagementError error)
    {
        var payload = new { error = error.ToString() };
        return error switch
        {
            UserProfileManagementError.PatientProfileAlreadyExists or
            UserProfileManagementError.NutritionistProfileAlreadyExists => controller.Conflict(payload),

            UserProfileManagementError.PatientProfileNotFound or
            UserProfileManagementError.NutritionistProfileNotFound or
            UserProfileManagementError.GoalNotFound or
            UserProfileManagementError.PreferenceNotFound or
            UserProfileManagementError.RestrictionNotFound => controller.NotFound(payload),

            _ => controller.BadRequest(payload)
        };
    }
}
