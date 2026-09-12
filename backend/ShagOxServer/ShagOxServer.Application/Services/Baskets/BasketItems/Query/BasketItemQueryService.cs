using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Query;
public class BasketItemQueryService
    : IBasketItemQueryService
{
    private readonly IBasketItemQueryRepository _itemQueryRepository;


    public BasketItemQueryService(
        IBasketItemQueryRepository itemQueryRepository)
    {
        _itemQueryRepository = itemQueryRepository;
    }


    public async Task<Result<BasketItemDto>> GetByIdAsync(
        int id)
    {
        var item = await _itemQueryRepository
            .GetByIdAsync(id);

        return item.ToResult(BasketItemMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByAdvertisementAsync(
        int advertisementId,
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
           .GetByAdvertisementAsync(advertisementId, pagination);

        return items.ToResultPaged(BasketItemMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetByBasketAsync(
        int basketId, 
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
           .GetByBasketAsync(basketId, pagination);

        return items.ToResultPaged(BasketItemMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
            .GetPagedAsync(pagination);

        return items.ToResultPaged(BasketItemMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> GetPagedAsync(
        int userId,
        PaginationParams pagination)
    {
        var items = await _itemQueryRepository
            .GetPagedAsync(userId, pagination);

        return items.ToResultPaged(BasketItemMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketItemDto>>> Search(
       BasketItemSearchFilter filter,
       PaginationParams pagination)
    {
        var items = await _itemQueryRepository
            .Search(filter, pagination);

        return items.ToResultPaged(BasketItemMapper.ToDto);
    }
}