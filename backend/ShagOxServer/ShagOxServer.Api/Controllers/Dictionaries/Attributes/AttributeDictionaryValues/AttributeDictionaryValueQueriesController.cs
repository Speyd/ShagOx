using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues;
[ApiController]
[Route("api/attribute-dictionary-values")]
public class AttributeDictionaryValueQueriesController
    : BaseQueryController<AttributeDictionaryValueDto,
        AttributeDictionaryValueSearchFilter>
{
    private readonly IAttributeDefinitionQueryService _attributeQueryService;


    public AttributeDictionaryValueQueriesController(
        IAttributeDictionaryValueQueryService queryService,
        IAttributeDefinitionQueryService attributeQueryService
    )
        : base(queryService)
    {
        _attributeQueryService = attributeQueryService;
    }


    [HttpGet("category/{categoryId:long}")]
    public virtual async Task<IActionResult> GetByCategoryAsync(
      [FromRoute] long categoryId,
      [FromQuery] PaginationParams pagination,
       CancellationToken cancellationToken)
    {
        var result = await _attributeQueryService
            .GetValuesByCaregoryAsync(categoryId,
                pagination, cancellationToken);

        return result.ToActionResult();
    }
}
