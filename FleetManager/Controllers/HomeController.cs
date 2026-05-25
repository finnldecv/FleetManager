using FleetManager.Interfaces;
using FleetManager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers;

public class HomeController : Controller
{

    private IVehicleRepository _vehicleRepo;

    private IRecordRepository _recordRepo;
    
    public HomeController(IVehicleRepository vehicleRepo, IRecordRepository recordRepo)
    {
        _vehicleRepo = vehicleRepo;
        _recordRepo = recordRepo;
    }
    public async Task<IActionResult> Index()
    {
        var vehicles = await _vehicleRepo.GetAllVehiclesAsync();
        var records  = await _recordRepo.GetAllRecordsAsync();
        return View(new DashboardViewModel
        {
            TotalVehicles = vehicles.Count(),
            TotalFleetMileage = vehicles.Sum(v => v.CurrentMileage),
            TotalMaintenanceLogs = records.Count(),
            RecentServices = records.OrderByDescending(r => r.ServiceDate).Take(5),
            TotalMaintenanceCost = records.Sum(r => r.Cost),
            VehiclesNeedingService = vehicles.Count(v => v.NeedsMaintenance)
        });
    }
}