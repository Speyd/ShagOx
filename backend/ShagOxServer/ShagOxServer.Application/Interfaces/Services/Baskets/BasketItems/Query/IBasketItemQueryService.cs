using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;

public interface IBasketItemQueryService
    : IQueryService<BasketItemDto>
{
    Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        int basketId,
        PaginationParams pagination);

    Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementAsync(
        int advertisementId,
        PaginationParams pagination);

    Task<Result<PagedResult<BasketItemDto>>> Search(
        BasketItemSearchFilter filter,
        PaginationParams pagination);
}