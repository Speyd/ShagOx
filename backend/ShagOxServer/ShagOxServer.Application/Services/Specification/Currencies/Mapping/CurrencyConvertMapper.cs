using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Mapping;
public static class CurrencyConvertMapper
{
    public static CurrencyConvertDto ToDto(
        Currency from,
        Currency? to,
        decimal? convertPrice,
        decimal coefficient)
    {
        return new CurrencyConvertDto
        (
            CurrencyMapper.ToShortDto(from),
            to is null ? null: CurrencyMapper.ToShortDto(to),
            convertPrice,
            coefficient
        );
    }
}
