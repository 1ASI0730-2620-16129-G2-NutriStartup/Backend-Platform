using System.ComponentModel.DataAnnotations;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

public class PatientProfile
{
    public int Id { get; set; }
    
    [Required, MaxLength(120)]
    public string UserId { get; set; } = string.Empty;
    
    [Required, MaxLength(80)]
    public string Name { get; set; } = string.Empty;
    
    [Range(1, 110)]
    public int Age { get; set; }
    
    [Required, MaxLength(20)]
    public string Gender { get; set; } = string.Empty;
    
    [Range(typeof(decimal), "0.01", "999.99")]
    public decimal Weight { get; set; }
    
    [Range(typeof(decimal), "0.01", "999.99")]
    public decimal Height { get; set; }
}