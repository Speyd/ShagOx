using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Query;
public class BasketItemQueryService
    : BaseLocalizedQueryService<
        BasketItemDto,
        BasketItem,
        BasketItemSearchFilter
        >,
    IBasketItemQueryService
{
    private readonly IBasketItemQueryRepository _itemQueryRepository;

    private readonly IBasketQueryRepository _basketQueryRepository;

    private readonly IAdvertisementQueryService _advertisementService;
    private readonly IStatusQueryService _statusService;
    private readonly IBasketAttributeExistsRepository _attributeRepository;



    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;



    public BasketItemQueryService(
        IBasketItemQueryRepository itemQueryRepository,
        IBasketQueryRepository basketQueryRepository,
        IAdvertisementQueryService advertisementService,
        IStatusQueryService statusService,
        IBasketAttributeExistsRepository attributeRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(itemQueryRepository, language, cacheService, settings)
    {
        _itemQueryRepository = itemQueryRepository;
        _basketQueryRepository = basketQueryRepository;
        _statusService = statusService;
        _advertisementService = advertisementService;
        _attributeRepository = attributeRepository;
    }


    public override async Task<BasketItemDto> ApplyMapperAsync(
        BasketItem entity)
    {
        await PrepareAttributesForMappingAsync(
            entity.AdvertisementVariant);


        var advert = entity.AdvertisementVariant.Advertisement;
        var converts = await _advertisementService
            .ConvertPricesAsync(advert);

        var statusDto = await _statusService
            .ApplyMapperAsync(advert.Status);

        var advertDto = AdvertisementShortMapper
            .ToDto(advert, converts, statusDto);

        return BasketItemMapper.ToDto(entity, advertDto);
    }

    public async Task PrepareAttributesForMappingAsync(
        AdvertisementVariant advert)
    {
        var variantKeys = await _attributeRepository.ExistsByKeysAsync(
            advert.Attributes,
            advert.Advertisement.CategoryId);

        var advertisementKeys = await _attributeRepository.ExistsByKeysAsync(
            advert.Advertisement.Attributes,
            advert.Advertisement.CategoryId);

        advert.Attributes = FilterAttributes(advert.Attributes, variantKeys);
        advert.Advertisement.Attributes = FilterAttributes(
            advert.Advertisement.Attributes,
            advertisementKeys);
    }

    private static JsonDocument FilterAttributes(
        JsonDocument attributes,
        List<string> existingKeys)
    {
        var filtered = attributes.RootElement
            .EnumerateObject()
            .Where(x => existingKeys.Contains(x.Name))
            .ToDictionary(x => x.Name, x => x.Value.Clone());

        return JsonDocument.Parse(
            JsonSerializer.Serialize(filtered));
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementVariantAsync(
        long variantId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = BasketItemCache.ByAdvertisementVariant(
            variantId,
            _language.Language,
            pagination.Page,
            pagination.PageSize,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetByAdvertisementVariantAsync(variantId, pagination,
                        cancellationToken, status);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        long basketId, 
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = BasketItemCache.ByBasket(
           basketId,
           _language.Language,
           pagination.Page,
           pagination.PageSize,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetByBasketAsync(basketId, pagination,
                        cancellationToken, status);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );    
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var basketId = await _basketQueryRepository
            .GetIdByUserAsync(userId, 
                cancellationToken, status);

        if (!basketId.HasValue)
        {
            return Result<PagedResult<BasketItemDto>>
                .NotFound(EntityNamesResources.Basket);
        }

        var cacheKey = BasketItemCache.ByBasket(
           basketId.Value,
           _language.Language,
           pagination.Page,
           pagination.PageSize,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetPagedAsync(userId, pagination,
                        cancellationToken, status);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
    
}
