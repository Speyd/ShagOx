using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using Twilio.Rest.Verify.V2.Service;

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



    public BasketItemQueryService(
        IBasketItemQueryRepository itemQueryRepository,
        IBasketQueryRepository basketQueryRepository,
        IAdvertisementQueryService advertisementService,
        ICacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(itemQueryRepository, language, cacheService, settings)
    {
        _itemQueryRepository = itemQueryRepository;
        _basketQueryRepository = basketQueryRepository;
        _advertisementService = advertisementService;
    }


    public override async Task<BasketItemDto> ApplyMapperAsync(
        BasketItem entity)
    {
        var adert = await _advertisementService
            .ApplyMapperAsync(entity.AdvertisementVariant.Advertisement);

        return BasketItemMapper.ToDto(entity, adert);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementVariantAsync(
        long variantId,
        PaginationParams pagination)
    {
        var cacheKey = BasketItemCache.ByAdvertisementVariant(
            variantId,
            _language.Language,
            pagination.Page,
            pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetByAdvertisementVariantAsync(variantId, pagination);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        long basketId, 
        PaginationParams pagination)
    {
        var cacheKey = BasketItemCache.ByBasket(
           basketId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetByBasketAsync(basketId, pagination);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );    
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination)
    {
        var basketId = await _basketQueryRepository
            .GetIdByUserAsync(userId);
        if (!basketId.HasValue)
        {
            return Result<PagedResult<BasketItemDto>>
                .NotFound(EntityNamesResources.Basket);
        }

        var cacheKey = BasketItemCache.ByBasket(
           basketId.Value,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var items = await _itemQueryRepository
                    .GetPagedAsync(userId, pagination);

                return await items.ToResultPagedAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}