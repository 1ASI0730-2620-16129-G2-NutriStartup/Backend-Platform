using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class PatientProfileCommandFromResourceAssembler
{
    public static CreatePatientProfileCommand ToCommandFromResource(CreatePatientProfileResource resource) =>
        new(resource.UserId, resource.FirstName, resource.LastName, resource.Age,
            resource.Gender, resource.Weight, resource.Height);

    public static UpdatePatientProfileCommand ToCommandFromResource(UpdatePatientProfileResource resource, int id) =>
        new(id, resource.UserId, resource.FirstName, resource.LastName, resource.Age,
            resource.Gender, resource.Weight, resource.Height);
}
