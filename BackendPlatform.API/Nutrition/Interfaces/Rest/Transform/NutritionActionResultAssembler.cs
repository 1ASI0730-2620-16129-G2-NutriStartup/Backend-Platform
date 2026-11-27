using BackendPlatform.API.Nutrition.Domain.Model;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Shared.Application.Model;
using BackendPlatform.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using BackendPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.Extensions.Localization;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class NutritionActionResultAssembler
{
    private static int ToStatusCodeFromNutritionError(NutritionError error)
    {
        return error switch
        {
            NutritionError.InvalidData => StatusCodes.Status400BadRequest,
            NutritionError.NutritionPlanNotFound => StatusCodes.Status404NotFound,
            NutritionError.MealPlanNotFound => StatusCodes.Status404NotFound,
            NutritionError.FoodRecommendationNotFound => StatusCodes.Status404NotFound,
            NutritionError.OperationCancelled => StatusCodes.Status409Conflict,
            NutritionError.DatabaseError => StatusCodes.Status500InternalServerError,
            NutritionError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
    
    public static IActionResult ToActionResultFromCreateNutritionPlanResult(
        ControllerBase controller,
        Result<NutritionPlan> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<NutritionPlan, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
    public static IActionResult ToActionResultFromGetNutritionPlanByIdResult(
        ControllerBase controller,
        NutritionPlan? nutritionPlan,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<NutritionPlan, IActionResult> successAction)
    {
        if (nutritionPlan is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromNutritionError(NutritionError.NutritionPlanNotFound),
                NutritionError.NutritionPlanNotFound,
                errorLocalizer[nameof(NutritionError.NutritionPlanNotFound)]
            );
        return successAction(nutritionPlan);
    }
    
    public static IActionResult ToActionResultFromUpdateNutritionPlanResult(
        ControllerBase controller,
        Result<NutritionPlan> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<NutritionPlan, IActionResult> successAction)
    {
        if (result.IsSuccess)
            return successAction(result.Value!);

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message);
    }
    
    public static IActionResult ToActionResultFromDeleteNutritionPlanResult(
        ControllerBase controller,
        Result<bool> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message);
    }
    
    public static IActionResult ToActionResultFromCreateMealPlanResult(
        ControllerBase controller,
        Result<MealPlan> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<MealPlan, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
    public static IActionResult ToActionResultFromGetMealPlanByIdResult(
        ControllerBase controller,
        MealPlan? mealPlan,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<MealPlan, IActionResult> successAction)
    {
        if (mealPlan is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromNutritionError(NutritionError.MealPlanNotFound),
                NutritionError.MealPlanNotFound,
                errorLocalizer[nameof(NutritionError.MealPlanNotFound)]
            );
        return successAction(mealPlan);
    }
    
    public static IActionResult ToActionResultFromGetAllMealPlansByNutritionPlanIdResult(
        ControllerBase controller,
        IEnumerable<MealPlan> mealPlans,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IEnumerable<MealPlan>, IActionResult> successAction)
    {
        return successAction(mealPlans);
    }
    
    public static IActionResult ToActionResultFromDeleteMealPlanResult(
        ControllerBase controller,
        Result<bool> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message);
    }
    
    public static IActionResult ToActionResultFromCreateFoodRecommendationResult(
        ControllerBase controller,
        Result<FoodRecommendation> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<FoodRecommendation, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
    public static IActionResult ToActionResultFromGetFoodRecommendationByIdResult(
        ControllerBase controller,
        FoodRecommendation? foodRecommendation,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<FoodRecommendation, IActionResult> successAction)
    {
        if (foodRecommendation is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromNutritionError(NutritionError.FoodRecommendationNotFound),
                NutritionError.FoodRecommendationNotFound,
                errorLocalizer[nameof(NutritionError.FoodRecommendationNotFound)]
            );
        return successAction(foodRecommendation);
    }
    
    public static IActionResult ToActionResultFromDeleteFoodRecommendationResult(
        ControllerBase controller,
        Result<bool> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        var statusCode = ToStatusCodeFromNutritionError((NutritionError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller,
            statusCode,
            result.Error,
            result.Message);
    }
    
    public static IActionResult ToActionResultFromGetAllFoodRecommendationsByMealPlanIdResult(
        ControllerBase controller,
        IEnumerable<FoodRecommendation> foodRecommendations,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IEnumerable<FoodRecommendation>, IActionResult> successAction)
    {
        return successAction(foodRecommendations);
    }
}
