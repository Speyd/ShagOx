using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
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
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        long basketId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementVariantAsync(
        long advertisementVariantId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}
