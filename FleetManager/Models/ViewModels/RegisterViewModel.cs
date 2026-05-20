using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName {get; set;} = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName {get;set;} = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage ="Username can only contain letters, numbers, and underscores.")]
    public string Username {get;set;} = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;} = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword {get; set;} = string.Empty;
}