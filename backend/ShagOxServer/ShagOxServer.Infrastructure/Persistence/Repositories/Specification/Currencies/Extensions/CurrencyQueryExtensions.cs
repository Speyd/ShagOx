using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;
public static class CurrencyQueryExtensions
{
    public static IQueryable<CurrencyCacheContext> SelectCacheInfo(
       this IQueryable<Currency> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => x.SelectCacheInfo());
    }

    public static CurrencyCacheContext SelectCacheInfo(
       this Currency query,
        EntityStatus status = EntityStatus.Active)
    {
        return new CurrencyCacheContext(
            query.Id,
            query.Code,
            query.Symbol,
            status);
    }
}
