using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class PatientProfileResourceFromEntityAssembler
{
    public static PatientProfileResource ToResourceFromEntity(PatientProfile entity) => new(
        entity.Id, entity.UserId, entity.FirstName, entity.LastName, entity.Age,
        entity.Gender, entity.Measurements.WeightKg, entity.Measurements.HeightMeters, entity.Measurements.Bmi);
}
