using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Application.Services.Baskets.Core.Mapping;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.Core.Query;
public class BasketQueryService
    : IBasketQueryService
{
    private readonly IBasketQueryRepository _basketQueryRepository;


    public BasketQueryService(
        IBasketQueryRepository basketQueryRepository)
    {
        _basketQueryRepository = basketQueryRepository;
    }


    public async Task<Result<BasketDto>> GetByIdAsync(
        int id)
    {
        var basket = await _basketQueryRepository
            .GetByIdAsync(id);

        return basket.ToResult(BasketMapper.ToDto);
    }

    public async Task<Result<BasketDto>> GetByUserAsync(
        int userId)
    {
        var basket = await _basketQueryRepository
            .GetByUserAsync(userId);

        return basket.ToResult(BasketMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var baskets = await _basketQueryRepository
            .GetPagedAsync(pagination);

        return baskets.ToResultPaged(BasketMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketDto>>> Search(
       BasketSearchFilter filter,
       PaginationParams pagination)
    {
        var baskets = await _basketQueryRepository
            .Search(filter, pagination);

        return baskets.ToResultPaged(BasketMapper.ToDto);
    }
}