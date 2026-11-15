namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record UpdateNutritionistProfileCommand(int NutritionistProfileId, string UserId, string Specialty, int ExperienceYears);
