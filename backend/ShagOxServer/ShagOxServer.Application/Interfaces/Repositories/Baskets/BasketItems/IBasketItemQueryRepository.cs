using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemQueryRepository
    : IQueryRepository<BasketItem>
{
    Task<PagedResult<BasketItem>> GetPagedAsync(
        long userId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> GetByAdvertisementAsync(
        long advertisementId,
        PaginationParams pagination);

    Task<PagedResult<BasketItem>> Search(
        BasketItemSearchFilter filter,
        PaginationParams pagination);
}