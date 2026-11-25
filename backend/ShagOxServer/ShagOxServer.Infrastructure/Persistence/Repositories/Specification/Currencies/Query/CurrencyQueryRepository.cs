using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Query;
public partial class CurrencyQueryRepository 
    : SearchRepository<Currency, CurrencySearchFilter>, 
    ICurrencyQueryRepository
{
    public CurrencyQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<Currency?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Currencies
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c => c.Code == code,
                cancellationToken);
    }

    public async Task<Currency?> GetBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Currencies
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c => c.Symbol == symbol,
                cancellationToken);
    }

    public async Task<Currency?> GetByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => u.Currency)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CurrencyCacheContext?> GetCahceInfoByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var user = await _db.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Currency)
            .FirstOrDefaultAsync(cancellationToken);

        return user?.SelectCacheInfo();
    }
}
