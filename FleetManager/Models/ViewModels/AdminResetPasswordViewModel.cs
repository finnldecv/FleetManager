using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models.ViewModels;

public class AdminResetPasswordViewModel
{
    [Required]
    public string UserId { get; set; } = string.Empty;
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}