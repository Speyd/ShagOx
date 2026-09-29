using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
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
    private readonly IBasketItemQueryService _basketItemService;



    public BasketQueryService(
        IBasketQueryRepository basketQueryRepository,
        IBasketItemQueryService basketItemService
    )
        : base(basketQueryRepository)
    {
        _basketQueryRepository = basketQueryRepository;
        _basketItemService = basketItemService;
    }


    public override async Task<BasketDto> ApplyMapperAsync(
        Basket entity)
    {
        var items = new List<BasketItemDto>();
        Console.WriteLine(entity.Id);
        foreach(var item in entity.BasketItems)
        {
            Console.WriteLine(item.Id);
            var itemDto = await _basketItemService
                .ApplyMapperAsync(item);

            items.Add(itemDto);
        }

        return BasketMapper.ToDto(entity, items);
    }

    public async Task<Result<BasketDto>> GetByUserAsync(
        long userId)
    {
        var basket = await _basketQueryRepository
            .GetByUserAsync(userId);

        return await basket.ToResultAsync(ApplyMapperAsync);
    }
}