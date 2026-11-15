namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record UpdateGoalCommand(int GoalId, int UserProfileId, string Type, decimal TargetWeight, DateOnly TargetDate, string Status);
