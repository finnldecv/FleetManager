using FleetManager.Helpers;
using FleetManager.Models;

namespace FleetManager.Interfaces;

public interface IVehicleService
{
    Task<PaginatedList<Vehicle>> GetAllVehiclesAsync(string? searchString, int pageNumber, int pageSize);
    Task<Vehicle?> GetVehicleByIdAsync(int id);
    Task AddVehicleAsync(Vehicle vehicle);
    Task UpdateVehicleAsync(Vehicle vehicle);
    Task DeleteVehicleAsync(int id);
} 