using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
public class AttributeDictionaryValueValidator
    : BaseValidator<AttributeDictionaryValue>
{
    private readonly IAttributeDictionaryValueExistsRepository 
        _valueExistsRepository;


    public AttributeDictionaryValueValidator(
        IRepository<AttributeDictionaryValue> valueRepository,
        IAttributeDictionaryValueExistsRepository valueExistsRepository
    ) : base(valueRepository, valueExistsRepository)
    {
        _valueExistsRepository = valueExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
        long dictionaryId,
        string code)
    {
        if (!await _valueExistsRepository
            .ExistsAsync(dictionaryId, code))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long dictionaryId,
        string code)
    {
        if (await _valueExistsRepository
            .ExistsAsync(dictionaryId, code))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDictionaryValue);
        }

        return Result<bool>.Success(true);
    }


    public async Task<Result<bool>> ExistsByCodeAsync(
        string code)
    {
        if (!await _valueExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDictionaryValue);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code)
    {
        if (await _valueExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDictionaryValue);
        }

        return Result<bool>.Success(true);
    }
}