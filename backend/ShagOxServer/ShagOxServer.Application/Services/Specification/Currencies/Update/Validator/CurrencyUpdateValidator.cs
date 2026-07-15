using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update.Validator;
public class CurrencyUpdateValidator
{
    private readonly ICurrencyExistsRepository _existsRepository;


    public CurrencyUpdateValidator(
        ICurrencyExistsRepository existsRepository)
    {
        _existsRepository = existsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeValidator(
        string? code)
    {
        if(code is null)
            return Result<bool>.Success(false);

        var currency = await _existsRepository.ExistsByCodeAsync(code);
        if (!currency)
            return Result<bool>.AlreadyExists("Currency code");

        return Result<bool>.Success(currency);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
        string? name)
    {
        if (name is null)
            return Result<bool>.Success(false);

        var currency = await _existsRepository.ExistsByNameAsync(name);
        if (!currency)
            return Result<bool>.AlreadyExists("Currency name");

        return Result<bool>.Success(currency);
    }
}