using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Validator;
public class ConditionValidator
    : BaseValidator<Condition>
{
    private readonly IConditionExistsRepository _conditionExistsRepository;


    public ConditionValidator(
        IRepository<Condition> repository,
        IQueryRepository<Condition> conditionRepository,
        IConditionExistsRepository conditionExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, conditionRepository, conditionExistsRepository, cache)
    {
        _conditionExistsRepository = conditionExistsRepository;
    }

    public async Task<Result<bool>> ExistsByNameAsync(
        string code,
        CancellationToken cancellationToken = default)
    {

        if (!await _conditionExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Condition);
        }

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        if (await _conditionExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Condition);
        }

        return Result<bool>.Success(false);
    }
}
