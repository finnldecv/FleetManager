using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models;

public class ServiceRecord
{
    public int Id { get; set; }
    [Required]
    public string Description { get; set; } = String.Empty;
    public DateTime ServiceDate { get; set; } = DateTime.UtcNow;
    public int MileageAtService { get; set; }
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public bool IsDeleted { get; set; } = false;
}