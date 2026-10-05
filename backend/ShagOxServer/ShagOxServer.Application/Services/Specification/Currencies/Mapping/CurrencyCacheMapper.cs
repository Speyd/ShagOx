using ShagOxServer.Application.DTOs.Specification.Currencies.Cache;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Mapping;
public static class CurrencyCacheMapper
{
    public static CurrencyCacheInfo ToInfo(
        Currency x)
    {
        return new CurrencyCacheInfo
        (
            x.Id,
            x.Code,
            x.Symbol
        );
    }
}