namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

public sealed record GoalResource(int Id, int UserProfileId, string Type, decimal TargetWeight, DateOnly TargetDate, string Status);

public sealed record CreateGoalResource(int UserProfileId, string Type, decimal TargetWeight, DateOnly TargetDate, string Status);

public sealed record UpdateGoalResource(int UserProfileId, string Type, decimal TargetWeight, DateOnly TargetDate, string Status);
