namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

public sealed record NutritionistProfileResource(int Id, string UserId, string Specialty, int ExperienceYears);
public sealed record CreateNutritionistProfileResource(string UserId, string Specialty, int ExperienceYears);
public sealed record UpdateNutritionistProfileResource(string UserId, string Specialty, int ExperienceYears);
