using System.ComponentModel.DataAnnotations;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model;

public class NutritionistProfile
{
    public int Id { get; set; }
    [Required, MaxLength(128)]
    
    public string UserId { get; set; } = string.Empty;
    [Required, MaxLength(120)]
    
    public string Specialty { get; set; } = string.Empty;
    [Range(0, 80)]
    
    public int ExperienceYears { get; set; }
}