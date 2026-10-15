using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues;
[ApiController]
[Route("api/attribute-dictionary-values")]
public class AttributeDictionaryValueQueriesController
    : BaseQueryController<AttributeDictionaryValueDto,
        AttributeDictionaryValueSearchFilter>
{
    public AttributeDictionaryValueQueriesController(
        IAttributeDictionaryValueQueryService queryService
    )
        : base(queryService)
    {
    }
}
