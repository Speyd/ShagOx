using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.LineItems.LineItemAttributes;

[ApiController]
[Route("api/line-item-attributes")]
public class LineItemAttributeQueriesController
    : BaseQueryController<LineItemAttributeDto,
        LineItemAttributeSearchFilter>
{
    private readonly ILineItemAttributeQueryService _basketQueryService;


    public LineItemAttributeQueriesController(
        ILineItemAttributeQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
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
