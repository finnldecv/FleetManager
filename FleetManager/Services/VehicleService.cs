using FleetManager.Data;
using FleetManager.Helpers;
using FleetManager.Interfaces;
using FleetManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Services;

public class VehicleService : IVehicleService
{
    private IVehicleRepository _vehicleRepository;
    private AppDbContext _context;
    public VehicleService(IVehicleRepository vehicleRepository, AppDbContext context)
    {
        _vehicleRepository = vehicleRepository;
        _context = context;
    }
    public async Task<PaginatedList<Vehicle>> GetAllVehiclesAsync(string? searchString, int pageNumber, int pageSize)
    {
        var vehicles = _context.Vehicles.Include(v => v.Mechanic).Include(v => v.ServiceRecords).AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            var searchLower = searchString.ToLower();
            vehicles = vehicles.Where(v =>
                v.Make.ToLower().Contains(searchLower) ||
                v.Model.ToLower().Contains(searchLower) ||
                v.VIN.ToLower().Contains(searchLower));
        }

        vehicles = vehicles.OrderBy(v => v.Make).ThenBy(v => v.Model);

        return await PaginatedList<Vehicle>.CreateAsync(vehicles, pageNumber, pageSize);
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