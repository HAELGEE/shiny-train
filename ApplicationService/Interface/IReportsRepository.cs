using Entity;

namespace EFCore;

public interface IReportsRepository
{
    Task<List<Reports>> GettingAllReportsAsync();
    Task DeleteReportsAsync(Reports reports);
}