using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyQueryRepository : BaseRepository, ICurrencyQueryRepository
{
    public CurrencyQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Currency?> GetByCodeAsync(string code)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Currency?> GetBySymbolAsync(string symbol)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Symbol == symbol);
    }

    public async Task<List<Currency>> SearchByCode(
      string code,
      PaginationParams pagination)
    {
        return await _db.Currencies
            .Where(c => c.Code.Contains(code))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<Currency>> SearchByName(
      string name,
      PaginationParams pagination)
    {
        return await _db.Currencies
           .Where(c => c.Name.Contains(name))
           .Skip((pagination.Page - 1) * pagination.PageSize)
           .Take(pagination.PageSize)
           .ToListAsync();
    }
}
