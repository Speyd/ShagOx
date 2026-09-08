using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Validator;
public class CurrencyValidator
    : BaseValidator<Currency>
{
    private readonly ICurrencyExistsRepository _existsRepository;


    public CurrencyValidator(
        IRepository<Currency> repository,
        ICurrencyExistsRepository existsRepository
    ) : base(repository, existsRepository)
    {
        _existsRepository = existsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeValidator(
        string code)
    {
        if (!await _existsRepository.ExistsByCodeAsync(code))
            return Result<bool>.AlreadyExists("Currency code");

        return Result<bool>.Success(true);
    }


    public async Task<Result<bool>> NotExistsByCodeValidator(
        string code)
    {
        if (await _existsRepository.ExistsByCodeAsync(code))
            return Result<bool>.AlreadyExists("Currency code");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
        string name)
    {
        if (!await _existsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Currency name");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameValidator(
        string name)
    {
        if (await _existsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Currency name");

        return Result<bool>.Success(true);
    }
}