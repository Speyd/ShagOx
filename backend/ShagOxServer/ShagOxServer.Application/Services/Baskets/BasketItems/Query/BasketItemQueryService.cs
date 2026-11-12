using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Items;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.LineItems.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Query;
public class BasketItemQueryService
    : LineItemQueryService<
        BasketItemDto,
        BasketItem,
        BasketItemSearchFilter
        >,
    IBasketItemQueryService
{
    private readonly IBasketQueryRepository _basketQueryRepository;

    private readonly IAdvertisementQueryService _advertisementService;
    private readonly IStatusQueryService _statusService;



    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public BasketItemQueryService(
        IBasketItemQueryRepository itemQueryRepository,
        IBasketQueryRepository basketQueryRepository,
        IAdvertisementQueryService advertisementService,
        IStatusQueryService statusService,
        ILineItemAttributeExistsRepository attributeRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(itemQueryRepository, 
            attributeRepository,
            cacheService,
            settings,
            language)
    {
        _basketQueryRepository = basketQueryRepository;
        _statusService = statusService;
        _advertisementService = advertisementService;
    }


    public override async Task<BasketItemDto> ApplyMapperAsync(
        BasketItem entity)
    {
        await PrepareAttributesForMappingAsync(
            entity.Advertisement,
            entity.AdvertisementVariant);


        var converts = await _advertisementService
            .ConvertPricesAsync(entity.Advertisement);

        var statusDto = await _statusService
            .ApplyMapperAsync(entity.Advertisement.Status);

        var advertDto = AdvertisementItemMapper
            .ToDto(entity.Advertisement, converts, statusDto);

        return BasketItemMapper.ToDto(entity, advertDto);
    }

    public override async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
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


        return await GetByParentAsync(
            basketId.Value, 
            pagination, 
            cancellationToken, 
            status
        );
    }  
}
