using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManager.Models;


public enum ServiceCategory
{
    [Display(Name = "Preventative Maintenance")]
    Maintenance,
    [Display(Name = "Unexpected Repair")]
    Repair,
    [Display(Name = "Anual Inspection")]
    Inspection
}
public class ServiceRecord
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Service Type")]
    public ServiceCategory Category { get; set; }

    [Required(ErrorMessage = "Please descrive the service performed.")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = String.Empty;

    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Cost {get;set;}

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