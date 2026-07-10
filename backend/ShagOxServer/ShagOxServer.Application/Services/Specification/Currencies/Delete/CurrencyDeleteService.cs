using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService : ICurrencyDeleteService
{
    private readonly IConditionRepository _repository;

    public CurrencyDeleteService(
        IConditionRepository currencyRepository)
    {
        _repository = currencyRepository;
    }

    public async Task<Result<CurrencyDeleteResponse>> DeleteCurrencyAsync(
        int id)
    {
        var currency = await _repository.GetByIdAsync(id);
        if (currency is null)
            return Result<CurrencyDeleteResponse>.NotFound("Currency");

        _repository.DeleteAsync(currency);
        return Result<CurrencyDeleteResponse>.Success(
          new CurrencyDeleteResponse(
              currency.Id,
              DateTime.UtcNow
          )
      );
    }
}
