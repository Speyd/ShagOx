using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Caches;
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
        IAttributeDictionaryQueryRepository dictionaryDictQueryRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(dictionaryDictQueryRepository, cacheService, settings)
    {
    }


    public override async Task<AttributeDictionaryDto> ApplyMapperAsync(
        AttributeDictionary entity)
    {
        return AttributeDictionaryMapper.ToDto(entity);
    }
}