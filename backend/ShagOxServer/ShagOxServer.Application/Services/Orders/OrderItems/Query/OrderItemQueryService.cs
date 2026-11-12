using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Items;
using ShagOxServer.Application.Services.LineItems.Core.Query;
using ShagOxServer.Application.Services.Orders.OrderItems.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Query;
public class OrderItemQueryService
    : LineItemQueryService<
        OrderItemDto,
        OrderItem,
        OrderItemSearchFilter
        >,
    IOrderItemQueryService
{

    //private readonly IBasketQueryRepository _basketQueryRepository;

    private readonly IAdvertisementQueryService _advertisementService;
    private readonly IStatusQueryService _statusService;



    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public OrderItemQueryService(
        IOrderItemQueryRepository itemQueryRepository,
        //IBasketQueryRepository basketQueryRepository,
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
        //_basketQueryRepository = basketQueryRepository;
        _statusService = statusService;
        _advertisementService = advertisementService;
    }


    public override async Task<OrderItemDto> ApplyMapperAsync(
        OrderItem entity)
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

        return OrderItemMapper.ToDto(entity, advertDto);
    }

    public override async Task<Result<PagedResult<OrderItemDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        //var basketId = await _basketQueryRepository
        //    .GetIdByUserAsync(userId,
        //        cancellationToken, status);

        //if (!basketId.HasValue)
        //{
        //    return Result<PagedResult<LineItemDto>>
        //        .NotFound(EntityNamesResources.Basket);
        //}


        return await GetByParentAsync(
            0,
            //basketId.Value,
            pagination,
            cancellationToken,
            status
        );
    }

}
