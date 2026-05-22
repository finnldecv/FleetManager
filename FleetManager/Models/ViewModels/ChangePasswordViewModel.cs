using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models.ViewModels;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string OldPassword { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The new password do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}