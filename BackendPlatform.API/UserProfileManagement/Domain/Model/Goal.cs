using System.ComponentModel.DataAnnotations;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model;

public class Goal
{
    public int Id { get; set; }
    
    [Range(1, int.MaxValue)]
    public int UserProfileId { get; set; }
    
    [Required, MaxLength(80)]
    public string Type { get; set; } =  string.Empty;

    [Range(typeof(decimal), "0.01", "999.99")]
    public decimal TargetWeight { get; set; }
    
    public DateOnly TargetDate { get; set; }
    
    [Required, MaxLength(40)]
    public string Status { get; set; } = string.Empty;
}