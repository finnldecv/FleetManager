using FleetManager.Data;
using FleetManager.Interfaces;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers;

public class HomeController : Controller
{
    private IVehicleService _service;
    public HomeController(IVehicleService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
}