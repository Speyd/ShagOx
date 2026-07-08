using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
public class AttributeDefinitionUpdateService : IAttributeDefinitionUpdateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly ICategoryExistsRepository _categoryExistsRepository;



    public AttributeDefinitionUpdateService(
        IAttributeDefinitionRepository attributeRepository,
        ICategoryExistsRepository categoryExistsRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryExistsRepository = categoryExistsRepository;
    }

    public async Task<Result<AttributeDefinitionUpdateResponse>> UpdateAttributeDefinitionAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request)
    {
        var attribute = await _attributeRepository.GetByIdAsync(attributeId);
        if (attribute is null)
            return Result<AttributeDefinitionUpdateResponse>.NotFound("Attribute Definition");

        if (request.CategoryId is not null &&
            !await _categoryExistsRepository.ExistsIdAsync(request.CategoryId.Value))
        {
            return Result<AttributeDefinitionUpdateResponse>.NotFound("Category");
        }

        var updatedCount = ApplyUpdates(attribute, request);
        var result = new AttributeDefinitionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<AttributeDefinitionUpdateResponse>.Success(result);

        await _attributeRepository.UpdateAsync(attribute);

        return Result<AttributeDefinitionUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        AttributeDefinition attribute,
        AttributeDefinitionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.CategoryId is not null)
        {
            attribute.CategoryId = request.CategoryId.Value;
            countUpdated++;
        }

        if (request.Key is not null)
        {
            attribute.Key = request.Key;
            countUpdated++;
        }

        if (request.Type is not null)
        {
            attribute.Type = request.Type.Value;
            countUpdated++;
        }

        if (request.Required is not null)
        {
            attribute.Required = request.Required.Value;
            countUpdated++;
        }

        if (request.Min is not null)
        {
            attribute.Min = request.Min.Value;
            countUpdated++;
        }

        if (request.Max is not null)
        {
            attribute.Max = request.Max.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}