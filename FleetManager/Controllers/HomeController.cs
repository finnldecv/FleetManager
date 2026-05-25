using FleetManager.Interfaces;
using FleetManager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers;

public class HomeController : Controller
{
    private IVehicleService _vehicleService;
    private IRecordService _recordService;
    public HomeController(IVehicleService vehicleService, IRecordService recordService)
    {
        _vehicleService = vehicleService;
        _recordService = recordService;
    }
    public async Task<IActionResult> Index()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        var records  = await _recordService.GetAllRecordsAsync();
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