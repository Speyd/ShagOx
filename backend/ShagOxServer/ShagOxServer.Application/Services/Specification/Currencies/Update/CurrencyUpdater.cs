using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public static class CurrencyUpdater
{
    public static int ApplyUpdates(
        Currency currency,
        CurrencyUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            currency.Code = request.Code;
            countUpdated++;
        }

        if (request.Symbol is not null)
        {
            currency.Symbol = request.Symbol;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            currency.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}