using FleetManager.Helpers;
using FleetManager.Models;

namespace FleetManager.Interfaces;

public interface IRecordService
{
    Task<PaginatedList<ServiceRecord>> GetAllRecordsAsync(string? searchString, int pageNumber, int pageSize);
    Task<ServiceRecord?> GetRecordByIdAsync(int id);
    Task AddRecordAsync(ServiceRecord serviceRecord);
    Task UpdateRecordAsync(ServiceRecord serviceRecord);
    Task DeleteRecordAsync(int id);
}