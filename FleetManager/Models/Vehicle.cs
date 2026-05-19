using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models;

public class Vehicle
{
    public int Id { get; set; }
    [Required]
    [StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters.")]
    public string VIN { get; set; } = string.Empty;
    [Required(ErrorMessage = "The Vehicle Make is required.")]
    [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters.")]
    public string Make { get; set; } = string.Empty;
    [Required(ErrorMessage = "The Vehicle Model is required.")]
    [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
    public string Model { get; set; } = string.Empty;
    [Required]
    [Range(1950, 2100, ErrorMessage = "Please enter a valid year.")]
    public int Year { get; set; }
    [Required]
    [Range(0, 1000000, ErrorMessage = "Mileage must be a positive number")]
    [Display(Name = "Starting Mileage")]
    public int CurrentMileage { get; set; }
    [StringLength(15, ErrorMessage = "License Plate cannot exceed 15 characters.")]
    [Display(Name = "License Plate")]
    public string? LicensePlate { get; set; }
    public List<ServiceRecord> ServiceRecords { get; set; } = new();
    public bool IsDeleted { get; set; } = false;
}