using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService : IAttributeDefinitionQueryService
{
    private readonly IAttributeDefinitionRepository _repository;

    public AttributeDefinitionQueryService(
        IAttributeDefinitionRepository attributeRepository)
    {
        _repository = attributeRepository;
    }

    public async Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id)
    {
        var attribute = await _repository.GetByIdAsync(id);
        if (attribute is null)
            Result<AttributeDefinitionDto>.NotFound("Attribute Definition");

        return attribute.ToResult(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int categoryId)
    {
        var attributes = await _repository.GetByCategoryAsync(categoryId);
        if (attributes is null || !attributes.Any())
            return Result<List<AttributeDefinitionDto>>.NotFound("Attribute Definition");

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }
}
