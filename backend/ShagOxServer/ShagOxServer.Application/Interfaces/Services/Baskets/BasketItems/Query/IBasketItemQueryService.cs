using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;

public interface IBasketItemQueryService
    : IQueryService<BasketItemDto,
        BasketItem, 
        BasketItemSearchFilter>
{
    Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination);

    Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination);

    Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementAsync(
        long advertisementId,
        PaginationParams pagination);
}