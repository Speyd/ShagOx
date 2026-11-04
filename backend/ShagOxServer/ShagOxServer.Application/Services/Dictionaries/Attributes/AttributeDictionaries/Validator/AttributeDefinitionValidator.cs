using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
public class AttributeDictionaryValidator
    : BaseValidator<AttributeDictionary>
{
    private readonly IAttributeDictionaryExistsRepository _dictionaryExistsRepository;


    public AttributeDictionaryValidator(
        IRepository<AttributeDictionary> repository,
        IQueryRepository<AttributeDictionary> dictionaryRepository,
        IAttributeDictionaryExistsRepository dictionaryExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, dictionaryRepository, dictionaryExistsRepository, cache)
    {
        _dictionaryExistsRepository = dictionaryExistsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeAsync(
      string code)
    {
        if (!await _dictionaryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDictionary);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
      string code)
    {
        if (await _dictionaryExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDictionary);
        }

        return Result<bool>.Success(true);
    }
}
