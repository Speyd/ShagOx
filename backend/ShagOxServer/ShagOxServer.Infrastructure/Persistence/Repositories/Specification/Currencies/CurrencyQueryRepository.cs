using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

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

    public async Task<List<Currency>> GetPagedAsync(
      PaginationParams pagination)
    {
        return await _db.Currencies
            .WithPagination(pagination)
            .ToListAsync();
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

    public async Task<List<Currency>> Search(
      CurrencySearchFilter filter,
      PaginationParams pagination)
    {
        return await _db.Currencies
            .Filter(filter)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
