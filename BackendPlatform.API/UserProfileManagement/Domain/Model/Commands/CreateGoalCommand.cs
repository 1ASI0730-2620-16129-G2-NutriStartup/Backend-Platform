namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public record CreateGoalCommand(int UserProfileId, string Type, decimal TargetWeight, DateOnly TargetDate, string Status);