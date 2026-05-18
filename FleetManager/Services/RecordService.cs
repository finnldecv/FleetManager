using FleetManager.Models;
using FleetManager.Repositories;
using FleetManager.Interfaces;

namespace FleetManager.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;
    private readonly IVehicleRepository _vehicleRepository;

    public RecordService(IRecordRepository recordRepo, IVehicleRepository vehicleRepo)
    {
        _recordRepository = recordRepo;
        _vehicleRepository = vehicleRepo;
    }

    public async Task<IEnumerable<ServiceRecord>> GetAllRecordsAsync()
    {
        return await _recordRepository.GetAllRecordsAsync();
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