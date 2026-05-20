using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(50)]
    public string FirstName {get;set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string LastName {get;set;} = string.Empty;
}