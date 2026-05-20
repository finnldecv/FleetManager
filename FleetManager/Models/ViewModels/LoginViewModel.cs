using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your Email or Username.")]
    [Display(Name = "Email or Username")]
    public string EmailOrUsername { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}