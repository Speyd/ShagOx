using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketAttributes;

[ApiController]
[Route("api/basket-attributes")]
public class BasketAttributeQueriesController
    : BaseQueryController<BasketAttributeDto,
        BasketAttributeSearchFilter>
{
    private readonly IBasketAttributeQueryService _basketQueryService;


    public BasketAttributeQueriesController(
        IBasketAttributeQueryService queryService
    )
        : base(queryService)
    {
        _basketQueryService = queryService;
    }




    [HttpGet("category/{id:long}")]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _basketQueryService
            .GetByCategoryAsync(id, pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("attribute-definition/{id:long}")]
    public async Task<IActionResult> GetByAttributeDefinition(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _basketQueryService
            .GetByAttributeDefinitionAsync(id,
                cancellationToken);

        return result.ToActionResult();
    }
}
