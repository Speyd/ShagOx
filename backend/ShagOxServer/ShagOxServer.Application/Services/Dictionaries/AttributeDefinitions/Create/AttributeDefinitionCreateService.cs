using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
public class AttributeDefinitionCreateService : IAttributeDefinitionCreateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly ICategoryExistsRepository _categoryExistsRepository;

    public AttributeDefinitionCreateService(
        IAttributeDefinitionRepository attributeRepository,
        ICategoryExistsRepository categoryExistsRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryExistsRepository = categoryExistsRepository;
    }

    public async Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request)
    {
        var categoryExists = await _categoryExistsRepository.ExistsIdAsync(request.CategoryId);
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
