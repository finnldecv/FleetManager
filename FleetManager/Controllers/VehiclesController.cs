using FleetManager.Models;
using Microsoft.AspNetCore.Mvc;
using FleetManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FleetManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetManager.Controllers;

[Authorize]
public class VehiclesController : Controller
{
    private IVehicleService _vehicleService;
    private UserManager<ApplicationUser> _userManager;
    public VehiclesController(IVehicleService vehicleService, UserManager<ApplicationUser> userManager)
    {
        _vehicleService = vehicleService;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index(string? searchString, int? pageNumber)
    {
        ViewData["CurrentFilter"] = searchString;
        
        int pageSize = 5;
        int pageIndex = pageNumber ?? 1;

        var vehicles = await _vehicleService.GetAllVehiclesAsync(searchString, pageIndex, pageSize);

        return View(vehicles);
    }
    public async Task<IActionResult> Details(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
        {
            return NotFound("Vehicle not found or has been deleted.");
        }
        return View(vehicle);
    }
    public async Task<IActionResult> Create()
    {
        var allUsers = await _userManager.Users.ToListAsync();

        ViewBag.MechanicList = new SelectList(allUsers, "Id", "UserName");

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            await _vehicleService.AddVehicleAsync(vehicle);
            return RedirectToAction("Index", "Vehicles");
        }
        return View(vehicle);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var allUsers = await _userManager.Users.ToListAsync();

        ViewBag.MechanicList = new SelectList(allUsers, "Id", "UserName");

        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

        if (vehicle == null)
        {
            return NotFound("Vehicle not found or has been deleted");
        }

        return View(vehicle);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Vehicle vehicle)
    {
        if (ModelState.IsValid)
        {
            await _vehicleService.UpdateVehicleAsync(vehicle);
            return RedirectToAction("Details", "Vehicles", new { id = vehicle.Id });
        }
        return View(vehicle);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
        {
            return NotFound("Vehicle is not found or it has been deleted");
        }
        return View(vehicle);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _vehicleService.DeleteVehicleAsync(id);
        return RedirectToAction("Index", "Vehicles");
    }
}