namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;

public enum UserProfileManagementError
{
    InvalidData, PatientProfileAlreadyExists, NutritionistProfileAlreadyExists, PatientProfileNotFound, NutritionistProfileNotFound, GoalNotFound, PreferenceNotFound, RestrictionNotFound
}
