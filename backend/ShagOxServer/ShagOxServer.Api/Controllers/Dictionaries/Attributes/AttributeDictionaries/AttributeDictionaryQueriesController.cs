using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaries;
[ApiController]
[Route("api/attribute-dictionaries")]
public class AttributeDictionaryQueriesController
    : BaseQueryController<AttributeDictionaryDto,
        AttributeDictionarySearchFilter>
{
    public AttributeDictionaryQueriesController(
        IAttributeDictionaryQueryService queryService
    )
        : base(queryService)
    {
    }
}
