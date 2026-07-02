using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Update;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService : ICurrencyUpdateService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyUpdateService(
        ICurrencyRepository currencyRepository)
    {
        _repository = currencyRepository;
    }

    public async Task<Result<CurrencyUpdateResponse>> UpdateCurrencyAsync(
        int currencyId,
        CurrencyUpdateRequest request)
    {
        if (request.Code is not null &&
            await _repository.ExistsByCodeAsync(request.Code))
        {
            return Result<CurrencyUpdateResponse>
                .AlreadyExists("Currency code");
        }
        if (request.Name is not null &&
            await _repository.ExistsByNameAsync(request.Name))
        {
            return Result<CurrencyUpdateResponse>
                .AlreadyExists("Currency name");
        }

        var currency = await _repository.GetByIdAsync(currencyId);
        if (currency is null)
            return Result<CurrencyUpdateResponse>.NotFound("Currency");

        var updatedCount = ApplyUpdates(currency, request);

        var result = new CurrencyUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CurrencyUpdateResponse>.Success(result);

        await _repository.UpdateAsync(currency);

        return Result<CurrencyUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
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
