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

    public async Task<IActionResult> Index()
    {
        var records = await _recordService.GetAllRecordsAsync();
        var sortedRecords = records.OrderByDescending(r => r.ServiceDate).ToList();
        return View(sortedRecords);
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
            return RedirectToAction("Details", "Vehicles", new { id = record.VehicleId });
        }
        return View(record);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var record = await _recordService.GetRecordByIdAsync(id);
        if (record == null) return NotFound();
        var vehicle = await _vehicleService.GetVehicleByIdAsync(record.VehicleId);
        ViewBag.VehicleName = vehicle != null ? $"{vehicle.Make} {vehicle.Model}" : "Unknown vehicle";
        return View(record);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ServiceRecord record)
    {
        if (ModelState.IsValid)
        {
            await _recordService.UpdateRecordAsync(record);
            return RedirectToAction("Index");
        }
        return View(record);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var record = await _recordService.GetRecordByIdAsync(id);
        if (record == null) return NotFound();
        return View(record);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _recordService.DeleteRecordAsync(id);
        return RedirectToAction("Index");
    }
}