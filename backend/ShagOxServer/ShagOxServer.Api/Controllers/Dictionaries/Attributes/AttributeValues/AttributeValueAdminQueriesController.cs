using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeValues;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeValues;

[ApiController]
[Route("api/admin/attribute-values")]
[Authorize(Roles = "Admin")]
public class AttributeValueAdminQueriesController
{
    private readonly IAttributeValueQueryService _queryService;


    public AttributeValueAdminQueriesController(
        IAttributeValueQueryService queryService
    )
    {
        _queryService = queryService;
    }


    [HttpGet("category/{categoryId:long}")]
    public virtual async Task<IActionResult> GetByCategoryAsync(
      [FromRoute] long categoryId,
      [FromQuery] PaginationParams pagination,
      [FromQuery] EntityStatus status,
       CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetValuesByCategoryAsync(categoryId,
                pagination, cancellationToken, status);

        return result.ToActionResult();
    }
}
