using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Common;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.BasketItems.Admin;

[ApiController]
[Route("api/admin/basket-items")]
[Authorize(Roles = "Admin")]
public class BasketItemAdminQueriesController
    : OwnerExistsController
{
    private readonly IBasketItemQueryService _queryService;


    public BasketItemAdminQueriesController(
        IBasketItemQueryService queryService,
        IBasketExistsRepository basketExistsService,
        IUserAdminQueryService userQueryService
    ) : base(basketExistsService, userQueryService)
    {
        _queryService = queryService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet("basket/{id:int}")]
    public async Task<IActionResult> GetByBasket(
        [FromRoute] int id,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetByBasketAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("advertisement/{id:int}")]
    public async Task<IActionResult> GetByAdvertisement(
        [FromRoute] int id,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetByAdvertisementAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("user/{id:int}")]
    public async Task<IActionResult> GetPaged(
        [FromRoute] int id,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
       [FromQuery] BasketItemSearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}