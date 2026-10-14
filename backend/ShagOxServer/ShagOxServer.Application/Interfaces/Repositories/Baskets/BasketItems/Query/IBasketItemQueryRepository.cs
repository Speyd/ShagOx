using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
public partial interface IBasketItemQueryRepository
    : ISearchRepository<BasketItem, BasketItemSearchFilter>
{
    Task<PagedResult<BasketItem>> GetPagedAsync(
        long userId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> GetByAdvertisementVariantAsync(
        long advertisementVariantId,
        PaginationParams pagination);
}
