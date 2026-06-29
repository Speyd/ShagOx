using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
public class AttributeDefinitionUpdateService : IAttributeDefinitionUpdateService
{
    private readonly IAttributeDefinitionRepository _repository;

    public AttributeDefinitionUpdateService(
        IAttributeDefinitionRepository attributeRepository)
    {
        _repository = attributeRepository;
    }

    public async Task<Result<AttributeDefinitionUpdateResponse>> UpdateAttributeDefinitionAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request)
    {
        var attribute = await _repository.GetByIdAsync(attributeId);
        if (attribute is null)
            return Result<AttributeDefinitionUpdateResponse>.NotFound("Attribute Definition");

        var updatedCount = ApplyUpdates(attribute, request);

        if (updatedCount == 0)
            return Result<AttributeDefinitionUpdateResponse>.Fail(
                "No fields to update");

        await _repository.UpdateAsync(attribute);

        return Result<AttributeDefinitionUpdateResponse>.Success(
            new AttributeDefinitionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            )
        );
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