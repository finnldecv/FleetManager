using FleetManager.Models;
using FleetManager.Repositories;
using FleetManager.Interfaces;
using FleetManager.Helpers;
using FleetManager.Data;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private AppDbContext _context;

    public RecordService(IRecordRepository recordRepo, IVehicleRepository vehicleRepo, AppDbContext context)
    {
        _recordRepository = recordRepo;
        _vehicleRepository = vehicleRepo;
        _context = context;
    }

    public async Task<PaginatedList<ServiceRecord>> GetAllRecordsAsync(string? searchString, int pageNumber, int pageSize)
    {

        var records = _context.ServiceRecords.Include(sr => sr.Vehicle).AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            var searchLower = searchString.ToLower();
            records = records.Where(sr =>
                sr.Description.ToLower().Contains(searchLower) ||
                (sr.Vehicle !=null && sr.Vehicle.Make.ToLower().Contains(searchLower)) ||
                (sr.Vehicle !=null && sr.Vehicle.Model.ToLower().Contains(searchLower))
            );
        }

        records = records.OrderByDescending(sr => sr.ServiceDate);

        return  await PaginatedList<ServiceRecord>.CreateAsync(records, pageNumber, pageSize);
    }

    public async Task<ServiceRecord?> GetRecordByIdAsync(int id)
    {
        return await _recordRepository.GetRecordByIdAsync(id);
    }

    public async Task AddRecordAsync(ServiceRecord serviceRecord)
    {
        await _recordRepository.AddRecordAsync(serviceRecord);
        var vehicle = await _vehicleRepository.GetVehicleByIdAsync(serviceRecord.VehicleId);
        if (vehicle != null && serviceRecord.MileageAtService > vehicle.CurrentMileage)
        {
            vehicle.CurrentMileage = serviceRecord.MileageAtService;
            await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }
    }

    public async Task UpdateRecordAsync(ServiceRecord serviceRecord)
    {
        await _recordRepository.UpdateRecordAsync(serviceRecord);
        var vehicle = await _vehicleRepository.GetVehicleByIdAsync(serviceRecord.VehicleId);
        if (vehicle != null && serviceRecord.MileageAtService > vehicle.CurrentMileage)
        {
            vehicle.CurrentMileage = serviceRecord.MileageAtService;
            await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }
    }

    public async Task DeleteRecordAsync(int id)
    {
        await _recordRepository.DeleteRecordAsync(id);
    }
}