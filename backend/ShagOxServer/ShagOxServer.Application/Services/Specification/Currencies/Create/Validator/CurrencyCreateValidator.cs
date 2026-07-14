using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
public class CurrencyCreateValidator
{
    private readonly ICurrencyExistsRepository _existsRepository;


    public CurrencyCreateValidator(
        ICurrencyExistsRepository existsRepository)
    {
        _existsRepository = existsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeValidator(
        string code)
    {
        var currency = await _existsRepository.ExistsByCodeAsync(code);
        if (!currency)
            return Result<bool>.AlreadyExists("Currency code");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
        string name)
    {
        var currency = await _existsRepository.ExistsByNameAsync(name);
        if (!currency)
            return Result<bool>.AlreadyExists("Currency name");

        return Result<bool>.Success(true);
    }
}