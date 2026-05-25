namespace FleetManager.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalVehicles {get; set;}
    public int TotalFleetMileage {get; set;}
    public int TotalMaintenanceLogs {get; set;}
    public IEnumerable<ServiceRecord>? RecentServices {get;set;} 
    public decimal TotalMaintenanceCost {get;set;}
    public int VehiclesNeedingService {get;set;}
    public int HealthyVehiclesCount => TotalVehicles - VehiclesNeedingService;
}