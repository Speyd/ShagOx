using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Query;
public partial class CurrencyQueryRepository 
    : SearchRepository<Currency, CurrencySearchFilter>, 
    ICurrencyQueryRepository
{
    public CurrencyQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


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
