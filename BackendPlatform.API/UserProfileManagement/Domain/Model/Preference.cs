using System.ComponentModel.DataAnnotations;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model;

public class Preference
{
    public int Id { get; set; }
    
    [Required, MaxLength(120)]
    public string UserId { get; set; } =  string.Empty;
    
    [MaxLength(500)]
    public string DietPreferences { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string FavoriteFoods { get; set; } = string.Empty;
}