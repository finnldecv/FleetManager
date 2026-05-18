using FleetManager.Models;

namespace FleetManager.Interfaces;

public interface IRecordRepository
{
    Task AddRecordAsync(ServiceRecord serviceRecord);
    Task UpdateRecordAsync(ServiceRecord serviceRecord);
    Task DeleteRecordAsync(int id);
}