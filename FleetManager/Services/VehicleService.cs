using FleetManager.Interfaces;
using FleetManager.Models;

namespace FleetManager.Services;

public class VehicleService : IVehicleService
{
    private IVehicleRepository _vehicleRepository;
    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _vehicleRepository.GetAllVehiclesAsync();
    }
    public async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        return await _vehicleRepository.GetVehicleByIdAsync(id);
    }
    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        vehicle.VIN = vehicle.VIN.ToUpper();
        await _vehicleRepository.AddVehicleAsync(vehicle);
    }
    public async Task UpdateVehicleAsync(Vehicle vehicle)
    {
        vehicle.VIN = vehicle.VIN.ToUpper();
        await _vehicleRepository.UpdateVehicleAsync(vehicle);
    }
    public async Task DeleteVehicleAsync(int id)
    {
        var car = await _vehicleRepository.GetVehicleByIdAsync(id);
        if (car == null)
        {
            throw new Exception("Vehicle not found.");
        }
        await _vehicleRepository.DeleteVehicleAsync(id);
    }
}