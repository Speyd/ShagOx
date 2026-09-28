using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public class AttributeDictionaryValueQueryService
    : BaseTranslatableQueryService<
        AttributeDictionaryValueDto,
        AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter
        >,
    IAttributeDictionaryValueQueryService
{
    protected readonly IAttributeDictionaryValueQueryRepository 
        _dictionaryDictQueryRepository;

    public AttributeDictionaryValueQueryService(
        IAttributeDictionaryValueQueryRepository dictionaryDictQueryRepository
    )
        : base(dictionaryDictQueryRepository)
    {
        _dictionaryDictQueryRepository = dictionaryDictQueryRepository;
    }


    public override async Task<AttributeDictionaryValueDto> ApplyMapperAsync(
        AttributeDictionaryValue entity)
    {
        return AttributeDictionaryValueMapper.ToDto(entity);
    }


    public async Task<Result<PagedResult<AttributeDictionaryValueDto>>> 
        GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination)
    {
        var values = await _dictionaryDictQueryRepository
            .GetByDictionaryAsync(dictionaryId, pagination);

        return await values.ToResultPagedAsync(ApplyMapperAsync);
    }
}