using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
public class AttributeDefinitionValidator
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly IAttributeDefinitionExistsRepository _attributeExistsRepository;


    public AttributeDefinitionValidator(
        IAttributeDefinitionRepository attributeRepository,
        IAttributeDefinitionExistsRepository attributeExistsRepository)
    {
        _attributeRepository = attributeRepository;
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<AttributeDefinition>> GetAttributeValidator(
        int attributeId)
    {
        var attribute = await _attributeRepository.GetByIdAsync(attributeId);
        if (attribute is null)
            return Result<AttributeDefinition>.NotFound("Attribute Definition");

        return Result<AttributeDefinition>.Success(attribute);
    }

    public async Task<Result<bool>> ExistsAttributeValidator(
       int attributeId)
    {
        var attribute = await _attributeExistsRepository.ExistsByIdAsync(attributeId);
        if (attribute)
            return Result<bool>.AlreadyExists("Attribute Definition");

        return Result<bool>.Success(attribute);
    }

    public async Task<Result<bool>> ExistsByKeyValidator(
      string attributeName,
      int categoryId)
    {
        var attribute = await _attributeExistsRepository.ExistsByCategoryAsync(attributeName, categoryId);
        if (attribute)
            return Result<bool>.AlreadyExists("Attribute Definition");

        return Result<bool>.Success(attribute);
    }
}