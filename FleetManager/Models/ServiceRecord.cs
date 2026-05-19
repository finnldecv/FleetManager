using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models;

public class ServiceRecord
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please descrive the service performed.")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = String.Empty;
    [Required(ErrorMessage = "The date of service is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Service Date")]
    public DateTime ServiceDate { get; set; } = DateTime.UtcNow;
    [Required]
    [Range(0, 1000000, ErrorMessage = "Mileage must be a positive number.")]
    [Display(Name = "Mileage at Service")]
    public int MileageAtService { get; set; }
    [Required]
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public bool IsDeleted { get; set; } = false;
}