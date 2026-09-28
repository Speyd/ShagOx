using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Mappers;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
public class AttributeDictionaryQueryService
    : BaseQueryService<
        AttributeDictionaryDto,
        AttributeDictionary,
        AttributeDictionarySearchFilter
        >,
    IAttributeDictionaryQueryService
{
    public AttributeDictionaryQueryService(
        IAttributeDictionaryQueryRepository dictionaryDictQueryRepository
    )
        : base(dictionaryDictQueryRepository)
    {
    }


    protected override async Task<AttributeDictionaryDto> ApplyMapperAsync(
        AttributeDictionary entity)
    {
        return AttributeDictionaryMapper.ToDto(entity);
    }
}