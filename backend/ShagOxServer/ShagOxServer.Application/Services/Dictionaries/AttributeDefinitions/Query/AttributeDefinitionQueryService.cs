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
    private readonly IAttributeDefinitionQueryRepository _attributeQueryRepository;

    private readonly ICategoryExistsRepository _categoryExistsRepository;


    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository,
        ICategoryExistsRepository categoryExistsRepository)
    {
        _attributeQueryRepository = attributeQueryRepository;
        _categoryExistsRepository = categoryExistsRepository;
    }


    public async Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id)
    {
        var attribute = await _attributeQueryRepository.GetByIdAsync(id);

        return attribute.ToResult(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int categoryId)
    {
        if (!await _categoryExistsRepository.ExistsByIdAsync(categoryId))
            return Result<List<AttributeDefinitionDto>>.NotFound("Category");

        var attributes = await _attributeQueryRepository.GetByCategoryAsync(categoryId);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<List<AttributeDefinitionDto>>> Search(
       AttributeDefinitionSearchFilter filter,
       PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository.Search(filter, pagination);

        return attributes.ToResultList(AttributeDefinitionMapper.ToDto);
    }
}