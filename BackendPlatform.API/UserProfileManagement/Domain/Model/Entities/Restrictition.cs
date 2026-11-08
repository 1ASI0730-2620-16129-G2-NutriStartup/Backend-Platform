using System.ComponentModel.DataAnnotations;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;

public class Restriction
{
    public int Id { get; set; }
    
    [Required, MaxLength(128)]
    public string UserId {get; set;} = string.Empty;
    
    [Required, MaxLength(80)]
    public string Type { get; set; } = string.Empty;
    
    [Required, MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}