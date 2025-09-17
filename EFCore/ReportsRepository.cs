
using Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class ReportsRepository : IReportsRepository
{
    private readonly MyDbContext _context;

    public ReportsRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reports>> GettingAllReportsAsync() => await _context.Reports.ToListAsync();
    public async Task DeleteReportsAsync(Reports reports)
    {
        _context.Remove(reports);
        await _context.SaveChangesAsync();
    }
}

