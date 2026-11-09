namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public record CreatePatientProfileCommand( string UserId, string Name, int Age, string Gender, decimal Weight, decimal Height);