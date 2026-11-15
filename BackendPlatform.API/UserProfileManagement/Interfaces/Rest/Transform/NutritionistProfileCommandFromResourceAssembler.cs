using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class NutritionistProfileCommandFromResourceAssembler
{
    public static CreateNutritionistProfileCommand ToCommandFromResource(CreateNutritionistProfileResource resource) =>
        new(resource.UserId, resource.Specialty, resource.ExperienceYears);

    public static UpdateNutritionistProfileCommand ToCommandFromResource(UpdateNutritionistProfileResource resource, int id) =>
        new(id, resource.UserId, resource.Specialty, resource.ExperienceYears);
}
