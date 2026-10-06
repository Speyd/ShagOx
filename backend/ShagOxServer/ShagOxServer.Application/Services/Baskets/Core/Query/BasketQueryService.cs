using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.DTOs.Baskets.Core.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Baskets.Core.Mapping;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.Core.Query;
public class BasketQueryService
    : BaseLocalizedQueryService<
        BasketDto,
        Basket,
        BasketSearchFilter
        >,
    IBasketQueryService
{
    private readonly IBasketQueryRepository _basketQueryRepository;
    private readonly IBasketItemQueryService _basketItemService;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public BasketQueryService(
        IBasketQueryRepository basketQueryRepository,
        IBasketItemQueryService basketItemService,
        ICacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(basketQueryRepository, language, cacheService, settings)
    {
        _basketQueryRepository = basketQueryRepository;
        _basketItemService = basketItemService;
    }


    public override async Task<BasketDto> ApplyMapperAsync(
        Basket entity)
    {
        var items = new List<BasketItemDto>();

        foreach(var item in entity.BasketItems)
        {
            var itemDto = await _basketItemService
                .ApplyMapperAsync(item);

            items.Add(itemDto);
        }

        return BasketMapper.ToDto(entity, items);
    }

    public async Task<Result<BasketDto>> GetByUserAsync(
        long userId)
    {
        var cacheKey = BasketCache.ByUser(
            userId,
            _language.Language);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var basket = await _basketQueryRepository
                    .GetByUserAsync(userId);

                return await basket.ToResultAsync(ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}