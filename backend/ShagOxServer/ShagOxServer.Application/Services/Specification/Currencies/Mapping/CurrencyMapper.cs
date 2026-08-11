using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Mapping;
public static class CurrencyMapper
{
    public static CurrencyDto ToDto(
        Currency x)
    {
        return new CurrencyDto
        (
            x.Id,
            x.Code,
            x.Symbol,
            x.Name
        );
    }
}