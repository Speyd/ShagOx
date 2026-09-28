using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyQueryRepository 
    : QueryRepository<Currency, CurrencySearchFilter>, 
    ICurrencyQueryRepository
{
    public CurrencyQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<Currency> ApplyFilter(
        IQueryable<Currency> query,
        CurrencySearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<Currency?> GetByCodeAsync(
        string code)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Currency?> GetBySymbolAsync(
        string symbol)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Symbol == symbol);
    }
}