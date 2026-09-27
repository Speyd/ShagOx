using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
public class AttributeDefinitionValidator
    : BaseValidator<AttributeDefinition>
{
    private readonly IAttributeDefinitionExistsRepository _attributeExistsRepository;


    public AttributeDefinitionValidator(
        IRepository<AttributeDefinition> attributeRepository,
        IAttributeDefinitionExistsRepository attributeExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByKeyAsync(
      string key,
      long categoryId)
    {
        if (!await _attributeExistsRepository
            .ExistsByCategoryAsync(key, categoryId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDefinition);
        }       

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByKeyAsync(
      string key,
      long categoryId)
    {
        if (await _attributeExistsRepository
            .ExistsByCategoryAsync(key, categoryId))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDefinition);
        }

        return Result<bool>.Success(true);
    }
}