using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
using ShagOxServer.Application.Services.Caches.Baskets;
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


    public BasketAttributeQueryService(
        IBasketAttributeQueryRepository attributeQueryRepository,
        IAttributeDefinitionQueryService attributeService,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(attributeQueryRepository, cacheService, settings)
    {
        _attributeQueryRepository = attributeQueryRepository;
        _attributeService = attributeService;
    }


    public override async Task<BasketAttributeDto> ApplyMapperAsync(
        BasketAttribute entity)
    {
        var attributerDto = await _attributeService
            .ApplyMapperAsync(entity.AttributeDefinition);

        return BasketAttributeMapper.ToDto(entity, attributerDto);
    }

    public async Task<Result<BasketAttributeDto>> GetByAttributeDefenitionAsync(
        long attributeDefenitionId)
    {
        var cacheKey = BasketAttributeCache.ByAttributeDefenition(
           attributeDefenitionId);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attribute = await _attributeQueryRepository
                    .GetByAttributeDefenitionAsync(attributeDefenitionId);

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
        var cacheKey = BasketAttributeCache.ByCategoryId(
           categoryId);

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