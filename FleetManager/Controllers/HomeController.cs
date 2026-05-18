using FleetManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers;

public class HomeController : Controller
{
    private IVehicleService _vehicleService;
    public HomeController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
}