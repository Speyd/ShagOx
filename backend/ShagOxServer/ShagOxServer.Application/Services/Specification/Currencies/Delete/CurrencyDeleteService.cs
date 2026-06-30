using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Delete;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService : ICurrencyDeleteService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyDeleteService(
        ICurrencyRepository currencyRepository)
    {
        _repository = currencyRepository;
    }

    public async Task<Result<CurrencyDeleteResponse>> DeleteCurrencyAsync(
        int id)
    {
        var currency = await _repository.GetByIdAsync(id);
        if (currency is null)
            return Result<CurrencyDeleteResponse>.NotFound("Currency");

        await _repository.DeleteAsync(currency);
        return Result<CurrencyDeleteResponse>.Success(
          new CurrencyDeleteResponse(
              currency.Id,
              DateTime.UtcNow
          )
      );
    }
}
