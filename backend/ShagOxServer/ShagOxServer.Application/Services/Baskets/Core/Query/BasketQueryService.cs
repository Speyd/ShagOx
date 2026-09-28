using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Baskets.Core.Mapping;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.Core.Query;
public class BasketQueryService
    : BaseQueryService<
        BasketDto,
        Basket,
        BasketSearchFilter
        >,
    IBasketQueryService
{
    private readonly IBasketQueryRepository _basketQueryRepository;


    public BasketQueryService(
        IBasketQueryRepository basketQueryRepository
    )
        : base(basketQueryRepository)
    {
        _basketQueryRepository = basketQueryRepository;
    }


    protected override async Task<BasketDto> ApplyMapperAsync(
        Basket entity)
    {
        return BasketMapper.ToDto(entity);
    }

    public async Task<Result<BasketDto>> GetByUserAsync(
        long userId)
    {
        var basket = await _basketQueryRepository
            .GetByUserAsync(userId);

        return await basket.ToResultAsync(ApplyMapperAsync);
    }
}