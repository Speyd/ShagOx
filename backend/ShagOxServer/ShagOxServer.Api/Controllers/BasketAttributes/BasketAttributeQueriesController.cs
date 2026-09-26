using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.BasketAttributes;

[ApiController]
[Route("api/basket-attributes")]
public class BasketAttributeQueriesController
    : ApiController
{
    private readonly IBasketAttributeQueryService _queryService;


    public BasketAttributeQueriesController(
        IBasketAttributeQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet("category/{id:long}")]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetByCategoryAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("attribute-defenition/{id:long}")]
    public async Task<IActionResult> GetByAttributeDefenition(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByAttributeDefenitionAsync(id);

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
       [FromQuery] BasketAttributeSearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}