using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Xml.Linq;

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

    protected readonly IAttributeDictionaryValueTranslationQueryRepository _translation;


    public AttributeDictionaryValueQueryService(
        IAttributeDictionaryValueQueryRepository dictionaryDictQueryRepository,
        IAttributeDictionaryValueTranslationQueryRepository translation,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(dictionaryDictQueryRepository, 
            language, 
            cacheService,
            cacheOptions)
    {
        _dictionaryDictQueryRepository = dictionaryDictQueryRepository;
        _translation = translation;
    }


    public override async Task<AttributeDictionaryValueDto> ApplyMapperAsync(
        AttributeDictionaryValue entity)
    {
        var translation = await _translation
            .GetByIdentificatorAsync(entity.Id, _language.Language);

        return AttributeDictionaryValueMapper.ToDto(entity, translation?.Name);
    }


    public async Task<Result<PagedResult<AttributeDictionaryValueDto>>> 
        GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = AttributeDictionaryValueCache.ByDictionary(
           dictionaryId,
           _language.Language,
           pagination.Page,
           pagination.PageSize,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var values = await _dictionaryDictQueryRepository
                    .GetByDictionaryAsync(dictionaryId, pagination,
                        cancellationToken, status);

                return await values.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.KeyExpiration
        );
    }
}
