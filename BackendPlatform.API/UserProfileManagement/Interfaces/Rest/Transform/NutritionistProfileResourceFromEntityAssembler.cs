using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class NutritionistProfileResourceFromEntityAssembler
{
    public static NutritionistProfileResource ToResourceFromEntity(NutritionistProfile entity) =>
        new(entity.Id, entity.UserId, entity.Specialty, entity.ExperienceYears);
}
