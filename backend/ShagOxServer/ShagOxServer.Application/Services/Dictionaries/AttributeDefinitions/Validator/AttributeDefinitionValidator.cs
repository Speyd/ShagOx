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
            return Result<AttributeDefinition>.NotFound("AttributeDefinition");

        return Result<AttributeDefinition>.Success(attribute);
    }

    public async Task<Result<bool>> ExistsCategoryValidator(
       int attributeId)
    {
        var category = await _attributeExistsRepository.ExistsByIdAsync(attributeId);
        if (category)
            return Result<bool>.AlreadyExists("Category");

        return Result<bool>.Success(category);
    }
}