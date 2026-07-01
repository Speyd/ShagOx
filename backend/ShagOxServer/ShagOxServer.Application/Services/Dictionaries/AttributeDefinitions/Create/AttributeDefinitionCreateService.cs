using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
public class AttributeDefinitionCreateService : IAttributeDefinitionCreateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly ICategoryRepository _categoryRepository;


    public AttributeDefinitionCreateService(
        IAttributeDefinitionRepository attributeRepository,
        ICategoryRepository categoryRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request)
    {
        var categoryExists = await _categoryRepository.ExistsIdAsync(request.CategoryId);
        if (!categoryExists)
            return Result<AttributeDefinitionCreateResponse>.NotFound("Category");

        var attribute = CreateAttributeDefinition(request);
        await _attributeRepository.AddAsync(attribute);

        var response = new AttributeDefinitionCreateResponse(
            attribute.Id,
            DateTime.UtcNow
        );

        return Result<AttributeDefinitionCreateResponse>.Success(response);
    }

    private AttributeDefinition CreateAttributeDefinition(
        AttributeDefinitionCreateRequest request)
    {
        return new AttributeDefinition
        {
            CategoryId = request.CategoryId,
            Key = request.Key,
            Type = request.Type,
            Required = request.Required,
            Min = request.Min,
            Max = request.Max,
        };
    }

}
