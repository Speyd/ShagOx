using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemQueryRepository
    : IQueryRepository<BasketItem, BasketItemSearchFilter>
{
    Task<PagedResult<BasketItem>> GetPagedAsync(
        long userId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination);

    Task<List<BasketItemCacheInfo>> GetInfosByBasketAsync(
        long basketId);

    Task<PagedResult<BasketItem>> GetByAdvertisementVariantAsync(
        long advertisementVariantId,
        PaginationParams pagination);

    Task<List<BasketItemCacheInfo>> GetInfosByAdvertisementVariantAsync(
        long advertisementVariantId);
}