using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.DTOs.LineItems.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketItems.Admin;

[ApiController]
[Route("api/admin/basket-items")]
[Authorize(Roles = "Admin")]
public class BasketItemAdminQueriesController
    : BaseAdminQueryController<BasketItemDto,
        BasketItemSearchFilter>
{
    private readonly IBasketItemQueryService _itemQueryService;


    public BasketItemAdminQueriesController(
        IBasketItemQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _itemQueryService = queryService;
    }



    [HttpGet("basket/{id:long}")]
    public async Task<IActionResult> GetByBasket(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _itemQueryService
            .GetByParentAsync(id, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("user/{id:long}")]
    public async Task<IActionResult> GetPaged(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _itemQueryService
            .GetPagedAsync(id, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
