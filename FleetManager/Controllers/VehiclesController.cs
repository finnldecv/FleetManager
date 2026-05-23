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
    private AppDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    public VehiclesController(IVehicleService vehicleService, AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _vehicleService = vehicleService;
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index(string searchString)
    {
        ViewData["CurrentFilter"] = searchString;
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            var searchLower = searchString.ToLower();

            vehicles = vehicles.Where(v =>
                v.Make.ToLower().Contains(searchLower) ||
                v.Model.ToLower().Contains(searchLower) ||
                v.VIN.ToLower().Contains(searchLower)
            ).ToList();
        }
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