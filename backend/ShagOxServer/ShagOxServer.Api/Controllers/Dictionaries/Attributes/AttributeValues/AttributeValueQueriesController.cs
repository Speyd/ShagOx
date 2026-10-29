using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeValues;
[ApiController]
[Route("api/attribute-values")]
public class AttributeValueQueriesController
{
    private readonly IAttributeValueQueryService _queryService;


    public AttributeValueQueriesController(
        IAttributeValueQueryService queryService
    )
    {
        _queryService = queryService;
    }


    [HttpGet("category/{categoryId:long}")]
    public virtual async Task<IActionResult> GetByCategoryAsync(
      [FromRoute] long categoryId,
      [FromQuery] PaginationParams pagination,
       CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetValuesByCategoryAsync(categoryId,
                pagination, cancellationToken);

        return result.ToActionResult();
    }
}
