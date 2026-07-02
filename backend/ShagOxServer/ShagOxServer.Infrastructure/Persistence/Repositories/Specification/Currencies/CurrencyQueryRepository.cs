using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyQueryRepository : BaseRepository, ICurrencyQueryRepository
{
    public CurrencyQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Currency?> GetByCodeAsync(string code)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Currency?> GetBySymbolAsync(string symbol)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Symbol == symbol);
    }
}
