using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class GoalResourceFromEntityAssembler
{
    public static GoalResource ToResourceFromEntity(Goal entity) =>
        new(entity.Id, entity.PatientProfileId, entity.Type, entity.TargetWeight, entity.TargetDate, entity.Status);
}
