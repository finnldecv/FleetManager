using FleetManager.Data;
using FleetManager.Models;
using FleetManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private AppDbContext _dbContext;
    public VehicleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _dbContext.Vehicles
            .Include(v => v.Mechanic)
            .Include(v => v.ServiceRecords)
            .ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        return await _dbContext.Vehicles
            .Include(v => v.ServiceRecords)
            .Include(v => v.Mechanic)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        _dbContext.Vehicles.Add(vehicle);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateVehicleAsync(Vehicle vehicle)
    {
        _dbContext.Vehicles.Update(vehicle);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteVehicleAsync(int id)
    {
        var vehicle = await _dbContext.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            vehicle.IsDeleted = true;
            _dbContext.Vehicles.Update(vehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}