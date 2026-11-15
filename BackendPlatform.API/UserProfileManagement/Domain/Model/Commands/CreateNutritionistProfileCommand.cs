namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record CreateNutritionistProfileCommand(string UserId, string Specialty, int ExperienceYears);