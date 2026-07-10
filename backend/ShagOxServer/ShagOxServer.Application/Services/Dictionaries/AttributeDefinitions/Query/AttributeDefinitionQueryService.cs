using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService : IAttributeDefinitionQueryService
{
    private readonly IAttributeDefinitionQueryRepository _attributeRepository;

    private readonly ICategoryExistsRepository _categoryExistsRepository;


    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeRepository,
        ICategoryExistsRepository categoryExistsRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryExistsRepository = categoryExistsRepository;
    }

    public async Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id)
    {
        var attribute = await _attributeRepository.GetByIdAsync(id);

        return attribute.ToResult(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int categoryId)
    {
        var categoryExists = await _categoryExistsRepository.ExistsIdAsync(categoryId);
        if (!categoryExists)
            return Result<List<AttributeDefinitionDto>>.NotFound("Category");

        var attributes = await _attributeRepository.GetByCategoryAsync(categoryId);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> Search(
       AttributeDefinitionSearchFilter filter,
       PaginationParams pagination)
    {
        var attributes = await _attributeRepository.Search(filter, pagination);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }
}