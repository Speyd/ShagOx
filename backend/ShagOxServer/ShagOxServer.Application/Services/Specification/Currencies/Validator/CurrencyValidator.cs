using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.TwiML.Voice;

namespace ShagOxServer.Application.Services.Specification.Currencies.Validator;
public class CurrencyValidator
    : BaseValidator<Currency>
{
    private readonly ICurrencyExistsRepository _existsRepository;
    private readonly ICurrencyQueryRepository _queryRepository;


    public CurrencyValidator(
        IRepository<Currency> repository,
        ICurrencyQueryRepository queryRepository,
        ICurrencyExistsRepository existsRepository,
        IDataCacheService cache
    ) 
        : base(repository, queryRepository, existsRepository, cache)
    {
        _existsRepository = existsRepository;
        _queryRepository = queryRepository;
    }



    public async Task<Result<Currency?>> GetByUserAsync(
       long userId,
       CancellationToken cancellationToken = default,
       EntityStatus findStatus = EntityStatus.ActiveAndHidden)
    {
        var entity = await _queryRepository
            .GetByUserAsync(userId, cancellationToken, findStatus);

        if (entity is null)
        {
            return Result<Currency?>.NotFound(
                EntityNameExtensions.GetLocalizedName<Currency>());
        }

        return Result<Currency?>.Success(entity);
    }

    public async Task<Result<bool>> ExistsByCodeValidator(
        string code,
        CancellationToken cancellationToken = default)
    {
        if (!await _existsRepository
                .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Currency);
        }

        return Result<bool>.Success(true);
    }


    public async Task<Result<bool>> NotExistsByCodeValidator(
        string code,
        CancellationToken cancellationToken = default)
    {
        if (await _existsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Currency);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
        string name,
        CancellationToken cancellationToken = default)
    {
        if (!await _existsRepository
                .ExistsByNameAsync(name, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Currency);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameValidator(
        string name,
        CancellationToken cancellationToken = default)
    {
        if (await _existsRepository
                .ExistsByNameAsync(name, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Currency);
        }

        return Result<bool>.Success(true);
    }
}
