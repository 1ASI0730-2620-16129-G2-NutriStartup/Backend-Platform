using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.Resources.Errors;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

public static class AppointmentManagementActionResultAssembler
{
    // --- Helper for transforming AppointmentManagementError to StatusCode ---
    private static int ToStatusCodeFromAppointmentManagementError(AppointmentManagementError error)
    {
        return error switch
        {
            AppointmentManagementError.AvailabilityNotFound => StatusCodes.Status404NotFound,
            AppointmentManagementError.AppointmentNotFound => StatusCodes.Status404NotFound,
            AppointmentManagementError.ConsultationNotFound => StatusCodes.Status404NotFound,
            AppointmentManagementError.AvailabilityOverlap => StatusCodes.Status409Conflict,
            AppointmentManagementError.SlotNotAvailable => StatusCodes.Status409Conflict,
            AppointmentManagementError.ConsultationAlreadyRegistered => StatusCodes.Status409Conflict,
            AppointmentManagementError.InvalidAppointmentStatus => StatusCodes.Status409Conflict,
            AppointmentManagementError.AppointmentCannotBeDeleted => StatusCodes.Status409Conflict,
            AppointmentManagementError.OperationCancelled => StatusCodes.Status409Conflict,
            AppointmentManagementError.InvalidTimeSlot => StatusCodes.Status400BadRequest,
            AppointmentManagementError.InvalidAppointmentData => StatusCodes.Status400BadRequest,
            AppointmentManagementError.InvalidConsultationData => StatusCodes.Status400BadRequest,
            AppointmentManagementError.InvalidFollowUpDate => StatusCodes.Status400BadRequest,
            AppointmentManagementError.DataAccessNotAuthorized => StatusCodes.Status400BadRequest,
            AppointmentManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
            AppointmentManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }

    // --- Specific Assembler Methods ---

    /// <summary>
    ///     Transforms the result of a command into an action result.
    /// </summary>
    public static IActionResult ToActionResultFromResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromAppointmentManagementError((AppointmentManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    /// <summary>
    ///     Transforms the result of a command that returns no value into an action result.
    /// </summary>
    public static IActionResult ToActionResultFromResult(
        ControllerBase controller,
        Result result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCodeFromAppointmentManagementError((AppointmentManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    /// <summary>
    ///     Transforms the result of a query by id (a direct entity, not a Result) into an action result.
    /// </summary>
    public static IActionResult ToActionResultFromQuery<T>(
        ControllerBase controller,
        T? entity,
        AppointmentManagementError notFoundError,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> successAction) where T : class
    {
        if (entity is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromAppointmentManagementError(notFoundError),
                notFoundError,
                errorLocalizer[notFoundError.ToString()]
            );
        return successAction(entity);
    }
}
