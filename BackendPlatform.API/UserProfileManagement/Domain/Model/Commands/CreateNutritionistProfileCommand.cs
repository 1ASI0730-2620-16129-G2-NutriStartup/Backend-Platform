namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public record CreateNutritionistProfileCommand( string UserId, string Specialty, int ExperienceYears);