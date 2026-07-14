using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public static class CurrencyCreater
{
    public static Currency CreateCurrency(
        CurrencyCreateRequest request)
    {
        return new Currency
        {
            Code = request.Code,
            Symbol = request.Symbol,
            Name = request.Name,
        };
    }
}