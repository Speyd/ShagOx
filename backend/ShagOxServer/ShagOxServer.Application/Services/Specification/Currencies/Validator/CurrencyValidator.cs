using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Validator;
public class CurrencyValidator
{
    private readonly ICurrencyRepository _repository;


    public CurrencyValidator(
        ICurrencyRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result<Currency>> GetCurrencyValidator(
        int currencyId)
    {
        var currency = await _repository.GetByIdAsync(currencyId);
        if (currency is null)
            return Result<Currency>.NotFound("Currency");

        return Result<Currency>.Success(currency);
    }
}