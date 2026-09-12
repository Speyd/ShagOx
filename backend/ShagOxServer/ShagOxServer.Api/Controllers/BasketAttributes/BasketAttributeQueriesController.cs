using Microsoft.AspNetCore.Mvc;
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


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet("category/{id:int}")]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] int id,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetByCategoryAsync(id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("attribute-defenition/{id:int}")]
    public async Task<IActionResult> GetByAttributeDefenition(
        [FromRoute] int id)
    {
        var result = await _queryService
            .GetByAttributeDefenitionAsync(id);

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