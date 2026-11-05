using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
public class AttributeDefinitionValidator
    : BaseValidator<AttributeDefinition>
{
    private readonly IAttributeDefinitionExistsRepository _attributeExistsRepository;


    public AttributeDefinitionValidator(
        IRepository<AttributeDefinition> repository,
        IQueryRepository<AttributeDefinition> attributeRepository,
        IAttributeDefinitionExistsRepository attributeExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, attributeRepository, attributeExistsRepository, cache)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByKeyAsync(
        string key,
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        if (!await _attributeExistsRepository
            .ExistsByCategoryAsync(key, categoryId, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDefinition);
        }       

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByKeyAsync(
         string key,
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        if (await _attributeExistsRepository
            .ExistsByCategoryAsync(key, categoryId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDefinition);
        }

        return Result<bool>.Success(true);
    }
}
