using ShagOxServer.Application.DTOs.Specification.Currencies.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Mapping;
public static class CurrencyCacheMapper
{
    public static CurrencyCacheInfo ToInfo(
        Currency x,
        EntityStatus status = EntityStatus.Active)
    {
        return new CurrencyCacheInfo
        (
            x.Id,
            x.Code,
            x.Symbol,
            status
        );
    }
}
