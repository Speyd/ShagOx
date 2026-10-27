using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDefinitions;

[ApiController]
[Route("api/attribute-definitions")]
public class AttributeDefinitionQueriesController 
    : BaseQueryController<AttributeDefinitionDto,
        AttributeDefinitionSearchFilter>
{
    public AttributeDefinitionQueriesController(
        IAttributeDefinitionQueryService queryService
    )
        : base(queryService)
    {
    }
}
