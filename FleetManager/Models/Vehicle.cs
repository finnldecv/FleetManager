using System.ComponentModel.DataAnnotations;

namespace FleetManager.Models;

public class Vehicle
{
    public int Id { get; set; }
    [Required, StringLength(17)]
    public string VIN { get; set; } = string.Empty;
    [Required]
    public string Make { get; set; } = string.Empty;
    [Required]
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int CurrentMileage { get; set; }
    public List<ServiceRecord> ServiceRecords { get; set; } = new();
    public bool IsDeleted { get; set; } = false;
}