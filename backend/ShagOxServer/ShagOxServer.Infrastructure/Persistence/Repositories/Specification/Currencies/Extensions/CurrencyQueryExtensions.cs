using ShagOxServer.Application.DTOs.Specification.Currencies.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;
public static class CurrencyQueryExtensions
{
    public static IQueryable<CurrencyCacheInfo> SelectCacheInfo(
       this IQueryable<Currency> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => x.SelectCacheInfo());
    }

    public static CurrencyCacheInfo SelectCacheInfo(
       this Currency query,
        EntityStatus status = EntityStatus.Active)
    {
        return new CurrencyCacheInfo(
            query.Id,
            query.Code,
            query.Symbol,
            status);
    }
}
