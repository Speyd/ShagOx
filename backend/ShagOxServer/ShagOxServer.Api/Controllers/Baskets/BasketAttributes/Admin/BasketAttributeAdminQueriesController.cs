using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketAttributes.Admin;
[ApiController]
[Route("api/admin/basket-attributes")]
public class BasketAttributeAdminQueriesController
    : BaseAdminQueryController<BasketAttributeDto,
        BasketAttributeSearchFilter>
{
    private readonly IBasketAttributeQueryService _basketQueryService;


    public BasketAttributeAdminQueriesController(
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
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _basketQueryService
            .GetByCategoryAsync(id, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("attribute-definition/{id:long}")]
    public async Task<IActionResult> GetByAttributeDefinition(
        [FromRoute] long id,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _basketQueryService
            .GetByAttributeDefinitionAsync(id,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
