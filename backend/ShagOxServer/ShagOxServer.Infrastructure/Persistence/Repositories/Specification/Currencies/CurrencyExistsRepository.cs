using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyExistsRepository
    : ExistsRepository<Currency>,
      ICurrencyExistsRepository
{
    public CurrencyExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _db.Currencies
            .AnyAsync(c => c.Code == code,
                cancellationToken);
    }

    public async Task<bool> ExistsBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        return await _db.Currencies
            .AnyAsync(c => c.Symbol == symbol,
                cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _db.Currencies
            .AnyAsync(c => c.Name == name,
                cancellationToken);
    }
}
