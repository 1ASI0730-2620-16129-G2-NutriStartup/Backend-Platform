namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

public sealed record PatientProfileResource(
    int Id, string UserId, string FirstName, string LastName, int Age, string Gender,
    decimal Weight, decimal Height, decimal Bmi);

public sealed record CreatePatientProfileResource(
    string UserId, string FirstName, string LastName, int Age, string Gender,
    decimal Weight, decimal Height);

public sealed record UpdatePatientProfileResource(
    string UserId, string FirstName, string LastName, int Age, string Gender,
    decimal Weight, decimal Height);
