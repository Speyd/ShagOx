using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Query;
public class BasketItemQueryService
    : BaseQueryService<
        BasketItemDto,
        BasketItem,
        BasketItemSearchFilter
        >,
    IBasketItemQueryService
{
    private readonly IBasketItemQueryRepository _itemQueryRepository;
    private readonly IAdvertisementVariantQueryService _variantService;



    public BasketItemQueryService(
        IBasketItemQueryRepository itemQueryRepository,
        IAdvertisementVariantQueryService variantService
    )
        : base(itemQueryRepository)
    {
        _itemQueryRepository = itemQueryRepository;
        _variantService = variantService;
    }


    public override async Task<BasketItemDto> ApplyMapperAsync(
        BasketItem entity)
    {
        var variant = await _variantService
            .ApplyMapperAsync(entity.AdvertisementVariant);

        return BasketItemMapper.ToDto(entity, variant);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementVariantAsync(
        long advertisementVariantId,
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
           .GetByAdvertisementVariantAsync(advertisementVariantId, pagination);

        return await items.ToResultPagedAsync(ApplyMapperAsync);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        long basketId, 
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
           .GetByBasketAsync(basketId, pagination);

        return await items.ToResultPagedAsync(ApplyMapperAsync);
    }


    public async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
            .GetPagedAsync(userId, pagination);

        return await items.ToResultPagedAsync(ApplyMapperAsync);
    }
    
}