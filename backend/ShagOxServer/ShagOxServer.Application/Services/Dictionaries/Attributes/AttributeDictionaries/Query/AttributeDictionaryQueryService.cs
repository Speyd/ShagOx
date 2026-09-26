using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Mappers;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
public class AttributeDictionaryQueryService
    : IAttributeDictionaryQueryService
{
    private readonly IAttributeDictionaryQueryRepository _attributeDictQueryRepository;


    public AttributeDictionaryQueryService(
        IAttributeDictionaryQueryRepository attributeDictQueryRepository)
    {
        _attributeDictQueryRepository = attributeDictQueryRepository;
    }


    public async Task<Result<AttributeDictionaryDto>> GetByIdAsync(
        long id)
    {
        var attribute = await _attributeDictQueryRepository
            .GetByIdAsync(id);

        return attribute.ToResult(AttributeDictionaryMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDictionaryDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var attributes = await _attributeDictQueryRepository
            .GetPagedAsync(pagination);

        return attributes.ToResultPaged(AttributeDictionaryMapper.ToDto);
    }

    public async Task<Result<PagedResult<AttributeDictionaryDto>>> Search(
       AttributeDictionarySearchFilter filter,
       PaginationParams pagination)
    {
        var attributes = await _attributeDictQueryRepository
            .Search(filter, pagination);

        return attributes.ToResultPaged(AttributeDictionaryMapper.ToDto);
    }
}