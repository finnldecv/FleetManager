using FleetManager.Data;
using FleetManager.Models;
using FleetManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Repositories;

public class RecordRepository : IRecordRepository
{
    private readonly AppDbContext _context;

    public RecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRecordAsync(ServiceRecord serviceRecord)
    {
        _context.ServiceRecords.Add(serviceRecord);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateRecordAsync(ServiceRecord serviceRecord)
    {
        _context.ServiceRecords.Update(serviceRecord);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecordAsync(int id)
    {
        var record = await _context.ServiceRecords.FindAsync(id);
        if (record != null)
        {
            record.IsDeleted = true;
            _context.ServiceRecords.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}