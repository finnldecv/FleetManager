using FleetManager.Models;
using Microsoft.AspNetCore.Mvc;
using FleetManager.Interfaces;

namespace FleetManager.Controllers;

public class RecordsController : Controller
{
    private IVehicleService _vehicleService;
    private IRecordService _recordService;
    public RecordsController(IVehicleService vehicleService, IRecordService recordService)
    {
        _vehicleService = vehicleService;
        _recordService = recordService;
    }
    
    public async Task<IActionResult> Create(int vehicleId)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
        if (vehicle == null)
        {
            return NotFound("Vehicle not found.");
        }

        ViewBag.VehicleName = $"{vehicle.Make} {vehicle.Model}";
        ViewBag.CurrentMileage = vehicle.CurrentMileage;

        var record = new ServiceRecord
        {
            VehicleId = vehicleId,
            ServiceDate = DateTime.Today,
            MileageAtService = vehicle.CurrentMileage
        };

        return View(record);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServiceRecord record)
    {
        if (ModelState.IsValid)
        {
            await _recordService.AddRecordAsync(record);
            return RedirectToAction("Details", "Vehicles", new {id = record.VehicleId });
        }
        return View(record);
    }
}