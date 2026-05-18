using FleetManager.Data;
using FleetManager.Models;

namespace FleetManager.Models;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        if (context.Vehicles.Any())
        {
            return;
        }
        var vehicles = new Vehicle[]
        {
            new Vehicle { Make = "VinFast", Model = "VF8", VIN = "VF8A1234567890123", CurrentMileage = 15000, IsDeleted = false },
            new Vehicle { Make = "VinFast", Model = "VF9", VIN = "VF9B9876543210987", CurrentMileage = 5000, IsDeleted = false },
            new Vehicle { Make = "Toyota", Model = "Fortuner", VIN = "TOYF5555555555555", CurrentMileage = 120000, IsDeleted = false }
        };
        foreach (var v in vehicles)
        {
            context.Vehicles.Add(v);
        }
        context.SaveChanges();
        var serviceRecords = new ServiceRecord[]
        {
            new ServiceRecord { VehicleId = 1, ServiceDate = DateTime.Now.AddMonths(-6), Description = "Initial 5,000km Inspection & Tire Rotation", MileageAtService = 5100 },
            new ServiceRecord { VehicleId = 1, ServiceDate = DateTime.Now.AddMonths(-1), Description = "Replaced Cabin Air Filter & Firmware Update", MileageAtService = 14800 }
        };
        foreach (var sr in serviceRecords)
        {
            context.ServiceRecords.Add(sr);
        }
        context.SaveChanges();
    }
}