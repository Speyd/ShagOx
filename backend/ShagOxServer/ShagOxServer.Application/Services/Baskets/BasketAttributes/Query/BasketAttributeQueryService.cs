using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
using ShagOxServer.Application.Services.Caches.Baskets;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Query;
public class BasketAttributeQueryService
    : BaseQueryService<
        BasketAttributeDto,
        BasketAttribute,
        BasketAttributeSearchFilter
        >,
    IBasketAttributeQueryService
{
    private readonly IBasketAttributeQueryRepository _attributeQueryRepository;

    private readonly IAttributeDefinitionQueryService _attributeService;

    private readonly ILanguageProvider _language;


    public BasketAttributeQueryService(
        IBasketAttributeQueryRepository attributeQueryRepository,
        IAttributeDefinitionQueryService attributeService,
        ICacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(attributeQueryRepository, cacheService, settings)
    {
        _attributeQueryRepository = attributeQueryRepository;
        _attributeService = attributeService;
        _language = language;
    }


    public override async Task<BasketAttributeDto> ApplyMapperAsync(
        BasketAttribute entity)
    {
        var attributerDto = await _attributeService
            .ApplyMapperAsync(entity.AttributeDefinition);

        return BasketAttributeMapper.ToDto(entity, attributerDto);
    }

    public override string GetCacheKey(
        long id)
    {
        return CacheKeys.EntityLanguage<BasketAttribute>(
            id,
            _language.Language);
    }

    public override async Task CreateCache(
        BasketAttributeDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.EntityLanguage<BasketAttribute>(
                dto.Id, _language.Language),
            dto,
            _settings.KeyExpiration);
    }

    public async Task<Result<BasketAttributeDto>> GetByAttributeDefinitionAsync(
        long attributeDefenitionId)
    {
        var cacheKey = BasketAttributeCache.ByAttributeDefenition(
            attributeDefenitionId, 
            _language.Language);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attribute = await _attributeQueryRepository
                    .GetByAttributeDefinitionAsync(attributeDefenitionId);

                return await attribute.ToResultAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<BasketAttributeDto>>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination)
    {
        var cacheKey = BasketAttributeCache.ByCategory(
            categoryId,
            _language.Language,
            pagination.Page,
            pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attributes = await _attributeQueryRepository
                    .GetByCategoryAsync(categoryId, pagination);

                return await attributes.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}