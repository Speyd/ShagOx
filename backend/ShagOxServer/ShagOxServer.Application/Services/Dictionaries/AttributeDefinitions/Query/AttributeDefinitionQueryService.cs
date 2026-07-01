using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService : IAttributeDefinitionQueryService
{
    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeRepository,
        ICategoryRepository categoryRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id)
    {
        var attribute = await _attributeRepository.GetByIdAsync(id);

        return attribute.ToResult(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int categoryId)
    {
        var categoryExists = await _categoryRepository.ExistsIdAsync(categoryId);
        if (!categoryExists)
            return Result<List<AttributeDefinitionDto>>.NotFound("Category");

        var attributes = await _attributeRepository.GetByCategoryAsync(categoryId);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> SearchByKey(
       string key,
       int page,
       int pageSize)
    {
        var attributes = await _attributeRepository.SearchByKey(key, page, pageSize);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }
}
