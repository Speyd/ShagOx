using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.LineItems.LineItemAttributes.Admin;
[ApiController]
[Route("api/admin/line-item-attributes")]
public class LineItemAttributeAdminQueriesController
    : BaseAdminQueryController<LineItemAttributeDto,
        LineItemAttributeSearchFilter>
{
    private readonly ILineItemAttributeQueryService _queryService;


    public LineItemAttributeAdminQueriesController(
        ILineItemAttributeQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _queryService = queryService;
    }


    [HttpGet("category/{id:long}")]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] long id,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
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
        var result = await _queryService
            .GetByAttributeDefinitionAsync(id,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
