using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Validator;
public class CurrencyValidator
{
    private readonly ICurrencyRepository _repository;
    private readonly ICurrencyExistsRepository _existsRepository;


    public CurrencyValidator(
        ICurrencyRepository repository,
        ICurrencyExistsRepository existsRepository)
    {
        _repository = repository;
        _existsRepository = existsRepository;
    }


    public async Task<Result<Currency>> GetCurrencyValidator(
        int currencyId)
    {
        var currency = await _repository.GetByIdAsync(currencyId);
        if (currency is null)
            return Result<Currency>.NotFound("Currency");

        return Result<Currency>.Success(currency);
    }

    public async Task<Result<bool>> ExistsCurrencyValidator(
        int currencyId)
    {
        var currency = await _existsRepository.ExistsByIdAsync(currencyId);
        if (currency)
            return Result<bool>.AlreadyExists("Currency");

        return Result<bool>.Success(currency);
    }
}