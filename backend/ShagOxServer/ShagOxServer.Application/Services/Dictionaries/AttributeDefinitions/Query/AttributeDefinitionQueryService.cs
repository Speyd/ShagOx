using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
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
    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository)
    {
        _attributeQueryRepository = attributeQueryRepository;
    }


    public async Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id)
    {
        var attribute = await _attributeQueryRepository.GetByIdAsync(id);

        return attribute.ToResult(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDefinitionDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .GetPagedAsync(pagination);

        return attributes.ToResultPaged(AttributeDefinitionMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDefinitionDto>>> Search(
       AttributeDefinitionSearchFilter filter,
       PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .Search(filter, pagination);

        return attributes.ToResultPaged(AttributeDefinitionMapper.ToDto);
    }
}