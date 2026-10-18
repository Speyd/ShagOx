using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketItems.Admin;

[ApiController]
[Route("api/admin/basket-items")]
//[Authorize(Roles = "Admin")]
public class BasketItemAdminQueriesController
    : BaseQueryController<BasketItemDto,
        BasketItemSearchFilter>
{
    private readonly IBasketItemQueryService _itemQueryService;


    public BasketItemAdminQueriesController(
        IBasketItemQueryService queryService
    ) 
        : base(queryService)
    {
        _itemQueryService = queryService;
    }



    [HttpGet("basket/{id:long}")]
    public async Task<IActionResult> GetByBasket(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _itemQueryService
            .GetByBasketAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("advertisement/{id:long}")]
    public async Task<IActionResult> GetByAdvertisement(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _itemQueryService
            .GetByAdvertisementVariantAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("user/{id:long}")]
    public async Task<IActionResult> GetPaged(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _itemQueryService
            .GetPagedAsync(id, pagination);

        return result.ToActionResult();
    }
}
