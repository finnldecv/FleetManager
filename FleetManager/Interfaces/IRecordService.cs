using FleetManager.Models;

namespace FleetManager.Interfaces;

public interface IRecordService
{
    Task<IEnumerable<ServiceRecord>> GetAllRecordsAsync();
    Task<ServiceRecord?> GetRecordByIdAsync(int id);
    Task AddRecordAsync(ServiceRecord serviceRecord);
    Task UpdateRecordAsync(ServiceRecord serviceRecord);
    Task DeleteRecordAsync(int id);
}