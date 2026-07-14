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


    public async Task<Result<Currency>> GetByIdAsync(
        int currencyId)
    {
        var currency = await _repository.GetByIdAsync(currencyId);
        if (currency is null)
            return Result<Currency>.NotFound("Currency");

        return Result<Currency>.Success(currency);
    }

    public async Task<Result<bool>> ExistsByIdAsync(int id)
    {
        if (!await _existsRepository.ExistsByIdAsync(id))
            return Result<bool>.NotFound("Currency");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(int id)
    {
        if (await _existsRepository.ExistsByIdAsync(id))
            return Result<bool>.NotFound("Currency");

        return Result<bool>.Success(true);
    }
}