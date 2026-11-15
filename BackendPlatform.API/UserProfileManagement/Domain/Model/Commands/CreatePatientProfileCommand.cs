namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record CreatePatientProfileCommand(string UserId, string FirstName, string LastName, int Age, string Gender, decimal Weight, decimal Height);