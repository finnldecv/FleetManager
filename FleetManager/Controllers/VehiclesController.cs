using FleetManager.Data;
using FleetManager.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FleetManager.Services;
using FleetManager.Interfaces;

namespace FleetManager.Controllers;

public class VehiclesController : Controller
{
    private IVehicleService _vehicleService;
    public VehiclesController(IVehicleService service)
    {
        _vehicleService = service;
    }
    public async Task<IActionResult> Index()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
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
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if(vehicle == null)
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