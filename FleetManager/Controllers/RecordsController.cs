using FleetManager.Models;
using Microsoft.AspNetCore.Mvc;
using FleetManager.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace FleetManager.Controllers;

public class RecordsController : Controller
{
    private IVehicleService _vehicleService;
    private IRecordService _recordService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public RecordsController(IVehicleService vehicleService, IRecordService recordService, IWebHostEnvironment webHostEnvironment)
    {
        _vehicleService = vehicleService;
        _recordService = recordService;
        _webHostEnvironment = webHostEnvironment;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string? searchString, int? pageNumber)
    {
        ViewData["CurrentFilter"] = searchString;

        int pageSize = 10;
        int pageIndex = pageNumber ?? 1;

        var records = await _recordService.GetAllRecordsAsync(searchString, pageIndex, pageSize);

        return View(records);
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
    public async Task<IActionResult> Create(ServiceRecord record, IFormFile receipt)
    {
        if (ModelState.IsValid)
        {
            if (receipt != null && receipt.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "receipts");

                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + receipt.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await receipt.CopyToAsync(fileStream);
                }
                record.ReceiptUrl = "documents/receipts/" + uniqueFileName;
            }
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
    public async Task<IActionResult> Edit(ServiceRecord record, IFormFile receipt)
    {
        if (ModelState.IsValid)
        {
            if (receipt != null && receipt.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "receipts");

                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + receipt.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await receipt.CopyToAsync(fileStream);
                }
                record.ReceiptUrl = "documents/receipts/" + uniqueFileName;
            }
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